using com.bitmovin.Api.Codec;
using com.bitmovin.Api.Encoding;
using com.bitmovin.Api.Encoding.PerTitleEncoding;
using com.bitmovin.Api.Enums;
using com.bitmovin.Api.Exception;
using com.bitmovin.Api.Input;
using com.bitmovin.Api.Output;
using System;
using System.Collections.Generic;
using System.Threading;

namespace com.bitmovin.Api.Examples
{
    public class StartPerTitleEncoding
    {
        private const string API_KEY = "YOUR API KEY";

        private const string S3_INPUT_BUCKETNAME = "S3 BUCKET NAME";
        private const string S3_INPUT_ACCESS_KEY = "S3 ACCESS KEY";
        private const string S3_INPUT_SECRET_KEY = "S3 SECRET KEY";
        private const string S3_INPUT_PATH = "/path/to/your/input/file.mp4";

        private const string S3_OUTPUT_BUCKETNAME = "S3 BUCKET NAME";
        private const string S3_OUTPUT_ACCESS_KEY = "S3 ACCESS KEY";
        private const string S3_OUTPUT_SECRET_KEY = "S3 SECRET KEY";
        private const string S3_OUTPUT_BASE_PATH = "/your/output/base/path/";

        private static BitmovinApi bitmovin = new BitmovinApi(API_KEY);

        public void StartPerTitle()
        {
            try
            {
                // Create the input resource to access the input file
                var input = bitmovin.Input.S3.Create(new S3Input
                {
                    Name = "Sample S3 Input",
                    BucketName = S3_INPUT_BUCKETNAME,
                    AccessKey = S3_INPUT_ACCESS_KEY,
                    SecretKey = S3_INPUT_SECRET_KEY
                });

                // Create the output resource to write the output files
                var output = bitmovin.Output.S3.Create(new S3Output
                {
                    Name = "Sample S3 Output",
                    BucketName = S3_OUTPUT_BUCKETNAME,
                    AccessKey = S3_OUTPUT_ACCESS_KEY,
                    SecretKey = S3_OUTPUT_SECRET_KEY
                });

                // Create the encoding
                var encoding = bitmovin.Encoding.Encoding.Create(new Encoding.Encoding(".NET Per-Title Example"));

                // Select the video and audio input stream that should be encoded
                var audioInputStream = new InputStream
                {
                    InputId = input.Id,
                    InputPath = S3_INPUT_PATH,
                    SelectionMode = SelectionMode.AUTO
                };

                var videoInputStream = new InputStream
                {
                    InputId = input.Id,
                    InputPath = S3_INPUT_PATH,
                    SelectionMode = SelectionMode.AUTO
                };

                Stream audioStream = CreateAudioStream(encoding, audioInputStream);
                Stream videoStream = CreatePerTitleVideoStream(encoding, videoInputStream);

                CreateMP4Muxing(encoding, output, videoStream, audioStream);

                StartEncoding(encoding);
            }
            catch (BitmovinApiException ex)
            {
                Console.WriteLine(ex.Exception.Data.Message);
                Console.WriteLine("=================================");
                Console.WriteLine(ex.Exception.Data.DeveloperMessage);
                Console.ReadKey();
            }
        }

        /// <summary>
        /// This will create the audio stream that will be encoded with the given codec configuration.
        /// </summary>
        /// <param name="encoding">The reference of the encoding</param>
        /// <param name="audioInputStream">The input stream that should be encoded</param>
        /// <returns>The created audio stream. This will be used later for the MP4 muxing</returns>
        private static Stream CreateAudioStream(Encoding.Encoding encoding, InputStream audioInputStream)
        {
            var audioConfig = bitmovin.Codec.Aac.Create(new AACAudioConfiguration
            {
                Name = "AAC_Profile_128k",
                Bitrate = 128000,
                Rate = 48000
            });

            return bitmovin.Encoding.Encoding.Stream.Create(encoding.Id, new Stream
            {
                InputStreams = new List<InputStream> { audioInputStream },
                CodecConfigId = audioConfig.Id,
                Mode = StreamMode.STANDARD
            });
        }

