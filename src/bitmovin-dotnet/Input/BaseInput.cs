namespace com.bitmovin.Api.Input
{
    public class BaseInput : BaseObject
    {
        public string CreatedAt { get; set; }

        public string UpdatedAt { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}