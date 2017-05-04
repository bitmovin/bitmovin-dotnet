using System;
using System.Collections.Generic;
using System.Threading;
using com.bitmovin.Api.Codec;
using com.bitmovin.Api.Encoding;
using com.bitmovin.Api.Encoding.Drm;
using com.bitmovin.Api.Enums;
using com.bitmovin.Api.Input;
using com.bitmovin.Api.Manifest;
using com.bitmovin.Api.Manifest.Drm;
using com.bitmovin.Api.Output;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmp4 = com.bitmovin.Api.Encoding.Fmp4;

namespace com.bitmovin.Api.Examples
{

    [TestClass]
    public class DrmCencWidevinePlayReadyDashAes128HlsWithPresetFast
    {
        private const string API_KEY = "YOUR API KEY";

        private const string S3_ACCESS_KEY_OUTPUT = "YOUR S3 ACCESS KEY (OUTPUT)";
        private const string S3_SECRET_KEY_OUTPUT = "YOUR S3 SECRET KEY (OUTPUT)";
        private const string S3_BUCKET_NAME_OUTPUT = "YOUR S3 BUCKET NAME (OUTPUT)";
        private const string OUTPUT_PATH = "path/to/output/";

        private const string S3_ACCESS_KEY_INPUT = "YOUR S3 ACCESS KEY (INPUT)";
        private const string S3_SECRET_KEY_INPUT = "YOUR S3 SECRET KEY (INPUT)";
        private const string S3_BUCKET_NAME_INPUT = "YOUR S3 BUCKET NAME (INPUT)";
        private const string INPUT_PATH = "path/to/input";

        private const string CENC_KEY = "YOUR CENC KEY";
        private const string CENC_KID = "YOUR CENC KID";
        private const string CENC_WIDEVINE_PSSH = "YOUR CENC WIDEVINE PSSH";
        private const string CENC_PLAYREADY_LA_URL = "YOUR CENC PLAYREADY LA URL";

        private const string AES_128_KEY = "YOUR AES-128 KEY";
        private const string AES_128_IV = "YOUR AES-128 IV";

