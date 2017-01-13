namespace com.bitmovin.Api.Resource
{
    public class PeriodResource<T> : AbstractOneEmbeddedResource<T>
    {
        public PeriodResource(RestClient client, string url) : base(client, url)
        {
            //Add all the adaptation sets here, separate endpoints
        }
    }
}