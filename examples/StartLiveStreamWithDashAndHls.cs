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
    public class StartLiveStreamWithDashAndHls
    {
        private const string API_KEY = "YOUR API KEY";
        private const string GCS_ACCESS_KEY = "GCS ACCESS KEY";
        private const string GCS_SECRET_KEY = "GCS SECRET KEY";
        private const string GCS_BUCKET_NAME = "GCS BUCKET NAME";
        private const string OUTPUT_PATH = "path/to/output/";

        [TestMethod]
        public void StartLiveStream()
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
                Name = "Live Stream C#",
                CloudRegion = EncodingCloudRegion.GOOGLE_EUROPE_WEST_1,
                EncoderVersion = "STABLE"
            });


            var rtmpInput = bitmovin.Input.Rtmp.RetrieveList(0, 100)[0];


            // Create configurations and streams
            var videoConfig1080p = bitmovin.Codec.H264.Create(new H264VideoConfiguration
            {
                Name = "H264_Profile_1080p",
                Profile = H264Profile.HIGH,
                Width = 1920,
                Height = 1080,
                Bitrate = 4800000,
                Rate = 30.0f
            });
            var videoStream1080p = bitmovin.Encoding.Encoding.Stream.Create(encoding.Id,
                CreateStream(rtmpInput, "live", 0, videoConfig1080p, SelectionMode.AUTO));

            var videoConfig720p = bitmovin.Codec.H264.Create(new H264VideoConfiguration
            {
                Name = "H264_Profile_720p",
                Profile = H264Profile.HIGH,
                Width = 1280,
                Height = 720,
                Bitrate = 2400000,
                Rate = 30.0f
            });
            var videoStream720p = bitmovin.Encoding.Encoding.Stream.Create(encoding.Id,
                CreateStream(rtmpInput, "live", 0, videoConfig720p, SelectionMode.AUTO));

            var videoConfig480p = bitmovin.Codec.H264.Create(new H264VideoConfiguration
            {
                Name = "H264_Profile_480p",
                Profile = H264Profile.HIGH,
                Width = 858,
                Height = 480,
                Bitrate = 1200000,
                Rate = 30.0f
            });
            var videoStream480p = bitmovin.Encoding.Encoding.Stream.Create(encoding.Id,
                CreateStream(rtmpInput, "live", 0, videoConfig480p, SelectionMode.AUTO));

            var videoConfig360p = bitmovin.Codec.H264.Create(new H264VideoConfiguration
            {
                Name = "H264_Profile_360p",
                Profile = H264Profile.HIGH,
                Width = 640,
                Height = 360,
                Bitrate = 800000,
                Rate = 30.0f
            });
            var videoStream360p = bitmovin.Encoding.Encoding.Stream.Create(encoding.Id,
                CreateStream(rtmpInput, "live", 0, videoConfig360p, SelectionMode.AUTO));

            var videoConfig240p = bitmovin.Codec.H264.Create(new H264VideoConfiguration
            {
                Name = "H264_Profile_240p",
                Profile = H264Profile.HIGH,
                Width = 426,
                Height = 240,
                Bitrate = 400000,
                Rate = 30.0f
            });
            var videoStream240p = bitmovin.Encoding.Encoding.Stream.Create(encoding.Id,
                CreateStream(rtmpInput, "live", 0, videoConfig240p, SelectionMode.AUTO));

            var audioConfig = bitmovin.Codec.Aac.Create(new AACAudioConfiguration
            {
                Name = "AAC_Profile_128k",
                Bitrate = 128000,
                Rate = 48000
            });
            var audioStream = bitmovin.Encoding.Encoding.Stream.Create(encoding.Id,
                CreateStream(rtmpInput, "/", 1, audioConfig, SelectionMode.AUTO));


            // Create TS Muxings for HLS
            var videoTsMuxing240p = bitmovin.Encoding.Encoding.Ts.Create(encoding.Id,
                CreateTsMuxing(videoStream240p, output, OUTPUT_PATH + "video/240p_hls", segmentLength));
            var videoTsMuxing360p = bitmovin.Encoding.Encoding.Ts.Create(encoding.Id,
                CreateTsMuxing(videoStream360p, output, OUTPUT_PATH + "video/360p_hls", segmentLength));
            var videoTsMuxing480p = bitmovin.Encoding.Encoding.Ts.Create(encoding.Id,
                CreateTsMuxing(videoStream480p, output, OUTPUT_PATH + "video/480p_hls", segmentLength));
            var videoTsMuxing720p= bitmovin.Encoding.Encoding.Ts.Create(encoding.Id,
                CreateTsMuxing(videoStream720p, output, OUTPUT_PATH + "video/720p_hls", segmentLength));
            var videoTsMuxing1080p = bitmovin.Encoding.Encoding.Ts.Create(encoding.Id,
                CreateTsMuxing(videoStream1080p, output, OUTPUT_PATH + "video/1080p_hls", segmentLength));
            var audioTsMuxing = bitmovin.Encoding.Encoding.Ts.Create(encoding.Id,
                CreateTsMuxing(audioStream, output, OUTPUT_PATH + "audio/128kbps_hls", segmentLength));

            // Create manifest output (can be used for both HLS + DASH)
            var manifestOutput = new Encoding.Output
            {
                OutputPath = OUTPUT_PATH,
                OutputId = output.Id,
                Acl = new List<Acl> {new Acl {Permission = Permission.PUBLIC_READ}}
            };


            // Create HLS Manifest
            var manifestHls = bitmovin.Manifest.Hls.Create(new Hls
            {
                Name = "stream.m3u8",
                ManifestName = "stream.m3u8",
                Outputs = new List<Encoding.Output> {manifestOutput}
            });

            var mediaInfo = new MediaInfo
            {
                GroupId = "audio",
                Name = "audio.m3u8",
                Uri = "audio.m3u8",
                Type = MediaType.AUDIO,
                SegmentPath = "audio/128kbps_hls/",
                StreamId = audioStream.Id,
                MuxingId = audioTsMuxing.Id,
                EncodingId = encoding.Id,
                Language = "en",
                AssocLanguage = "en",
                Autoselect = false,
                IsDefault = false,
                Forced = false,
                Characteristics = new List<string> { "public.accessibility.describes-audio" }
            };

            bitmovin.Manifest.Hls.AddMediaInfo(manifestHls.Id, mediaInfo);

            bitmovin.Manifest.Hls.AddStreamInfo(manifestHls.Id, new StreamInfo
            {
                Uri = "video_240.m3u8",
                EncodingId = encoding.Id,
                StreamId = videoStream240p.Id,
                MuxingId = videoTsMuxing240p.Id,
                Audio = "audio",
                SegmentPath = "video/240p_hls/"
            });
            bitmovin.Manifest.Hls.AddStreamInfo(manifestHls.Id, new StreamInfo
            {
                Uri = "video_360.m3u8",
                EncodingId = encoding.Id,
                StreamId = videoStream360p.Id,
                MuxingId = videoTsMuxing360p.Id,
                Audio = "audio",
                SegmentPath = "video/360p_hls/"
            });
            bitmovin.Manifest.Hls.AddStreamInfo(manifestHls.Id, new StreamInfo
            {
                Uri = "video_480.m3u8",
                EncodingId = encoding.Id,
                StreamId = videoStream480p.Id,
                MuxingId = videoTsMuxing480p.Id,
                Audio = "audio",
                SegmentPath = "video/480p_hls/"
            });
            bitmovin.Manifest.Hls.AddStreamInfo(manifestHls.Id, new StreamInfo
            {
                Uri = "video_720.m3u8",
                EncodingId = encoding.Id,
                StreamId = videoStream720p.Id,
                MuxingId = videoTsMuxing720p.Id,
                Audio = "audio",
                SegmentPath = "video/720p_hls/"
            });
            bitmovin.Manifest.Hls.AddStreamInfo(manifestHls.Id, new StreamInfo
            {
                Uri = "video_1080.m3u8",
                EncodingId = encoding.Id,
                StreamId = videoStream1080p.Id,
                MuxingId = videoTsMuxing1080p.Id,
                Audio = "audio",
                SegmentPath = "video/1080p_hls/"
            });



            // Create FMP4 Muxing for DASH
            var videoFMP4Muxing240p = bitmovin.Encoding.Encoding.Fmp4.Create(encoding.Id,
                CreateFMP4Muxing(videoStream240p, output, OUTPUT_PATH + "video/240p_dash", segmentLength));
            var videoFMP4Muxing360p = bitmovin.Encoding.Encoding.Fmp4.Create(encoding.Id,
                CreateFMP4Muxing(videoStream360p, output, OUTPUT_PATH + "video/360p_dash", segmentLength));
            var videoFMP4Muxing480p = bitmovin.Encoding.Encoding.Fmp4.Create(encoding.Id,
                CreateFMP4Muxing(videoStream480p, output, OUTPUT_PATH + "video/480p_dash", segmentLength));
            var videoFMP4Muxing720p = bitmovin.Encoding.Encoding.Fmp4.Create(encoding.Id,
                CreateFMP4Muxing(videoStream720p, output, OUTPUT_PATH + "video/720p_dash", segmentLength));
            var videoFMP4Muxing1080p = bitmovin.Encoding.Encoding.Fmp4.Create(encoding.Id,
                CreateFMP4Muxing(videoStream1080p, output, OUTPUT_PATH + "video/1080p_dash", segmentLength));
            var audioFMP4Muxing = bitmovin.Encoding.Encoding.Fmp4.Create(encoding.Id,
                CreateFMP4Muxing(audioStream, output, OUTPUT_PATH + "audio/128kbps_dash", segmentLength));

            // Create DASH Manifest
            var manifestDash = bitmovin.Manifest.Dash.Create(new Dash
            {
                Name = "Manifest",
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
                    SegmentPath = "audio/128kbps_dash"
                });

            bitmovin.Manifest.Dash.Fmp4.Create(manifestDash.Id, period.Id, videoAdaptationSet.Id,
                new Manifest.Fmp4
                {
                    Type = SegmentScheme.TEMPLATE,
                    EncodingId = encoding.Id,
                    MuxingId = videoFMP4Muxing240p.Id,
                    SegmentPath = "video/240p_dash"
                });
            bitmovin.Manifest.Dash.Fmp4.Create(manifestDash.Id, period.Id, videoAdaptationSet.Id,
                new Manifest.Fmp4
                {
                    Type = SegmentScheme.TEMPLATE,
                    EncodingId = encoding.Id,
                    MuxingId = videoFMP4Muxing360p.Id,
                    SegmentPath = "video/360p_dash"
                });
            bitmovin.Manifest.Dash.Fmp4.Create(manifestDash.Id, period.Id, videoAdaptationSet.Id,
                new Manifest.Fmp4
                {
                    Type = SegmentScheme.TEMPLATE,
                    EncodingId = encoding.Id,
                    MuxingId = videoFMP4Muxing480p.Id,
                    SegmentPath = "video/480p_dash"
                });
            bitmovin.Manifest.Dash.Fmp4.Create(manifestDash.Id, period.Id, videoAdaptationSet.Id,
                new Manifest.Fmp4
                {
                    Type = SegmentScheme.TEMPLATE,
                    EncodingId = encoding.Id,
                    MuxingId = videoFMP4Muxing720p.Id,
                    SegmentPath = "video/720p_dash"
                });
            bitmovin.Manifest.Dash.Fmp4.Create(manifestDash.Id, period.Id, videoAdaptationSet.Id,
                new Manifest.Fmp4
                {
                    Type = SegmentScheme.TEMPLATE,
                    EncodingId = encoding.Id,
                    MuxingId = videoFMP4Muxing1080p.Id,
                    SegmentPath = "video/1080p_dash"
                });
            

            // Start encoding
            bitmovin.Encoding.Encoding.StartLive(encoding.Id, new StartLiveEncodingRequest
            {
                StreamKey = "bitcodin",
                HlsManifests = new List<LiveHlsManifest>
                {
                    new LiveHlsManifest
                    {
                        ManifestId = manifestHls.Id
                    }
                },
                DashManifests = new List<LiveDashManifest>
                {
                    new LiveDashManifest
                    {
                        ManifestId = manifestDash.Id
                    }
                }
            });

            LiveEncoding liveEncoding = null;

            while (liveEncoding == null)
            {
                try
                {
                    // Wait for the encoding to finish
                    liveEncoding = bitmovin.Encoding.Encoding.RetrieveLiveDetails(encoding.Id);
                }
                catch (System.Exception)
                {
                    Thread.Sleep(5000);
                }
            }

            Console.WriteLine("Live stream started");
            Console.WriteLine("Encoding ID: {0}", encoding.Id);
            Console.WriteLine("IP: {0}", liveEncoding.EncoderIp);
            Console.WriteLine("Rtmp URL: rtmp://{0}/live", liveEncoding.EncoderIp);
            Console.WriteLine("Stream Key: {0}", liveEncoding.StreamKey);
        }

        private static Ts CreateTsMuxing(Stream stream, BaseOutput output, string outputPath,
            double? segmentLength)
        {
            var encodingOutput = new Encoding.Output
            {
                OutputPath = outputPath,
                OutputId = output.Id,
                Acl = new List<Acl> {new Acl {Permission = Permission.PUBLIC_READ}}
            };

            var muxing = new Ts
            {
                Outputs = new List<Encoding.Output> {encodingOutput},
                Streams = new List<MuxingStream> {new MuxingStream {StreamId = stream.Id}},
                SegmentNaming = "segment_%number%.ts",
                SegmentLength = segmentLength
            };

            return muxing;
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

        private static Stream CreateStream(BaseInput input, string inputPath, int? position,
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