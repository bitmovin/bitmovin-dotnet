using System.Collections.Generic;
using com.bitmovin.Api.Rest;

namespace com.bitmovin.Api.Input
{
    public class AnalysisDetail : BaseObject
    {
        public List<VideoStream> VideoStreams { get; set; }

        public List<AudioStream> AudioStreams { get; set; }

        public List<MetaStream> MetaStreams { get; set; }

        public List<SubtitleStream> SubtitleStreams { get; set; }

        public object Metadata { get; set; }

        public List<Message> Messages { get; set; }
    }

    public class VideoStream
    {
        public string Id { get; set; }

        public int? Position { get; set; }

        public double? Duration { get; set; }

        public string Codec { get; set; }

        public double? Fps { get; set; }

        public int? Bitrate { get; set; }

        public int? Width { get; set; }

        public int? Height { get; set; }
    }

    public class AudioStream
    {
        public string Id { get; set; }

        public int? Position { get; set; }

        public double? Duration { get; set; }

        public string Codec { get; set; }

        public int? SampleRate { get; set; }

        public string ChannelFormat { get; set; }

        public string Language { get; set; }

        public bool? HearingImpaired { get; set; }
    }

    public class MetaStream
    {
        public string Id { get; set; }

        public int? Position { get; set; }

        public double? Duration { get; set; }

        public string Codec { get; set; }
    }

    public class SubtitleStream
    {
        public string Id { get; set; }

        public int? Position { get; set; }

        public double? Duration { get; set; }

        public string Codec { get; set; }

        public string Language { get; set; }

        public bool? HearingImpaired { get; set; }
    }
}