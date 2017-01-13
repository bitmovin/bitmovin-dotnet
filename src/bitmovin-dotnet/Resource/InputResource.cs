using com.bitmovin.Api.Enums;
using com.bitmovin.Api.Input;

namespace com.bitmovin.Api.Resource
{
    public class InputResource<T> : AbstractListResource<T>
    {
        public InputResource(RestClient client, string url) : base(client, url)
        {
        }

        public InputType RetrieveType(string id)
        {
            var retrieveUrl = string.Format("{0}/{1}/type", _url, id);
            return _restClient.Get<InputTypeContainer>(retrieveUrl).Type;
        }
    }
}