using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using com.bitmovin.Api.Enums;
using com.bitmovin.Api.Manifest;
using com.bitmovin.Api.Output;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.bitmovin.Api.Examples
{

    [TestClass]
    public class CreateHlsVodManifestFromLiveStream
    {
        private const string API_KEY = "YOUR API KEY";
        private const string ENCODING_ID = "ENCODING ID FROM LIVE ENCODING";
        private const string GCS_ACCESS_KEY = "GCS ACCESS KEY";
        private const string GCS_SECRET_KEY = "GCS SECRET KEY";
        private const string GCS_BUCKET_NAME = "GCS BUCKET NAME";
        private const string OUTPUT_PATH = "path/to/output/";
        private const string MANIFEST_NAME = "stream_vod.m3u8";

        // If START_SEGMENT is set to null the first segment is taken as START_SEGMENT
        private readonly int? START_SEGMENT = 2;
        // If END_SEGMENT is set to null the last segment is taken as END_SEGMENT
        private readonly int? END_SEGMENT = 10;

        [TestMethod]
        public void RunExample()
        {
            var bitmovin = new BitmovinApi(API_KEY);

            var output = bitmovin.Output.Gcs.Create(new GcsOutput
            {
                Name = "GCS Ouput",
                AccessKey = GCS_ACCESS_KEY,
                SecretKey = GCS_SECRET_KEY,
                BucketName = GCS_BUCKET_NAME
            });

            var muxings = bitmovin.Encoding.Encoding.Ts.RetrieveList(ENCODING_ID, 0, 50);
            var audioMuxing = muxings.First(c =>
            {
                var stream = bitmovin.Encoding.Encoding.Stream.Retrieve(ENCODING_ID, c.Streams.First().StreamId);
                return bitmovin.Codec.Codec.IsAudioCodec(stream.CodecConfigId);
            });
            var videoMuxings = muxings.Where(c =>
            {
                var stream = bitmovin.Encoding.Encoding.Stream.Retrieve(ENCODING_ID, c.Streams.First().StreamId);
                return bitmovin.Codec.Codec.IsVideoCodec(stream.CodecConfigId);
            }).ToList();

            var manifest = bitmovin.Manifest.Hls.Create(new Hls
            {
                Name = MANIFEST_NAME,
                ManifestName = MANIFEST_NAME,
                Outputs = new List<Encoding.Output>
                {
                    new Encoding.Output
                    {
                        OutputId = output.Id,
                        OutputPath = OUTPUT_PATH
                    }
                }
            });

            var audioManifestName = String.Format("audio_{0}.m3u8", Guid.NewGuid());
            var mediaInfo = new MediaInfo
            {
                GroupId = "audio",
                Name = audioManifestName,
                Uri = audioManifestName,
                Type = MediaType.AUDIO,
                SegmentPath = GetSegmentPath(audioMuxing.Outputs.First().OutputPath),
                StreamId = audioMuxing.Streams.First().StreamId,
                MuxingId = audioMuxing.Id,
                EncodingId = ENCODING_ID,
                StartSegmentNumber = START_SEGMENT,
                EndSegmentNumber = END_SEGMENT,
                Language = "en",
                AssocLanguage = "en",
                Autoselect = false,
                IsDefault = false,
                Forced = false
            };

            bitmovin.Manifest.Hls.AddMediaInfo(manifest.Id, mediaInfo);


            foreach (var videoMuxing in videoMuxings)
            {
                bitmovin.Manifest.Hls.AddStreamInfo(manifest.Id, new StreamInfo
                {
                    Uri = String.Format("video_{0}.m3u8", Guid.NewGuid()),
                    EncodingId = ENCODING_ID,
                    StreamId = videoMuxing.Streams.First().StreamId,
                    MuxingId = videoMuxing.Id,
                    StartSegmentNumber = START_SEGMENT,
                    EndSegmentNumber = END_SEGMENT,
                    Audio = "audio",
                    SegmentPath = GetSegmentPath(videoMuxing.Outputs.First().OutputPath)
                });
            }


            Console.WriteLine("Start creating hls manifest");
            bitmovin.Manifest.Hls.Start(manifest.Id);

            Console.WriteLine("Manifest creation started");
            var status = bitmovin.Manifest.Hls.RetrieveStatus(manifest.Id);
            while (status.Status == Status.RUNNING)
            {
                status = bitmovin.Manifest.Hls.RetrieveStatus(manifest.Id);
                Thread.Sleep(2500);
            }
            Console.WriteLine("Manifest created successfully");
        }

        private static string GetSegmentPath(string outputPath)
        {
            var s = outputPath.Replace(OUTPUT_PATH, "");
            while (s.StartsWith("/"))
            {
                s = s.Substring(1);
            }
            return s;
        }

    }
}