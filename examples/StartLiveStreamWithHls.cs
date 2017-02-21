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

namespace com.bitmovin.Api.Examples
{

    [TestClass]
    public class StartLiveStreamWithHls
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

            var output = bitmovin.Output.Gcs.Create(new GcsOutput
            {
                Name = "GCS Ouput",
                AccessKey = GCS_ACCESS_KEY,
                SecretKey = GCS_SECRET_KEY,
                BucketName = GCS_BUCKET_NAME
            });

            var encoding = bitmovin.Encoding.Encoding.Create(new Encoding.Encoding
            {
                Name = "Live Stream C#",
                CloudRegion = EncodingCloudRegion.GOOGLE_EUROPE_WEST_1,
                EncoderVersion = "STABLE"
            });


            var rtmpInput = bitmovin.Input.Rtmp.RetrieveList(0, 100)[0];

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

            var videoMuxing240p = bitmovin.Encoding.Encoding.Ts.Create(encoding.Id,
                CreateTsMuxing(videoStream240p, output, OUTPUT_PATH + "video/240p", segmentLength));
            var videoMuxing360p = bitmovin.Encoding.Encoding.Ts.Create(encoding.Id,
                CreateTsMuxing(videoStream360p, output, OUTPUT_PATH + "video/360p", segmentLength));
            var videoMuxing480p = bitmovin.Encoding.Encoding.Ts.Create(encoding.Id,
                CreateTsMuxing(videoStream480p, output, OUTPUT_PATH + "video/480p", segmentLength));
            var videoMuxing720p = bitmovin.Encoding.Encoding.Ts.Create(encoding.Id,
                CreateTsMuxing(videoStream720p, output, OUTPUT_PATH + "video/720p", segmentLength));
            var videoMuxing1080p = bitmovin.Encoding.Encoding.Ts.Create(encoding.Id,
                CreateTsMuxing(videoStream1080p, output, OUTPUT_PATH + "video/1080p", segmentLength));
            var audioMuxing = bitmovin.Encoding.Encoding.Ts.Create(encoding.Id,
                CreateTsMuxing(audioStream, output, OUTPUT_PATH + "audio/128kbps", segmentLength));


            var manifestOutput = new Encoding.Output
            {
                OutputPath = OUTPUT_PATH,
                OutputId = output.Id,
                Acl = new List<Acl> {new Acl {Permission = Permission.PUBLIC_READ}}
            };
            var manifest = bitmovin.Manifest.Hls.Create(new Hls
            {
                Name = "HLS Manifest",
                ManifestName = "stream.m3u8",
                Outputs = new List<Encoding.Output> {manifestOutput}
            });

            var mediaInfo = new MediaInfo
            {
                GroupId = "audio",
                Name = "English",
                Uri = "audio.m3u8",
                Type = MediaType.AUDIO,
                SegmentPath = "audio/128kbps/",
                StreamId = audioStream.Id,
                MuxingId = audioMuxing.Id,
                EncodingId = encoding.Id,
                Language = "en",
                AssocLanguage = "en",
                Autoselect = false,
                IsDefault = false,
                Forced = false
            };

            bitmovin.Manifest.Hls.AddMediaInfo(manifest.Id, mediaInfo);

            bitmovin.Manifest.Hls.AddStreamInfo(manifest.Id, new StreamInfo
            {
                Uri = "video_240.m3u8",
                EncodingId = encoding.Id,
                StreamId = videoStream240p.Id,
                MuxingId = videoMuxing240p.Id,
                Audio = "audio",
                SegmentPath = "video/240p/"
            });
            bitmovin.Manifest.Hls.AddStreamInfo(manifest.Id, new StreamInfo
            {
                Uri = "video_360.m3u8",
                EncodingId = encoding.Id,
                StreamId = videoStream360p.Id,
                MuxingId = videoMuxing360p.Id,
                Audio = "audio",
                SegmentPath = "video/360p/"
            });
            bitmovin.Manifest.Hls.AddStreamInfo(manifest.Id, new StreamInfo
            {
                Uri = "video_480.m3u8",
                EncodingId = encoding.Id,
                StreamId = videoStream480p.Id,
                MuxingId = videoMuxing480p.Id,
                Audio = "audio",
                SegmentPath = "video/480p/"
            });
            bitmovin.Manifest.Hls.AddStreamInfo(manifest.Id, new StreamInfo
            {
                Uri = "video_720.m3u8",
                EncodingId = encoding.Id,
                StreamId = videoStream720p.Id,
                MuxingId = videoMuxing720p.Id,
                Audio = "audio",
                SegmentPath = "video/720p/"
            });
            bitmovin.Manifest.Hls.AddStreamInfo(manifest.Id, new StreamInfo
            {
                Uri = "video_1080.m3u8",
                EncodingId = encoding.Id,
                StreamId = videoStream1080p.Id,
                MuxingId = videoMuxing1080p.Id,
                Audio = "audio",
                SegmentPath = "video/1080p/"
            });

            bitmovin.Encoding.Encoding.StartLive(encoding.Id, new StartLiveEncodingRequest
            {
                StreamKey = "YourStreamKey",
                HlsManifests = new List<LiveHlsManifest>
                {
                    new LiveHlsManifest
                    {
                        ManifestId = manifest.Id,
                        Timeshift = 300
                    }
                }
            });

            LiveEncoding liveEncoding = null;

            while (liveEncoding == null)
            {
                try
                {
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