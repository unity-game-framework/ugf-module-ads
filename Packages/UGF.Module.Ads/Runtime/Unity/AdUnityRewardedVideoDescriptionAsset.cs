using UGF.Description.Runtime;
using UnityEngine;

namespace UGF.Module.Ads.Runtime.Unity
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Ads/Ad Unity Rewarded Video Description", order = 2000)]
    public class AdUnityRewardedVideoDescriptionAsset : DescriptionBuilderAsset<AdUnityRewardedVideoDescription>
    {
        [SerializeField] private string m_placementName;

        public string PlacementName { get { return m_placementName; } set { m_placementName = value; } }

        protected override AdUnityRewardedVideoDescription OnBuild()
        {
            return new AdUnityRewardedVideoDescription(m_placementName);
        }
    }
}
