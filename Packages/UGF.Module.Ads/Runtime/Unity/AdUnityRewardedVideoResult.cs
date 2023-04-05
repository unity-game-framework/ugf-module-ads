namespace UGF.Module.Ads.Runtime.Unity
{
    public class AdUnityRewardedVideoResult : IAdResult
    {
        public bool IsSuccessful { get; }

        public AdUnityRewardedVideoResult(bool isSuccessful)
        {
            IsSuccessful = isSuccessful;
        }
    }
}
