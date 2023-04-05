namespace UGF.Module.Ads.Runtime
{
    public class AdResultError : IAdResult
    {
        public bool IsSuccessful { get; } = true;
        public static AdResultError Instance { get; } = new AdResultError();
    }
}
