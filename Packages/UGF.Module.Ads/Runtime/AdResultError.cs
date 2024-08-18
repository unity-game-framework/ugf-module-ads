namespace UGF.Module.Ads.Runtime
{
    public class AdResultError : IAdResult
    {
        public bool IsSuccessful { get; } = false;
        public static AdResultError Instance { get; } = new AdResultError();
    }
}
