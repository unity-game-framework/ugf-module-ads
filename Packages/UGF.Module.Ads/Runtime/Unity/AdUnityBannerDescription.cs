using System;

namespace UGF.Module.Ads.Runtime.Unity
{
    public class AdUnityBannerDescription : AdUnityDescription
    {
        public string PlacementName { get; }
        public AdUnityBannerPosition Position { get; }
        public AdUnityBannerSize Size { get; }
        public int SizeCustomWidth { get; }
        public int SizeCustomHeight { get; }

        public AdUnityBannerDescription(string placementName, AdUnityBannerPosition position, AdUnityBannerSize size, int sizeCustomWidth, int sizeCustomHeight)
        {
            if (string.IsNullOrEmpty(placementName)) throw new ArgumentException("Value cannot be null or empty.", nameof(placementName));
            if (sizeCustomWidth < 1) throw new ArgumentOutOfRangeException(nameof(sizeCustomWidth));
            if (sizeCustomHeight < 1) throw new ArgumentOutOfRangeException(nameof(sizeCustomHeight));

            PlacementName = placementName;
            Position = position;
            Size = size;
            SizeCustomWidth = sizeCustomWidth;
            SizeCustomHeight = sizeCustomHeight;
        }
    }
}
