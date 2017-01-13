using System.Runtime.Serialization;

namespace com.bitmovin.Api.Enums
{
    public enum ChannelLayout
    {
        [EnumMember(Value = "NONE")] CL_NONE,
        [EnumMember(Value = "MONO")] CL_MONO,
        [EnumMember(Value = "STEREO")] CL_STEREO,
        [EnumMember(Value = "2.1")] CL_2_1,
        [EnumMember(Value = "SURROUND")] CL_SURROUND,
        [EnumMember(Value = "3.1")] CL_3_1,
        [EnumMember(Value = "4.0")] CL_4_0,
        [EnumMember(Value = "4.1")] CL_4_1,
        [EnumMember(Value = "2.2")] CL_2_2,
        [EnumMember(Value = "QUAD")] CL_QUAD,
        [EnumMember(Value = "5.0")] CL_5_0,
        [EnumMember(Value = "5.1")] CL_5_1,
        [EnumMember(Value = "5.0_BACK")] CL_5_0_BACK,
        [EnumMember(Value = "5.1_BACK")] CL_5_1_BACK,
        [EnumMember(Value = "6.0")] CL_6_0,
        [EnumMember(Value = "6.0_FRONT")] CL_6_0_FRONT,
        [EnumMember(Value = "HEXAGONAL")] CL_HEXAGONAL,
        [EnumMember(Value = "6.1")] CL_6_1,
        [EnumMember(Value = "6.1_FRONT")] CL_6_1_FRONT,
        [EnumMember(Value = "6.1_BACK")] CL_6_1_BACK,
        [EnumMember(Value = "7.0")] CL_7_0,
        [EnumMember(Value = "7.0_FRONT")] CL_7_0_FRONT,
        [EnumMember(Value = "7.1")] CL_7_1,
        [EnumMember(Value = "7.1_WIDE")] CL_7_1_WIDE,
        [EnumMember(Value = "7.1_WIDE_BACK")] CL_7_1_WIDE_BACK,
        [EnumMember(Value = "OCTOGONAL")] CL_OCTOGONAL,
        [EnumMember(Value = "STEREO_DOWNMID")] CL_STEREO_DOWNMIX
    }
}