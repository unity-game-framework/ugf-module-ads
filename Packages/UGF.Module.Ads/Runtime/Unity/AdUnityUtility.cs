using System;

namespace UGF.Module.Ads.Runtime.Unity
{
    public static class AdUnityUtility
    {
#if UGF_MODULE_ADS_LEVELPLAY_INSTALLED
        public static IronSourceBannerPosition GetBannerPosition(AdUnityBannerPosition position)
        {
            return position switch
            {
                AdUnityBannerPosition.Top => IronSourceBannerPosition.TOP,
                AdUnityBannerPosition.Bottom => IronSourceBannerPosition.BOTTOM,
                _ => throw new ArgumentOutOfRangeException(nameof(position), position, "Ad Unity banner position is unknown.")
            };
        }

        public static IronSourceBannerSize GetBannerSize(AdUnityBannerSize size, int sizeCustomWidth, int sizeCustomHeight)
        {
            return size switch
            {
                AdUnityBannerSize.Standard => IronSourceBannerSize.BANNER,
                AdUnityBannerSize.Custom => new IronSourceBannerSize(sizeCustomWidth, sizeCustomHeight),
                AdUnityBannerSize.Large => IronSourceBannerSize.LARGE,
                AdUnityBannerSize.Rectangle => IronSourceBannerSize.RECTANGLE,
                AdUnityBannerSize.Smart => IronSourceBannerSize.SMART,
                _ => throw new ArgumentOutOfRangeException(nameof(size), size, "Ad Unity banner size is unknown.")
            };
        }
#endif
    }
}
