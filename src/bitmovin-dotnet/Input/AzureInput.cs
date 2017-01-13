namespace com.bitmovin.Api.Input
{
    public class AzureInput : BaseObject
    {
        public string CreatedAt { get; set; }

        public string UpdatedAt { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string AccountName { get; set; }

        public string AccountKey { get; set; }

        public string Container { get; set; }
    }
}