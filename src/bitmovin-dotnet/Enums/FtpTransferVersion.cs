using System.Runtime.Serialization;

namespace com.bitmovin.Api.Enums
{
    public enum FtpTransferVersion
    {
        [EnumMember(Value = "1.0.0")] V1_0_0,
        [EnumMember(Value = "1.1.0")] V1_1_1
    }
}