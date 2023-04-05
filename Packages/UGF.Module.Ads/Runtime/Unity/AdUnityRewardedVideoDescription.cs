using System;

namespace UGF.Module.Ads.Runtime.Unity
{
    public class AdUnityRewardedVideoDescription : AdUnityDescription
    {
        public string PlacementName { get; }

        public AdUnityRewardedVideoDescription(string placementName)
        {
            if (string.IsNullOrEmpty(placementName)) throw new ArgumentException("Value cannot be null or empty.", nameof(placementName));

            PlacementName = placementName;
        }
    }
}
