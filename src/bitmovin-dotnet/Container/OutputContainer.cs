using com.bitmovin.Api.Constants;
using com.bitmovin.Api.Output;
using com.bitmovin.Api.Resource;

namespace com.bitmovin.Api.Container
{
    public class OutputContainer
    {
        public AbstractResource<S3Output> S3 { get; private set; }

        public OutputResource<BaseOutput> Output { get; private set; }

        public AbstractResource<GcsOutput> Gcs { get; private set; }

        public AbstractResource<AzureOutput> Azure { get; private set; }

        public AbstractResource<FtpOutput> Ftp { get; private set; }

        public AbstractResource<SftpOutput> Sftp { get; private set; }

        public CloudOutputResource<BitmovinAws> BitmovinAws { get; private set; }

        public CloudOutputResource<BitmovinGcp> BitmovinGcp { get; private set; }

        public OutputContainer(RestClient client)
        {
            this.S3 = new AbstractResource<S3Output>(client, ApiUrls.OutputS3);
            this.Output = new OutputResource<BaseOutput>(client, ApiUrls.Outputs);
            this.Gcs = new AbstractResource<GcsOutput>(client, ApiUrls.OutputGcs);
            this.Azure = new AbstractResource<AzureOutput>(client, ApiUrls.OutputAzure);
            this.Ftp = new AbstractResource<FtpOutput>(client, ApiUrls.OutputFtp);
            this.Sftp = new AbstractResource<SftpOutput>(client, ApiUrls.OutputSftp);
            this.BitmovinAws = new CloudOutputResource<BitmovinAws>(client, ApiUrls.OutputBitmovinAws);
            this.BitmovinGcp = new CloudOutputResource<BitmovinGcp>(client, ApiUrls.OutputBitmovinGcp);
        }
    }
}