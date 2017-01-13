using com.bitmovin.Api.Constants;
using com.bitmovin.Api.Input;
using com.bitmovin.Api.Resource;

namespace com.bitmovin.Api.Container
{
    public class InputContainer
    {
        public AbstractResource<AzureInput> Aspera { get; private set; }
        public AbstractResource<HttpInput> Http { get; private set; }
        public AbstractResource<HttpsInput> Https { get; private set; }
        public InputResource<BaseInput> Input { get; private set; }
        public AbstractResource<S3Input> S3 { get; private set; }
        public AbstractResource<GcsInput> Gcs { get; private set; }
        public AbstractResource<FtpInput> Ftp { get; private set; }
        public AbstractResource<SftpInput> Sftp { get; private set; }
        public RtmpInputResource<RtmpInput> Rtmp { get; private set; }

        public InputContainer(RestClient client)
        {
            this.Aspera = new AbstractResource<AzureInput>(client, ApiUrls.InputAzure);
            this.Http = new AbstractResource<HttpInput>(client, ApiUrls.InputHttp);
            this.Https = new AbstractResource<HttpsInput>(client, ApiUrls.InputHttps);
            this.Input = new InputResource<BaseInput>(client, ApiUrls.Inputs);
            this.S3 = new AbstractResource<S3Input>(client, ApiUrls.InputS3);
            this.Gcs = new AbstractResource<GcsInput>(client, ApiUrls.InputGcs);
            this.Ftp = new AbstractResource<FtpInput>(client, ApiUrls.InputFtp);
            this.Sftp = new AbstractResource<SftpInput>(client, ApiUrls.InputSftp);
            this.Rtmp = new RtmpInputResource<RtmpInput>(client, ApiUrls.InputRtmp);
        }
    }
}