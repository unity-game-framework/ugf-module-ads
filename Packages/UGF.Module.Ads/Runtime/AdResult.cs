namespace UGF.Module.Ads.Runtime
{
    public class AdResult : IAdResult
    {
        public bool IsSuccessful { get; }

        public AdResult(bool isSuccessful)
        {
            IsSuccessful = isSuccessful;
        }
    }
}
