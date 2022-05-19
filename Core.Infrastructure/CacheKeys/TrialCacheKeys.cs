namespace Signaturit.Infrastructure.CacheKeys
{
    public static class TrialCacheKeys
    {
        public static string ListKey => "TrialList";

        public static string SelectListKey => "TrialSelectList";

        public static string GetKey(int trialId) => $"Trial-{trialId}";

        public static string GetDetailsKey(int trialId) => $"TrialDetails-{trialId}";
    }
}