﻿using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.Module.Ads.Runtime.Unity
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Ads/Ads Unity Module", order = 2000)]
    public class AdsUnityModuleAsset : ApplicationModuleAsset<AdsUnityModule, AdsUnityModuleDescription>
    {
        [SerializeField] private bool m_enableOnInitializeAsync;
        [SerializeField] private string m_appKey;
        [SerializeField] private bool m_validateIntegration;
        [SerializeField] private List<AssetIdReference<AdUnityRewardedVideoDescriptionAsset>> m_rewardedVideos = new List<AssetIdReference<AdUnityRewardedVideoDescriptionAsset>>();
        [SerializeField] private List<AssetIdReference<AdUnityInterstitialDescriptionAsset>> m_interstitials = new List<AssetIdReference<AdUnityInterstitialDescriptionAsset>>();
        [SerializeField] private List<AssetIdReference<AdUnityBannerDescriptionAsset>> m_banners = new List<AssetIdReference<AdUnityBannerDescriptionAsset>>();

        public bool EnableOnInitializeAsync { get { return m_enableOnInitializeAsync; } set { m_enableOnInitializeAsync = value; } }
        public string AppKey { get { return m_appKey; } set { m_appKey = value; } }
        public bool ValidateIntegration { get { return m_validateIntegration; } set { m_validateIntegration = value; } }
        public List<AssetIdReference<AdUnityRewardedVideoDescriptionAsset>> RewardedVideos { get { return m_rewardedVideos; } }
        public List<AssetIdReference<AdUnityInterstitialDescriptionAsset>> Interstitials { get { return m_interstitials; } }
        public List<AssetIdReference<AdUnityBannerDescriptionAsset>> Banners { get { return m_banners; } }

        protected override AdsUnityModuleDescription OnBuildDescription()
        {
            var ads = new Dictionary<GlobalId, IAdDescription>();

            for (int i = 0; i < m_rewardedVideos.Count; i++)
            {
                AssetIdReference<AdUnityRewardedVideoDescriptionAsset> reference = m_rewardedVideos[i];

                ads.Add(reference.Guid, reference.Asset.Build());
            }

            for (int i = 0; i < m_interstitials.Count; i++)
            {
                AssetIdReference<AdUnityInterstitialDescriptionAsset> reference = m_interstitials[i];

                ads.Add(reference.Guid, reference.Asset.Build());
            }

            for (int i = 0; i < m_banners.Count; i++)
            {
                AssetIdReference<AdUnityBannerDescriptionAsset> reference = m_banners[i];

                ads.Add(reference.Guid, reference.Asset.Build());
            }

            return new AdsUnityModuleDescription(m_enableOnInitializeAsync,
                ads,
                m_appKey,
                m_validateIntegration
            );
        }

        protected override AdsUnityModule OnBuild(AdsUnityModuleDescription description, IApplication application)
        {
            return new AdsUnityModule(description, application);
        }
    }
}
