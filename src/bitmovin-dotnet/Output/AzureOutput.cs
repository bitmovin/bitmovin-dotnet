namespace com.bitmovin.Api.Output
{
    public class AzureOutput : BaseOutput
    {
        public string AccountName { get; set; }

        public string AccountKey { get; set; }

        public string Container { get; set; }
    }
}