        /// <summary>
        /// This will create the Per-Title template video stream. This stream will be used as a template for the Per-Title 
        /// encoding.The Codec Configuration, Muxings, DRMs and Filters applied to the generated Per-Title profile will be
        /// based on the same, or closest matching resolutions defined in the template.
        /// Please note, that template streams are not necessarily used for the encoding - they are just used as template.
        /// </summary>
        /// <param name="encoding">The reference of the encoding</param>
        /// <param name="videoInputStream">The input stream that should be encoded</param>
        /// <returns>The created Per-Title template video stream. This will be used later for the MP4 muxing</returns>
        private static Stream CreatePerTitleVideoStream(Encoding.Encoding encoding, InputStream videoInputStream)
        {
            var videoConfig = bitmovin.Codec.H264.Create(new H264VideoConfiguration
            {
                Name = "H264_Profile",
                Profile = H264Profile.HIGH
            });

            return bitmovin.Encoding.Encoding.Stream.Create(encoding.Id, new Stream
            {
                InputStreams = new List<InputStream> { videoInputStream },
                CodecConfigId = videoConfig.Id,
                Mode = StreamMode.PER_TITLE_TEMPLATE
            });
        }

        /// <summary>
        /// An MP4 muxing will be created for with the Per-Title video stream template and the audio stream.
        /// This muxing must define either {uuid} or {bitrate} in the output path.
        /// These placeholders will be replaced during the generation of the Per-Title.
        /// </summary>
        /// <param name="encoding">The reference of the encoding</param>
        /// <param name="output">The output the files should be written to</param>
        /// <param name="videoStream">The Per-Title template video stream</param>
        /// <param name="audioStream">The audio stream</param>
        private static Mp4 CreateMP4Muxing(Encoding.Encoding encoding, BaseOutput output, Stream videoStream, Stream audioStream)
        {
            var encodingOutput = new Encoding.Output
            {
                OutputPath = System.IO.Path.Combine(S3_OUTPUT_BASE_PATH, "{width}_{bitrate}_{uuid}"),
                OutputId = output.Id,
                Acl = new List<Acl> { new Acl { Permission = Permission.PUBLIC_READ } }
            };

            var muxing = new Mp4
            {
                Filename = "video.mp4",
                Outputs = new List<Encoding.Output> { encodingOutput },
                Streams = new List<MuxingStream> { new MuxingStream { StreamId = videoStream.Id }, new MuxingStream { StreamId = audioStream.Id } }
            };

            return bitmovin.Encoding.Encoding.Mp4.Create(encoding.Id, muxing);
        }

        private static Stream CreateStream(InputStream inputStream, CodecConfig codecConfig, StreamMode streamMode)
        {
            return new Stream
            {
                InputStreams = new List<InputStream> { inputStream },
                CodecConfigId = codecConfig.Id,
                Mode = streamMode
            };
        }

        /// <summary>
        /// The encoding will be started with the per title object and the auto representations set. If the auto
        /// representation is set, stream configurations will be automatically added to the Per-Title profile. In that case
        /// at least one PER_TITLE_TEMPLATE stream configuration must be available. All other configurations will be
        /// automatically chosen by the Per-Title algorithm. All relevant settings for streams and muxings will be taken from
        /// the closest PER_TITLE_TEMPLATE stream defined. The closest stream will be chosen based on the resolution
        /// specified in the codec configuration.
        /// </summary>
        /// <param name="encoding">The reference of the encoding</param>
        private static void StartEncoding(Encoding.Encoding encoding)
        {
            var h264PerTitleConfig = new H264PerTitleConfiguration
            {
                AutoRepresentations = new AutoRepresentation()
            };

            var startEncodingRequest = new StartEncodingRequest
            {
                PerTitle = new PerTitle(h264PerTitleConfig),
                EncodingMode = EncodingMode.THREE_PASS,
            };

            bitmovin.Encoding.Encoding.Start(encoding.Id, startEncodingRequest);

            var encodingTask = bitmovin.Encoding.Encoding.RetrieveStatus(encoding.Id);

            // Wait for the encoding to finish
            while (encodingTask.Status != Status.ERROR && encodingTask.Status != Status.FINISHED)
            {
                encodingTask = bitmovin.Encoding.Encoding.RetrieveStatus(encoding.Id);
                Console.WriteLine("Status: {0}, Progress: {1}%", encodingTask.Status, encodingTask.Progress);
                Thread.Sleep(5000);
            }

            if (encodingTask.Status != Status.FINISHED)
            {
                Console.WriteLine("Encoding could not be finished successfully.");
                return;
            }
        }
    }
}
