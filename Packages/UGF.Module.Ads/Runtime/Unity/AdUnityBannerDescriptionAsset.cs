using UGF.Description.Runtime;
using UnityEngine;

namespace UGF.Module.Ads.Runtime.Unity
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Ads/Ad Unity Banner Description", order = 2000)]
    public class AdUnityBannerDescriptionAsset : DescriptionBuilderAsset<AdUnityBannerDescription>
    {
        [SerializeField] private string m_placementName;
        [SerializeField] private AdUnityBannerPosition m_position;
        [SerializeField] private AdUnityBannerSize m_size;
        [SerializeField] private int m_sizeCustomWidth;
        [SerializeField] private int m_sizeCustomHeight;

        public string PlacementName { get { return m_placementName; } set { m_placementName = value; } }
        public AdUnityBannerPosition Position { get { return m_position; } set { m_position = value; } }
        public AdUnityBannerSize Size { get { return m_size; } set { m_size = value; } }
        public int SizeCustomWidth { get { return m_sizeCustomWidth; } set { m_sizeCustomWidth = value; } }
        public int SizeCustomHeight { get { return m_sizeCustomHeight; } set { m_sizeCustomHeight = value; } }

        protected override AdUnityBannerDescription OnBuild()
        {
            return new AdUnityBannerDescription(
                m_placementName,
                m_position,
                m_size,
                m_sizeCustomWidth,
                m_sizeCustomHeight
            );
        }
    }
}
