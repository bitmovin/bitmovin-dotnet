using com.bitmovin.Api.Enums;
using com.bitmovin.Api.Output;

namespace com.bitmovin.Api.Resource
{
    public class OutputResource<T> : AbstractListResource<T>
    {
        public OutputResource(RestClient client, string url) : base(client, url)
        {
        }

        public OutputType RetrieveType(string id)
        {
            var retrieveUrl = string.Format("{0}/{1}/type", _url, id);
            return _restClient.Get<OutputTypeContainer>(retrieveUrl).Type;
        }
    }
}