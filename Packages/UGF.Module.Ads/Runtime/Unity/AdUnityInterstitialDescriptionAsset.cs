using UGF.Description.Runtime;
using UnityEngine;

namespace UGF.Module.Ads.Runtime.Unity
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Ads/Ad Unity Interstitial Description", order = 2000)]
    public class AdUnityInterstitialDescriptionAsset : DescriptionBuilderAsset<AdUnityInterstitialDescription>
    {
        [SerializeField] private string m_placementName;

        public string PlacementName { get { return m_placementName; } set { m_placementName = value; } }

        protected override AdUnityInterstitialDescription OnBuild()
        {
            return new AdUnityInterstitialDescription(m_placementName);
        }
    }
}
