namespace com.bitmovin.Api.Output
{
    public class SftpOutput : BaseOutput
    {
        public string Host { get; set; }

        public int? Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}