using System.Runtime.Serialization;

namespace com.bitmovin.Api.Enums
{
    public enum H264Level
    {
        [EnumMember(Value = "1")] L1,
        [EnumMember(Value = "1b")] L1b,
        [EnumMember(Value = "1.1")] L1_1,
        [EnumMember(Value = "1.2")] L1_2,
        [EnumMember(Value = "1.3")] L1_3,
        [EnumMember(Value = "2")] L2,
        [EnumMember(Value = "2.1")] L2_1,
        [EnumMember(Value = "2.2")] L2_2,
        [EnumMember(Value = "3")] L3,
        [EnumMember(Value = "3.1")] L3_1,
        [EnumMember(Value = "3.2")] L3_2,
        [EnumMember(Value = "4")] L4,
        [EnumMember(Value = "4.1")] L4_1,
        [EnumMember(Value = "4.2")] L4_2,
        [EnumMember(Value = "5")] L5,
        [EnumMember(Value = "5.1")] L5_1,
        [EnumMember(Value = "5.2")] L5_2
    }
}