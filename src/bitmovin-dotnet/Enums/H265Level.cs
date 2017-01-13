using System.Runtime.Serialization;

namespace com.bitmovin.Api.Enums
{
    public enum H265Level
    {
        [EnumMember(Value = "1")] L1,
        [EnumMember(Value = "2")] L2,
        [EnumMember(Value = "2.1")] L2_1,
        [EnumMember(Value = "3")] L3,
        [EnumMember(Value = "3.1")] L3_1,
        [EnumMember(Value = "4")] L4,
        [EnumMember(Value = "4.1")] L4_1,
        [EnumMember(Value = "5")] L5,
        [EnumMember(Value = "5.1")] L5_1,
        [EnumMember(Value = "5.2")] L5_2,
        [EnumMember(Value = "6")] L6,
        [EnumMember(Value = "6.1")] L6_1,
        [EnumMember(Value = "6.2")] L6_2
    }
}