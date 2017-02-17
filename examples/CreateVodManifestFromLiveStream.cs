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
    public class CreateVodManifestFromLiveStream
    {
        private const string API_KEY = "YOUR API KEY";
        private const string ENCODING_ID = "ENCODING ID FROM LIVE ENCODING";
        private const string GCS_ACCESS_KEY = "GCS ACCESS KEY";
        private const string GCS_SECRET_KEY = "GCS SECRET KEY";
        private const string GCS_BUCKET_NAME = "GCS BUCKET NAME";
        private const string OUTPUT_PATH = "path/to/output/";

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

            var muxings = bitmovin.Encoding.Encoding.Fmp4.RetrieveList(ENCODING_ID, 0, 50);
            var audioMuxing = muxings.First(c =>
            {
                var stream = bitmovin.Encoding.Encoding.Stream.Retrieve(ENCODING_ID, c.Streams.First().StreamId);
                var configType = bitmovin.Codec.Codec.RetrieveType(stream.CodecConfigId);
                return configType == CodecType.AAC;
            });
            var videoMuxings = muxings.Where(c =>
            {
                var stream = bitmovin.Encoding.Encoding.Stream.Retrieve(ENCODING_ID, c.Streams.First().StreamId);
                var configType = bitmovin.Codec.Codec.RetrieveType(stream.CodecConfigId);
                return configType == CodecType.H264;
            }).ToList();

            var manifest = bitmovin.Manifest.Dash.Create(new Dash
            {
                Name = "Dash Manifest",
                ManifestName = "stream_vod_dotnet_3.mpd",
                Outputs = new List<Encoding.Output>
                {
                    new Encoding.Output
                    {
                        OutputId = output.Id,
                        OutputPath = OUTPUT_PATH
                    }
                }
            });
            var period = bitmovin.Manifest.Dash.Period.Create(manifest.Id, new Period());
            var audioAdaptationSet = bitmovin.Manifest.Dash.AudioAdaptationSet.Create(manifest.Id, period.Id,
                new AudioAdaptationSet
                {
                    Lang = "en"
                });
            var videoAdaptationSet = bitmovin.Manifest.Dash.VideoAdaptationSet.Create(manifest.Id, period.Id,
                new VideoAdaptationSet());

            bitmovin.Manifest.Dash.Fmp4.Create(manifest.Id, period.Id, audioAdaptationSet.Id, new Fmp4
            {
                Type = SegmentScheme.TEMPLATE,
                EncodingId = ENCODING_ID,
                MuxingId = audioMuxing.Id,
                StartSegmentNumber = START_SEGMENT,
                EndSegmentNumber = END_SEGMENT,
                SegmentPath = GetSegmentPath(audioMuxing.Outputs.First().OutputPath)
            });

            foreach (var videoMuxing in videoMuxings)
            {
                bitmovin.Manifest.Dash.Fmp4.Create(manifest.Id, period.Id, videoAdaptationSet.Id, new Fmp4
                {
                    Type = SegmentScheme.TEMPLATE,
                    EncodingId = ENCODING_ID,
                    MuxingId = videoMuxing.Id,
                    StartSegmentNumber = START_SEGMENT,
                    EndSegmentNumber = END_SEGMENT,
                    SegmentPath = GetSegmentPath(videoMuxing.Outputs.First().OutputPath)
                });
            }

            Console.WriteLine("Start creating dash manifest");
            bitmovin.Manifest.Dash.Start(manifest.Id);

            Console.WriteLine("Manifest creation started");
            var status = bitmovin.Manifest.Dash.RetrieveStatus(manifest.Id);
            while (status.Status == Status.RUNNING)
            {
                status = bitmovin.Manifest.Dash.RetrieveStatus(manifest.Id);
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