using com.bitmovin.Api.Constants;
using com.bitmovin.Api.Input;
using com.bitmovin.Api.Resource;

namespace com.bitmovin.Api.Container
{
    public class StatisticContainer
    {
        public LiveStatisticResource LiveStatistic { get; private set; }

        public StatisticContainer(RestClient client)
        {
            this.LiveStatistic = new LiveStatisticResource(client, ApiUrls.StatisticsLiveEncdoing);
        }
    }
}