        [TestMethod]
        public void StartVodEncoding()
        {
            var bitmovin = new BitmovinApi(API_KEY);
            double? segmentLength = 4.0;

            // Create Output
            var output = bitmovin.Output.S3.Create(new S3Output
            {
                Name = "S3 Ouput",
                AccessKey = S3_ACCESS_KEY_OUTPUT,
                SecretKey = S3_SECRET_KEY_OUTPUT,
                BucketName = S3_BUCKET_NAME_OUTPUT
            });

            // Create encoding
            var encoding = bitmovin.Encoding.Encoding.Create(new Encoding.Encoding
            {
                Name = "VoD Encoding C# - DRM CENC + AES 128",
                CloudRegion = EncodingCloudRegion.GOOGLE_EUROPE_WEST_1,
                EncoderVersion = "STABLE"
            });


            var input = bitmovin.Input.S3.Create(new S3Input
            { 
                Name = "S3 Input",
                AccessKey = S3_ACCESS_KEY_INPUT,
                SecretKey = S3_SECRET_KEY_INPUT,
                BucketName = S3_BUCKET_NAME_INPUT
            });



            // Create configurations and streams
            var videoConfig360p = bitmovin.Codec.H264.Create(new H264VideoConfiguration
            {
                Name = "H264_Profile_360p",
                Profile = H264Profile.MAIN,
                Width = 640,
                Height = 360,
                Bitrate = 800000,
                BFrames = 3,
                Cabac = true,
                MvSearchRangeMax = 16,
                RefFrames = 2,
                MvPredictionMode = MvPredictionMode.SPATIAL,
                RcLookahead = 30,
                SubMe = H264SubMe.RD_IP,
                MotionEstimationMethod = H264MotionEstimationMethod.HEX,
                BAdaptiveStrategy = BAdapt.FAST,
                Partitions = new List<H264Partition> { H264Partition.I4X4, H264Partition.I8X8, H264Partition.P8X8, H264Partition.B8X8 }
            });
            var videoStream360p = bitmovin.Encoding.Encoding.Stream.Create(encoding.Id,
                CreateStream(input, INPUT_PATH, 0, videoConfig360p, SelectionMode.VIDEO_RELATIVE));

            var audioConfig = bitmovin.Codec.Aac.Create(new AACAudioConfiguration
            {
                Name = "AAC_Profile_128k",
                Bitrate = 128000,
                Rate = 48000
            });
            var audioStream = bitmovin.Encoding.Encoding.Stream.Create(encoding.Id,
                CreateStream(input, INPUT_PATH, 0, audioConfig, SelectionMode.AUDIO_RELATIVE));


            // Create FMP4 Muxing for DASH
            var videoFMP4Muxing360p = bitmovin.Encoding.Encoding.Fmp4.Create(encoding.Id,
                CreateFMP4Muxing(videoStream360p, segmentLength));
            var cencFmp4_360p = bitmovin.Encoding.Encoding.Fmp4.CencDrm.Create(encoding.Id, videoFMP4Muxing360p.Id,
                CreateCencDrmForFMP4Muxing(output, OUTPUT_PATH + "video/360p_dash"));
            var audioFMP4Muxing = bitmovin.Encoding.Encoding.Fmp4.Create(encoding.Id,
                CreateFMP4Muxing(audioStream, segmentLength));
            var cencFmp4_Audio = bitmovin.Encoding.Encoding.Fmp4.CencDrm.Create(encoding.Id, audioFMP4Muxing.Id,
                CreateCencDrmForFMP4Muxing(output, OUTPUT_PATH + "audio/128kbps_dash"));


            // Create TS Muxings for HLS
            var videoTsMuxing360p = bitmovin.Encoding.Encoding.Ts.Create(encoding.Id,
                CreateTsMuxing(videoStream360p, segmentLength));
            var aesTs_360p = bitmovin.Encoding.Encoding.Ts.Aes.Create(encoding.Id, videoTsMuxing360p.Id,
                CreateAes128ForTsMuxing(output, OUTPUT_PATH + "video/360p_hls"));
            var audioTsMuxing = bitmovin.Encoding.Encoding.Ts.Create(encoding.Id,
                CreateTsMuxing(audioStream, segmentLength));
            var aesTs_Audio = bitmovin.Encoding.Encoding.Ts.Aes.Create(encoding.Id, audioTsMuxing.Id,
                CreateAes128ForTsMuxing(output, OUTPUT_PATH + "audio/128kbps_hls"));

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


            // Create manifest output (can be used for both HLS + DASH)
            var manifestOutput = new Encoding.Output
            {
                OutputPath = OUTPUT_PATH,
                OutputId = output.Id,
                Acl = new List<Acl> { new Acl { Permission = Permission.PUBLIC_READ } }
            };


            // Create HLS Manifest
            var manifestHls = bitmovin.Manifest.Hls.Create(new Hls
            {
                Name = "HLS Manifest",
                ManifestName = "stream.m3u8",
                Outputs = new List<Encoding.Output> { manifestOutput }
            });

            var mediaInfo = new MediaInfo
            {
                GroupId = "audio",
                Name = "English",
                Uri = "audio.m3u8",
                Type = MediaType.AUDIO,
                SegmentPath = "audio/128kbps_hls/",
                StreamId = audioStream.Id,
                MuxingId = audioTsMuxing.Id,
                EncodingId = encoding.Id,
                DrmId = aesTs_Audio.Id,
                Language = "en",
                AssocLanguage = "en",
                Autoselect = false,
                IsDefault = false,
                Forced = false
            };

            bitmovin.Manifest.Hls.AddMediaInfo(manifestHls.Id, mediaInfo);
            
            bitmovin.Manifest.Hls.AddStreamInfo(manifestHls.Id, new StreamInfo
            {
                Uri = "video_360.m3u8",
                EncodingId = encoding.Id,
                StreamId = videoStream360p.Id,
                MuxingId = videoTsMuxing360p.Id,
                DrmId = aesTs_360p.Id,
                Audio = "audio",
                SegmentPath = "video/360p_hls/"
            });

            bitmovin.Manifest.Hls.Start(manifestHls.Id);
            var hlsManifestStatus = bitmovin.Manifest.Hls.RetrieveStatus(manifestHls.Id);

            while (hlsManifestStatus.Status != Status.ERROR && hlsManifestStatus.Status != Status.FINISHED)
            {
                // Wait for the HLS Manifest creation to finish
                hlsManifestStatus = bitmovin.Manifest.Hls.RetrieveStatus(manifestHls.Id);
                Thread.Sleep(2500);
            }

            if (hlsManifestStatus.Status != Status.FINISHED)
            {
                Console.WriteLine("HLS Manifest could not be finished successfully.");
                return;
            }

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
            bitmovin.Manifest.Dash.ContentProtection.Create(manifestDash.Id, period.Id, videoAdaptationSet.Id,
                new ContentProtection
                {
                    EncodingId = encoding.Id,
                    MuxingId = videoFMP4Muxing360p.Id,
                    DrmId = cencFmp4_360p.Id
                });
            var audioAdaptationSet = bitmovin.Manifest.Dash.AudioAdaptationSet.Create(manifestDash.Id, period.Id,
                new AudioAdaptationSet { Lang = "en" });
            bitmovin.Manifest.Dash.ContentProtection.Create(manifestDash.Id, period.Id, audioAdaptationSet.Id,
                new ContentProtection
                {
                    EncodingId = encoding.Id,
                    MuxingId = audioFMP4Muxing.Id,
                    DrmId = cencFmp4_Audio.Id
                });

            bitmovin.Manifest.Dash.DrmFmp4.Create(manifestDash.Id, period.Id, audioAdaptationSet.Id,
                new DrmFmp4
                {
                    Type = SegmentScheme.TEMPLATE,
                    EncodingId = encoding.Id,
                    MuxingId = audioFMP4Muxing.Id,
                    DrmId = cencFmp4_Audio.Id,
                    SegmentPath = "audio/128kbps_dash"
                });

            bitmovin.Manifest.Dash.DrmFmp4.Create(manifestDash.Id, period.Id, videoAdaptationSet.Id,
                new DrmFmp4
                {
                    Type = SegmentScheme.TEMPLATE,
                    EncodingId = encoding.Id,
                    MuxingId = videoFMP4Muxing360p.Id,
                    DrmId = cencFmp4_360p.Id,
                    SegmentPath = "video/360p_dash"
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

        private static Ts CreateTsMuxing(Stream stream, double? segmentLength)
        {

            var muxing = new Ts
            {
                Streams = new List<MuxingStream> {new MuxingStream {StreamId = stream.Id}},
                SegmentNaming = "segment_%number%.ts",
                SegmentLength = segmentLength
            };

            return muxing;
        }

        private static Fmp4 CreateFMP4Muxing(Stream stream, double? segmentLength)
        {

            var muxing = new Fmp4
            {
                Streams = new List<MuxingStream> { new MuxingStream { StreamId = stream.Id } },
                SegmentLength = segmentLength
            };

            return muxing;
        }

        private static AESEncryption CreateAes128ForTsMuxing(BaseObject output, string outputPath)
        {

            var encodingOutput = new Encoding.Output
            {
                OutputPath = outputPath,
                OutputId = output.Id,
                Acl = new List<Acl> { new Acl { Permission = Permission.PUBLIC_READ } }
            };
            return new AESEncryption
            {
                Key = AES_128_KEY,
                Iv = AES_128_IV,
                Method = AesMethod.AES_128,
                Outputs = new List<Encoding.Output>() { encodingOutput }
            };
        }

        private static CencDrm CreateCencDrmForFMP4Muxing(BaseObject output, string outputPath)
        {

            var encodingOutput = new Encoding.Output
            {
                OutputPath = outputPath,
                OutputId = output.Id,
                Acl = new List<Acl> { new Acl { Permission = Permission.PUBLIC_READ } }
            };
            return new CencDrm
            {
                Key = CENC_KEY,
                Kid = CENC_KID,
                Widevine = new CencWidevine
                {
                    Pssh = CENC_WIDEVINE_PSSH
                },
                PlayReady = new CencPlayReady
                {
                    LaUrl = CENC_PLAYREADY_LA_URL
                },
                Outputs = new List<Encoding.Output>() { encodingOutput }
            };
        }
        
        private static Stream CreateStream(S3Input input, string inputPath, int? position,
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
