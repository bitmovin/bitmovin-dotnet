using System;
using System.Collections.Generic;
using System.Threading;
using com.bitmovin.Api.Codec;
using com.bitmovin.Api.Encoding;
using com.bitmovin.Api.Enums;
using com.bitmovin.Api.Input;
using com.bitmovin.Api.Manifest;
using com.bitmovin.Api.Output;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmp4 = com.bitmovin.Api.Encoding.Fmp4;

namespace com.bitmovin.Api.Examples
{

    [TestClass]
    public class VP9VodEncoding
    {

        private const string API_KEY = "YOUR API KEY";
        private const string GCS_ACCESS_KEY = "GCS ACCESS KEY";
        private const string GCS_SECRET_KEY = "GCS SECRET KEY";
        private const string GCS_BUCKET_NAME = "GCS BUCKET NAME";
        private const string OUTPUT_PATH = "path/to/output/";
        private const string INPUT_HTTP_HOST = "my.inputhost.com/";
        private const string INPUT_HTTP_PATH = "path/to/input";

        [TestMethod]
        public void StartVodEncoding()
        {
            var bitmovin = new BitmovinApi(API_KEY);
            double? segmentLength = 4.0;

            // Create Output
            var output = bitmovin.Output.Gcs.Create(new GcsOutput
            {
                Name = "GCS Ouput",
                AccessKey = GCS_ACCESS_KEY,
                SecretKey = GCS_SECRET_KEY,
                BucketName = GCS_BUCKET_NAME
            });

            // Create encoding
            var encoding = bitmovin.Encoding.Encoding.Create(new Encoding.Encoding
            {
                Name = "VoD VP9 Encoding C#",
                CloudRegion = EncodingCloudRegion.GOOGLE_EUROPE_WEST_1,
                EncoderVersion = "STABLE"
            });


            var httpHost = bitmovin.Input.Http.Create(new HttpInput
            { 
                Name = "HTTP Input",
                Host = INPUT_HTTP_HOST
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
                CreateStream(httpHost, INPUT_HTTP_PATH, 0, videoConfig1080p, SelectionMode.VIDEO_RELATIVE));

            var videoConfig720p = bitmovin.Codec.VP9.Create(new VP9VideoConfiguration
            {
                Name = "VP9_Profile_720p",
                Width = 1280,
                Height = 720,
                Bitrate = 2400000,
                Rate = 30.0f
            });
            var videoStream720p = bitmovin.Encoding.Encoding.Stream.Create(encoding.Id,
                CreateStream(httpHost, INPUT_HTTP_PATH, 0, videoConfig720p, SelectionMode.VIDEO_RELATIVE));

            var videoConfig480p = bitmovin.Codec.VP9.Create(new VP9VideoConfiguration
            {
                Name = "VP9_Profile_480p",
                Width = 858,
                Height = 480,
                Bitrate = 1200000,
                Rate = 30.0f
            });
            var videoStream480p = bitmovin.Encoding.Encoding.Stream.Create(encoding.Id,
                CreateStream(httpHost, INPUT_HTTP_PATH, 0, videoConfig480p, SelectionMode.VIDEO_RELATIVE));

            var videoConfig360p = bitmovin.Codec.VP9.Create(new VP9VideoConfiguration
            {
                Name = "VP9_Profile_360p",
                Width = 640,
                Height = 360,
                Bitrate = 800000,
                Rate = 30.0f
            });
            var videoStream360p = bitmovin.Encoding.Encoding.Stream.Create(encoding.Id,
                CreateStream(httpHost, INPUT_HTTP_PATH, 0, videoConfig360p, SelectionMode.VIDEO_RELATIVE));

            var videoConfig240p = bitmovin.Codec.VP9.Create(new VP9VideoConfiguration
            {
                Name = "VP9_Profile_240p",
                Width = 426,
                Height = 240,
                Bitrate = 400000,
                Rate = 30.0f
            });
            var videoStream240p = bitmovin.Encoding.Encoding.Stream.Create(encoding.Id,
                CreateStream(httpHost, INPUT_HTTP_PATH, 0, videoConfig240p, SelectionMode.VIDEO_RELATIVE));

            var audioConfig = bitmovin.Codec.Aac.Create(new AACAudioConfiguration
            {
                Name = "AAC_Profile_128k",
                Bitrate = 128000,
                Rate = 48000
            });
            var audioStream = bitmovin.Encoding.Encoding.Stream.Create(encoding.Id,
                CreateStream(httpHost, INPUT_HTTP_PATH, 0, audioConfig, SelectionMode.AUDIO_RELATIVE));


            // Create Muxing for DASH
            var videoWebmMuxing240p = bitmovin.Encoding.Encoding.SegmentedWebm.Create(encoding.Id,
                CreateSegmentedWebmMuxing(videoStream240p, output, OUTPUT_PATH + "video/240p", segmentLength));
            var videoWebmMuxing360p = bitmovin.Encoding.Encoding.SegmentedWebm.Create(encoding.Id,
                CreateSegmentedWebmMuxing(videoStream360p, output, OUTPUT_PATH + "video/360p", segmentLength));
            var videoWebmMuxing480p = bitmovin.Encoding.Encoding.SegmentedWebm.Create(encoding.Id,
                CreateSegmentedWebmMuxing(videoStream480p, output, OUTPUT_PATH + "video/480p", segmentLength));
            var videoWebmMuxing720p = bitmovin.Encoding.Encoding.SegmentedWebm.Create(encoding.Id,
                CreateSegmentedWebmMuxing(videoStream720p, output, OUTPUT_PATH + "video/720p", segmentLength));
            var videoWebmMuxing1080p = bitmovin.Encoding.Encoding.SegmentedWebm.Create(encoding.Id,
                CreateSegmentedWebmMuxing(videoStream1080p, output, OUTPUT_PATH + "video/1080p", segmentLength));
            var audioFMP4Muxing = bitmovin.Encoding.Encoding.Fmp4.Create(encoding.Id,
                CreateFMP4Muxing(audioStream, output, OUTPUT_PATH + "audio/128kbps", segmentLength));
            
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


            // Create manifest output
            var manifestOutput = new Encoding.Output
            {
                OutputPath = OUTPUT_PATH,
                OutputId = output.Id,
                Acl = new List<Acl> { new Acl { Permission = Permission.PUBLIC_READ } }
            };
            
            // Create DASH Manifest
            var manifestDash = bitmovin.Manifest.Dash.Create(new Dash
            {
                Name = "MPEG-DASH Manifest",
                ManifestName = "stream.mpd",
                Outputs = new List<Encoding.Output> { manifestOutput }
            });
            var period = bitmovin.Manifest.Dash.Period.Create(manifestDash.Id, new Period());
            var videoAdaptationSet =
                bitmovin.Manifest.Dash.VideoAdaptationSet.Create(manifestDash.Id, period.Id, new VideoAdaptationSet());
            var audioAdaptationSet = bitmovin.Manifest.Dash.AudioAdaptationSet.Create(manifestDash.Id, period.Id,
                new AudioAdaptationSet { Lang = "en" });

            bitmovin.Manifest.Dash.Fmp4.Create(manifestDash.Id, period.Id, audioAdaptationSet.Id,
                new Manifest.Fmp4
                {
                    Type = SegmentScheme.TEMPLATE,
                    EncodingId = encoding.Id,
                    MuxingId = audioFMP4Muxing.Id,
                    SegmentPath = "audio/128kbps"
                });

            bitmovin.Manifest.Dash.Webm.Create(manifestDash.Id, period.Id, videoAdaptationSet.Id,
                new Manifest.Webm
                {
                    Type = SegmentScheme.TEMPLATE,
                    EncodingId = encoding.Id,
                    MuxingId = videoWebmMuxing240p.Id,
                    SegmentPath = "video/240p"
                });
            bitmovin.Manifest.Dash.Webm.Create(manifestDash.Id, period.Id, videoAdaptationSet.Id,
                new Manifest.Webm
                {
                    Type = SegmentScheme.TEMPLATE,
                    EncodingId = encoding.Id,
                    MuxingId = videoWebmMuxing360p.Id,
                    SegmentPath = "video/360p"
                });
            bitmovin.Manifest.Dash.Webm.Create(manifestDash.Id, period.Id, videoAdaptationSet.Id,
                new Manifest.Webm
                {
                    Type = SegmentScheme.TEMPLATE,
                    EncodingId = encoding.Id,
                    MuxingId = videoWebmMuxing480p.Id,
                    SegmentPath = "video/480p"
                });
            bitmovin.Manifest.Dash.Webm.Create(manifestDash.Id, period.Id, videoAdaptationSet.Id,
                new Manifest.Webm
                {
                    Type = SegmentScheme.TEMPLATE,
                    EncodingId = encoding.Id,
                    MuxingId = videoWebmMuxing720p.Id,
                    SegmentPath = "video/720p"
                });
            bitmovin.Manifest.Dash.Webm.Create(manifestDash.Id, period.Id, videoAdaptationSet.Id,
                new Manifest.Webm
                {
                    Type = SegmentScheme.TEMPLATE,
                    EncodingId = encoding.Id,
                    MuxingId = videoWebmMuxing1080p.Id,
                    SegmentPath = "video/1080p"
                });
            
            bitmovin.Manifest.Dash.Start(manifestDash.Id);
            var dashManifestStatus = bitmovin.Manifest.Dash.RetrieveStatus(manifestDash.Id);

            while (dashManifestStatus.Status != Status.ERROR && dashManifestStatus.Status != Status.FINISHED)
            {
                // Wait for the Dash Manifest creation to finish
                dashManifestStatus = bitmovin.Manifest.Dash.RetrieveStatus(manifestDash.Id);
                Thread.Sleep(2500);
            }

            if (dashManifestStatus.Status != Status.FINISHED)
            {
                Console.WriteLine("DASH Manifest could not be finished successfully.");
                return;
            }

            Console.WriteLine("Encoding finished successfully");
        }

        private static SegmentedWebm CreateSegmentedWebmMuxing(Stream stream, BaseOutput output, string outputPath,
            double? segmentLength)
        {
            var encodingOutput = new Encoding.Output
            {
                OutputPath = outputPath,
                OutputId = output.Id,
                Acl = new List<Acl> { new Acl { Permission = Permission.PUBLIC_READ } }
            };

            var muxing = new SegmentedWebm
            {
                Outputs = new List<Encoding.Output> { encodingOutput },
                Streams = new List<MuxingStream> { new MuxingStream { StreamId = stream.Id } },
                SegmentLength = segmentLength,
                InitSegmentName = "init.hdr",
                SegmentNaming = "segment_%number%.chk"
            };

            return muxing;
        }

        private static Stream CreateStream(HttpInput input, string inputPath, int? position,
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

    
        private static Fmp4 CreateFMP4Muxing(Stream stream, BaseOutput output, string outputPath,
            double? segmentLength)
        {
            var encodingOutput = new Encoding.Output
            {
                OutputPath = outputPath,
                OutputId = output.Id,
                Acl = new List<Acl> { new Acl { Permission = Permission.PUBLIC_READ } }
            };

            var muxing = new Fmp4
            {
                Outputs = new List<Encoding.Output> { encodingOutput },
                Streams = new List<MuxingStream> { new MuxingStream { StreamId = stream.Id } },
                SegmentLength = segmentLength
            };

            return muxing;
        }

    }
}