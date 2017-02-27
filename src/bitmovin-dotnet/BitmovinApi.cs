using com.bitmovin.Api.Container;

namespace com.bitmovin.Api
{
    public class BitmovinApi
    {
        private string _apiUrl;

        public string ApiKey { get; }

        private RestClient _restClient;

        public InputContainer Input;

        public OutputContainer Output;

        public CodecContainer Codec;

        public EncodingContainer Encoding;

        public ManifestContainer Manifest;

        public StatisticContainer Statistic;

        // Containers go here

        public BitmovinApi(string apiKey, string apiUrl = "https://api.bitmovin.com/v1/")
        {
            ApiKey = apiKey;
            _apiUrl = apiUrl;
            _restClient = new RestClient(apiKey, apiUrl);

            this.Input = new InputContainer(_restClient);
            this.Output = new OutputContainer(_restClient);
            this.Codec = new CodecContainer(_restClient);
            this.Encoding = new EncodingContainer(_restClient);
            this.Manifest = new ManifestContainer(_restClient);
            this.Statistic = new StatisticContainer(_restClient);
        }
    }
}