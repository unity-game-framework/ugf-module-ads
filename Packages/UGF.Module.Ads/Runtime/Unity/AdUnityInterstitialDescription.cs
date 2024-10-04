using System;

namespace UGF.Module.Ads.Runtime.Unity
{
    public class AdUnityInterstitialDescription : AdUnityDescription
    {
        public string PlacementName { get; }

        public AdUnityInterstitialDescription(string placementName)
        {
            if (string.IsNullOrEmpty(placementName)) throw new ArgumentException("Value cannot be null or empty.", nameof(placementName));

            PlacementName = placementName;
        }
    }
}
