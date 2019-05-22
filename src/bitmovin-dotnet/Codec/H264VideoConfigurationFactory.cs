using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.bitmovin.Api.Enums;

namespace com.bitmovin.Api.Codec
{
    public class H264VideoConfigurationFactory
    {

        public static H264VideoConfiguration CreateConfigurationWithPreset(H264Preset preset, string name)
        {
            switch (preset)
            {
                case H264Preset.ULTRAFAST:
                    return new H264VideoConfiguration
                    {
                        Name = name,
                        BAdaptiveStrategy = BAdapt.NONE,
                        BFrames = 0,
                        MvSearchRangeMax = 16,
                        MvPredictionMode = MvPredictionMode.SPATIAL,
                        MotionEstimationMethod = H264MotionEstimationMethod.DIA,
                        Cabac = false,
                        RcLookahead = 0,
                        RefFrames = 1,
                        SubMe = H264SubMe.FULLPEL,
                        Trellis = H264Trellis.DISABLED,
                        Partitions = new List<H264Partition> { H264Partition.NONE }
                    };
                case H264Preset.SUPERFAST:
                    return new H264VideoConfiguration
                    {
                        Name = name,
                        BAdaptiveStrategy = BAdapt.FAST,
                        BFrames = 3,
                        MvSearchRangeMax = 16,
                        MvPredictionMode = MvPredictionMode.SPATIAL,
                        MotionEstimationMethod = H264MotionEstimationMethod.DIA,
                        Cabac = true,
                        RcLookahead = 0,
                        RefFrames = 1,
                        SubMe = H264SubMe.SAD,
                        Trellis = H264Trellis.ENABLED_FINAL_MB,
                        Partitions = new List<H264Partition> { H264Partition.I4X4, H264Partition.I8X8 }
                    };
                case H264Preset.VERYFAST:
                    return new H264VideoConfiguration
                    {
                        Name = name,
                        BAdaptiveStrategy = BAdapt.FAST,
                        BFrames = 3,
                        MvSearchRangeMax = 16,
                        MvPredictionMode = MvPredictionMode.SPATIAL,
                        MotionEstimationMethod = H264MotionEstimationMethod.HEX,
                        Cabac = true,
                        RcLookahead = 10,
                        RefFrames = 1,
                        SubMe = H264SubMe.SATD,
                        Trellis = H264Trellis.ENABLED_FINAL_MB,
                        Partitions = new List<H264Partition> { H264Partition.I4X4, H264Partition.I8X8, H264Partition.P8X8, H264Partition.B8X8 }
                    };
                case H264Preset.FASTER:
                    return new H264VideoConfiguration
                    {
                        Name = name,
                        BAdaptiveStrategy = BAdapt.FAST,
                        BFrames = 3,
                        MvSearchRangeMax = 16,
                        MvPredictionMode = MvPredictionMode.SPATIAL,
                        MotionEstimationMethod = H264MotionEstimationMethod.HEX,
                        Cabac = true,
                        RcLookahead = 20,
                        RefFrames = 2,
                        SubMe = H264SubMe.QPEL4,
                        Trellis = H264Trellis.ENABLED_FINAL_MB,
                        Partitions = new List<H264Partition> { H264Partition.I4X4, H264Partition.I8X8, H264Partition.P8X8, H264Partition.B8X8 }
                    };
                case H264Preset.FAST:
                    return new H264VideoConfiguration
                    {
                        Name = name,
                        BAdaptiveStrategy = BAdapt.FAST,
                        BFrames = 3,
                        MvSearchRangeMax = 16,
                        MvPredictionMode = MvPredictionMode.SPATIAL,
                        MotionEstimationMethod = H264MotionEstimationMethod.HEX,
                        Cabac = true,
                        RcLookahead = 30,
                        RefFrames = 2,
                        SubMe = H264SubMe.RD_IP,
                        Trellis = H264Trellis.ENABLED_FINAL_MB,
                        Partitions = new List<H264Partition> { H264Partition.I4X4, H264Partition.I8X8, H264Partition.P8X8, H264Partition.B8X8 }
                    };
                case H264Preset.MEDIUM:
                    return new H264VideoConfiguration
                    {
                        Name = name,
                        BAdaptiveStrategy = BAdapt.FAST,
                        BFrames = 3,
                        MvSearchRangeMax = 16,
                        MvPredictionMode = MvPredictionMode.SPATIAL,
                        MotionEstimationMethod = H264MotionEstimationMethod.HEX,
                        Cabac = true,
                        RcLookahead = 40,
                        RefFrames = 3,
                        SubMe = H264SubMe.RD_ALL,
                        Trellis = H264Trellis.ENABLED_ALL,
                        Partitions = new List<H264Partition> { H264Partition.I4X4, H264Partition.I8X8, H264Partition.P8X8, H264Partition.B8X8 }
                    };
                case H264Preset.SLOW:
                    return new H264VideoConfiguration
                    {
                        Name = name,
                        BAdaptiveStrategy = BAdapt.FULL,
                        BFrames = 3,
                        MvSearchRangeMax = 16,
                        MvPredictionMode = MvPredictionMode.AUTO,
                        MotionEstimationMethod = H264MotionEstimationMethod.UMH,
                        Cabac = true,
                        RcLookahead = 50,
                        RefFrames = 5,
                        SubMe = H264SubMe.RD_REF_IP,
                        Trellis = H264Trellis.ENABLED_ALL,
                        Partitions = new List<H264Partition> { H264Partition.ALL }
                    };
                case H264Preset.SLOWER:
                    return new H264VideoConfiguration
                    {
                        Name = name,
                        BAdaptiveStrategy = BAdapt.FULL,
                        BFrames = 3,
                        MvSearchRangeMax = 16,
                        MvPredictionMode = MvPredictionMode.AUTO,
                        MotionEstimationMethod = H264MotionEstimationMethod.UMH,
                        Cabac = true,
                        RcLookahead = 60,
                        RefFrames = 8,
                        SubMe = H264SubMe.RD_REF_ALL,
                        Trellis = H264Trellis.ENABLED_ALL,
                        Partitions = new List<H264Partition> { H264Partition.ALL }
                    };
                case H264Preset.VERYSLOW:
                    return new H264VideoConfiguration
                    {
                        Name = name,
                        BAdaptiveStrategy = BAdapt.FULL,
                        BFrames = 8,
                        MvSearchRangeMax = 24,
                        MvPredictionMode = MvPredictionMode.AUTO,
                        MotionEstimationMethod = H264MotionEstimationMethod.UMH,
                        Cabac = true,
                        RcLookahead = 60,
                        RefFrames = 16,
                        SubMe = H264SubMe.RD_REF_ALL,
                        Trellis = H264Trellis.ENABLED_ALL,
                        Partitions = new List<H264Partition> { H264Partition.ALL }
                    };
                case H264Preset.PLACEBO:
                    return new H264VideoConfiguration
                    {
                        Name = name,
                        BAdaptiveStrategy = BAdapt.FULL,
                        BFrames = 16,
                        MvSearchRangeMax = 24,
                        MvPredictionMode = MvPredictionMode.AUTO,
                        MotionEstimationMethod = H264MotionEstimationMethod.UMH,
                        Cabac = true,
                        RcLookahead = 60,
                        RefFrames = 16,
                        SubMe = H264SubMe.RD_REF_ALL,
                        Trellis = H264Trellis.ENABLED_ALL,
                        Partitions = new List<H264Partition> { H264Partition.ALL }
                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(preset), preset, null);
            }
        }

    }
}
