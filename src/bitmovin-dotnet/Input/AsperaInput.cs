namespace com.bitmovin.Api.Input
{
    public class AsperaInput : BaseObject
    {
        public string CreatedAt { get; set; }

        public string UpdatedAt { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Host { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string MinBandwidth { get; set; }

        public string MaxBandwidth { get; set; }
    }
}
