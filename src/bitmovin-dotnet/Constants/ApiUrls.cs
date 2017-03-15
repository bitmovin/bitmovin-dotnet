namespace com.bitmovin.Api.Constants
{
    public class ApiUrls
    {
        public const string AdminApiUrlPrefix = "api.bitmovin.com/admin/";
        public const string AdminUsers = "admin/users";

        public const string Inputs = "encoding/inputs";
        public const string InputsLimitOffset = "encoding/inputs?limit={limit}&offset={offset}";
        public const string InputHttp = "encoding/inputs/http";
        public const string InputHttps = "encoding/inputs/https";
        public const string InputS3 = "encoding/inputs/s3";
        public const string InputGcs = "encoding/inputs/gcs";
        public const string InputFtp = "encoding/inputs/ftp";
        public const string InputSftp = "encoding/inputs/sftp";
        public const string InputAzure = "encoding/inputs/azure";
        public const string InputAspera = "encoding/inputs/aspera";
        public const string InputRtmp = "encoding/inputs/rtmp";

        // public const string analysisStart = "encoding/inputs/{inputType}/{inputId}/analysis";
        // public const string analysisStatus = "encoding/inputs/{inputType}/{inputId}/analysis/{analysisId}/status";
        // public const string analysisDetails = "encoding/inputs/{inputType}/{inputId}/analysis/{analysisId}";

        public const string Outputs = "encoding/outputs";
        public const string OutputsLimitOffset = "encoding/outputs?limit={limit}&offset={offset}";
        public const string OutputGcs = "encoding/outputs/gcs";
        public const string OutputBitmovinGcp = "encoding/outputs/bitmovin/gcp";
        public const string OutputS3 = "encoding/outputs/s3";
        public const string OutputBitmovinAws = "encoding/outputs/bitmovin/aws";
        public const string OutputAzure = "encoding/outputs/azure";
        public const string OutputFtp = "encoding/outputs/ftp";
        public const string OutputSftp = "encoding/outputs/sftp";

        public const string CodecConfig = "encoding/configurations";
        public const string CodecConfigLimitOffset = "encoding/configurations?offset={offset}&limit={limit}";
        public const string CodecConfigH264 = "encoding/configurations/video/h264";
        public const string CodecConfigH265 = "encoding/configurations/video/h265";
        public const string CodecConfigVP9 = "encoding/configurations/video/vp9";
        public const string CodecConfigAac = "encoding/configurations/audio/aac";

        public const string Encodings = "encoding/encodings/";
        public const string Streams = "encoding/encodings/{0}/streams";
        public const string StreamsWithId = "encoding/encodings/{encoding_id}/streams/{stream_id}";
        public const string EncodingReport = "encoding/encodings/{encoding_id}/report";

        public const string AddFilterToStream = "encoding/encodings/{encoding_id}/streams/{stream_id}/filters";

        public const string FMP4Muxings = "encoding/encodings/{0}/muxings/fmp4";
        public const string TSMuxings = "encoding/encodings/{0}/muxings/ts";
        public const string MP4Muxings = "encoding/encodings/{0}/muxings/mp4";
        public const string WebmMuxings = "encoding/encodings/{0}/muxings/webm";

        public const string WidevineDrms = "encoding/encodings/{encoding_id}/muxings/fmp4/{muxing_id}/drm/widevine";
        public const string PlayReadyDrms = "encoding/encodings/{encoding_id}/muxings/fmp4/{muxing_id}/drm/playready";
        public const string PrimeTimeDrms = "encoding/encodings/{encoding_id}/muxings/fmp4/{muxing_id}/drm/primetime";
        public const string FairPlayFmp4Drms = "encoding/encodings/{encoding_id}/muxings/fmp4/{muxing_id}/drm/fairplay";
        public const string MarlinDrms = "encoding/encodings/{encoding_id}/muxings/fmp4/{muxing_id}/drm/marlin";
        public const string ClearKeyDrms = "encoding/encodings/{encoding_id}/muxings/fmp4/{muxing_id}/drm/clearkey";
        public const string CencDrms = "encoding/encodings/{encoding_id}/muxings/fmp4/{muxing_id}/drm/cenc";

        public const string FairPlayTsDrms = "encoding/encodings/{encoding_id}/muxings/ts/{muxing_id}/drm/fairplay";
        public const string AesEncryptionDrms = "encoding/encodings/{encoding_id}/muxings/ts/{muxing_id}/drm/aes";

        public const string AddWidevineDrmToFmp4Muxing =
            "encoding/encodings/{encoding_id}/muxings/fmp4/{muxing_id}/drm/widevine";

        public const string AddPlayReadyDrmToFmp4Muxing =
            "encoding/encodings/{encoding_id}/muxings/fmp4/{muxing_id}/drm/playready";

        public const string AddPrimeTimeDrmToFmp4Muxing =
            "encoding/encodings/{encoding_id}/muxings/fmp4/{muxing_id}/drm/primetime";

        public const string AddFairPlayDrmToFmp4Muxing =
            "encoding/encodings/{encoding_id}/muxings/fmp4/{muxing_id}/drm/fairplay";

        public const string AddMarlinDrmToFmp4Muxing =
            "encoding/encodings/{encoding_id}/muxings/fmp4/{muxing_id}/drm/marlin";

        public const string AddClearKeyDrmToFmp4Muxing =
            "encoding/encodings/{encoding_id}/muxings/fmp4/{muxing_id}/drm/clearkey";

        public const string AddCencDrmToFmp4Muxing =
            "encoding/encodings/{encoding_id}/muxings/fmp4/{muxing_id}/drm/cenc";

        public const string GetCencDrmToFmp4Muxing =
            "encoding/encodings/{encoding_id}/streams/{stream_id}/muxings/fmp4/{muxing_id}/drm/cenc/{cencDrmId}";

        public const string AddFairPlayDrmToTssMuxing =
            "encoding/encodings/{encoding_id}/muxings/ts/{muxing_id}/drm/fairplay";

        public const string AddAesEncryptionToTssMuxing =
            "encoding/encodings/{encoding_id}/muxings/ts/{muxing_id}/drm/aes";

        public const string AddPlayReadyDrmToMp4Muxing =
            "encoding/encodings/{encoding_id}/muxings/mp4/{muxing_id}/drm/playready";

        public const string EncodingsLimitOffset = "encoding/encodings?limit={limit}&offset={offset}";
        public const string EncodingStart = "encoding/encodings/{encoding_id}/start/";
        public const string EncodingDetailsLive = "encoding/encodings/{encoding_id}/live";
        public const string EncodingStartLive = "encoding/encodings/{encoding_id}/live/start";
        public const string EncodingStopLive = "encoding/encodings/{encoding_id}/live/stop";
        public const string EncodingStartRest = "encoding/encodings/{encoding_id}/startrest";
        public const string EncodingStop = "encoding/encodings/{encoding_id}/stop";
        public const string EncodingRestart = "encoding/encodings/{encoding_id}/restart";
        public const string EncodingStatus = "encoding/encodings/{encoding_id}/status";
        public const string EncodingDelete = "encoding/encodings/{encoding_id}";

        public const string Manifests = "encoding/manifests";
        public const string ManifestsLimitOffset = "encoding/manifests?limit={limit}&offset={offset}";

        public const string ManifestDash = "encoding/manifests/dash";
        public const string ManifestDashAddPeriod = "encoding/manifests/dash/{0}/periods";

        public const string ManifestDashAddVideoAdaptionSet =
            "encoding/manifests/dash/{0}/periods/{1}/adaptationsets/video";

        public const string ManifestDashAddAudioAdaptionSet =
            "encoding/manifests/dash/{0}/periods/{1}/adaptationsets/audio";

        public const string ManifestDashAddSubtitleAdaptionSet =
            "encoding/manifests/dash/{0}/periods/{1}/adaptationsets/subtitle";

        public const string ManifestDashAddCustomAdaptionSet =
            "encoding/manifests/dash/{0}/periods/{1}/adaptationsets/custom";

        public const string ManifestDashAddRepresentationFmp4 =
            "encoding/manifests/dash/{0}/periods/{1}/adaptationsets/{2}/representations/fmp4";

        public const string ManifestDashAddRepresentationWebm =
            "encoding/manifests/dash/{0}/periods/{1}/adaptationsets/{2}/representations/webm";

        public const string ManifestDashAddContentProtectionToAdaptationSet =
            "encoding/manifests/dash/{manifestId}/periods/{periodId}/adaptationsets/{adaptionsetId}/contentprotection";

        public const string ManifestDashAddContentProtectionTofMp4Representation =
                "encoding/manifests/dash/{manifestId}/periods/{periodId}/adaptationsets/{adaptionsetId}/representations/fmp4/{representationId}/contentprotection"
            ;

        public const string ManifestDashAddContentProtectionToDrmfMp4Representation =
                "encoding/manifests/dash/{manifestId}/periods/{periodId}/adaptationsets/{adaptionsetId}/representations/fmp4/drm/{representationId}/contentprotection"
            ;

        public const string ManifestDashStart = "encoding/manifests/dash/{manifestId}/start";
        public const string ManifestDashStatus = "encoding/manifests/dash/{manifestId}/status";

        public const string ManifestHls = "encoding/manifests/hls";
        public const string ManifestHlsMediaInfo = "encoding/manifests/hls/{manifestId}/media";
        public const string ManifestHlsStreamInfo = "encoding/manifests/hls/{manifestId}/streams";
        public const string ManifestHlsStart = "encoding/manifests/hls/{manifestId}/start";
        public const string ManifestHlsStatus = "encoding/manifests/hls/{manifestId}/status";

        public const string ManifestSmooth = "encoding/manifests/smooth";
        public const string ManifestSmoothStart = "encoding/manifests/smooth/{manifestId}/start";
        public const string ManifestSmoothStatus = "encoding/manifests/smooth/{manifestId}/status";
        public const string ManifestSmoothRepresentation = "encoding/manifests/smooth/{manifestId}/representations/mp4";

        public const string ManifestSmoothContentProtection =
            "encoding/manifests/smooth/{manifestId}/contentprotection";

        public const string FiltersLimitOffset = "encoding/filters?offset={offset}&limit={limit}";
        public const string Filters = "encoding/filters";
        public const string FilterCrop = "encoding/filters/crop";
        public const string FilterRotate = "encoding/filters/rotate";
        public const string FilterWatermark = "encoding/filters/watermark";
        public const string FilterDeinterlace = "encoding/filters/deinterlace";

        public const string CustomDataSuffix = "/customData";

        public const string Thumbnails = "encoding/encodings/{encoding_id}/streams/{stream_id}/thumbnails";
        public const string Sprites = "encoding/encodings/{encoding_id}/streams/{stream_id}/sprites";

        public const string TransferEncoding = "encoding/transfers/encoding";
        public const string TransferEncodingStatus = "encoding/transfers/encoding/{transfer_id}/status";
        public const string TransferEncodingDetails = "encoding/transfers/encoding/{transfer_id}";

        public const string TransferManifest = "encoding/transfers/manifest";
        public const string TransferManifestStatus = "encoding/transfers/manifest/{transfer_id}/status";
        public const string TransferManifestDetails = "encoding/transfers/manifest/{transfer_id}";

        public const string StorageUrl = "storage";
        public const string StorageFolders = "storage/folders";
        public const string StorageFiles = "storage/files";
        public const string StorageStatistics = "storage/statistics";

        public const string AccountOrganization = "account/organizations";

        public const string PlayerLicenseKeys = "player/licenses/{playerLicenseId}";
        public const string AnalyticsLicenseKeys = "analytics/licenses/{analyticsLicenseId}";
        public const string AnalyticsQuery = "analytics/queries";

        public const string StatisticsLiveEncdoing = "encoding/statistics/encodings/{encoding_id}/live-statistics";

    }
}