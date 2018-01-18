using System;
using System.Collections.Generic;
using System.Threading;
using com.bitmovin.Api.Codec;
using com.bitmovin.Api.Encoding;
using com.bitmovin.Api.Enums;
using com.bitmovin.Api.Input;
using com.bitmovin.Api.Output;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.bitmovin.Api.Examples
{

    [TestClass]
    public class ProgressiveEncodingWithAzureInputAndAzureOutput
    {

        private const string API_KEY = "YOUR API KEY";

        private const string AZURE_INPUT_TEST_ACCOUNT_NAME = "YOUR AZURE INPUT ACCOUNT NAME";
        private const string AZURE_INPUT_TEST_ACCOUNT_KEY = "YOUR ACURE INPUT ACCOUNT KEY";
        private const string AZURE_INPUT_TEST_CONTAINER_NAME = "YOUR AZURE INPUT CONTAINER NAME";
        private const string AZURE_INPUT_PATH = "/path/to/your/file.ext";

        private const string AZURE_OUTPUT_TEST_ACCOUNT_NAME = "YOUR AZURE OUTPUT ACCOUNT NAME";
        private const string AZURE_OUTPUT_TEST_ACCOUNT_KEY = "YOUR AZURE OUTPUT ACCOUNT KEY";
        private const string AZURE_OUTPUT_TEST_CONTAINER_NAME = "YOUR AZURE OUTPUT CONTAINER NAME";
        private const string AZURE_OUTPUT_PATH = "path/to/output/";
        private const string AZURE_OUTPUT_FILENAME = "yourfilename.webm";


        [TestMethod]
        public void StartVodEncoding()
        {
            // If you run into network errors, try uncommenting the following line.
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3 | System.Net.SecurityProtocolType.Tls12;

            var bitmovin = new BitmovinApi(API_KEY);

            // Create Output
            var output = bitmovin.Output.Azure.Create(new AzureOutput
            {
                Name = "Azure Output",
                AccountName = AZURE_OUTPUT_TEST_ACCOUNT_NAME,
                AccountKey = AZURE_OUTPUT_TEST_ACCOUNT_KEY,
                Container = AZURE_OUTPUT_TEST_CONTAINER_NAME
            });

            // Create encoding
            var encoding = bitmovin.Encoding.Encoding.Create(new Encoding.Encoding
            {
                Name = "VoD Encoding C#",
                CloudRegion = EncodingCloudRegion.GOOGLE_EUROPE_WEST_1,
                EncoderVersion = "STABLE"
            });


            var input = bitmovin.Input.Azure.Create(new AzureInput
            { 
                Name = "Azure Input",
                AccountName = AZURE_INPUT_TEST_ACCOUNT_NAME,
                AccountKey = AZURE_INPUT_TEST_ACCOUNT_KEY,
                Container = AZURE_INPUT_TEST_CONTAINER_NAME
            });


            // Create configurations and streams
            var videoConfig1080p = bitmovin.Codec.VP9.Create(new VP9VideoConfiguration
            {
                Name = "VP9_Profile_1080p",
                Width = 1920,
                Height = 1080,
                Bitrate = 4800000,
                Rate = 30.0f
            });
            var videoStream1080p = bitmovin.Encoding.Encoding.Stream.Create(encoding.Id,
                CreateStream(input, AZURE_INPUT_PATH, 0, videoConfig1080p, SelectionMode.VIDEO_RELATIVE));

            var audioConfig = bitmovin.Codec.Vorbis.Create(new VorbisAudioConfiguration
            {
                Name = "Vorbis_Profile_128k",
                Bitrate = 128000,
                Rate = 48000
            });
            var audioStream = bitmovin.Encoding.Encoding.Stream.Create(encoding.Id,
                CreateStream(input, AZURE_INPUT_PATH, 0, audioConfig, SelectionMode.AUDIO_RELATIVE));


            var muxing = CreateProgressiveWebmMuxing(videoStream1080p, audioStream, output, AZURE_OUTPUT_PATH + "progressiveVideo", AZURE_OUTPUT_FILENAME);

            Console.WriteLine(muxing);

            // Create Progressive Webm
            var videoFMP4Muxing240p = bitmovin.Encoding.Encoding.ProgressiveWebm.Create(encoding.Id,
                muxing);

            // Start encoding
            bitmovin.Encoding.Encoding.Start(encoding.Id);

            var encodingTask = bitmovin.Encoding.Encoding.RetrieveStatus(encoding.Id);

            while (encodingTask.Status != Status.ERROR && encodingTask.Status != Status.FINISHED)
            {
                // Wait for the encoding to finish
                encodingTask = bitmovin.Encoding.Encoding.RetrieveStatus(encoding.Id);
                Thread.Sleep(2500);
            }

            if (encodingTask.Status != Status.FINISHED)
            {
                Console.WriteLine("Encoding could not be finished successfully.");
                return;
            }

            Console.WriteLine("Encoding finished successfully");
        }

        private static ProgressiveWebm CreateProgressiveWebmMuxing(Stream videoStream, Stream audioStream, BaseOutput output, string outputPath,
            string fileName)
        {
            var encodingOutput = new Encoding.Output
            {
                OutputPath = outputPath,
                OutputId = output.Id,
                Acl = new List<Acl> { new Acl { Permission = Permission.PUBLIC_READ } }
            };

            var muxing = new ProgressiveWebm
            {
                Outputs = new List<Encoding.Output> { encodingOutput },
                Streams = new List<MuxingStream> { new MuxingStream { StreamId = videoStream.Id }, new MuxingStream { StreamId = audioStream.Id } },
                Filename = fileName
            };

            return muxing;
        }

        private static Stream CreateStream(AzureInput input, string inputPath, int? position,
            CodecConfig codecConfig, SelectionMode selectionMode)
        {

            var inputStream = new InputStream
            {
                InputId = input.Id,
                InputPath = inputPath,
                Position = position,
                SelectionMode = selectionMode
            };

            var stream = new Stream
            {
                InputStreams = new List<InputStream> {inputStream},
                CodecConfigId = codecConfig.Id
            };

            return stream;
        }


    }
}