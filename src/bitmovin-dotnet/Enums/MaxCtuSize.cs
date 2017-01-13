using System.Runtime.Serialization;

namespace com.bitmovin.Api.Enums
{
    public enum MaxCtuSize
    {
        [EnumMember(Value = "64")] S64,
        [EnumMember(Value = "16")] S16,
        [EnumMember(Value = "32")] S32
    }
}