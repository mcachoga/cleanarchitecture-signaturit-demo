namespace Signaturit.Application.CacheKeys
{
    public static class TrialCacheKeys
    {
        public static string ListKey => "TrialList";

        public static string SelectListKey => "TrialSelectList";

        public static string GetKey(int trialId) => $"Product-{trialId}";

        public static string GetDetailsKey(int trialId) => $"ProductDetails-{trialId}";
    }
}