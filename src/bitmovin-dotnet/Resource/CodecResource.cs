using com.bitmovin.Api.Codec;
using com.bitmovin.Api.Enums;
using com.bitmovin.Api.Output;

namespace com.bitmovin.Api.Resource
{
    public class CodecResource<T> : AbstractListResource<T>
    {
        public CodecResource(RestClient client, string url) : base(client, url)
        {
        }

        public CodecType? RetrieveType(string id)
        {
            var retrieveUrl = string.Format("{0}/{1}/type", _url, id);
            return _restClient.Get<CodecTypeContainer>(retrieveUrl).Type;
        }

        public bool IsVideoCodec(string id)
        {
            switch (RetrieveType(id))
            {
                case CodecType.H264:
                case CodecType.H265:
                    return true;
            }
            return false;
        }

        public bool IsAudioCodec(string id)
        {
            switch (RetrieveType(id))
            {
                case CodecType.AAC:
                    return true;
            }
            return false;
        }

    }
}