using System;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;
using UGF.Initialize.Runtime;
using UGF.Logs.Runtime;

namespace UGF.Module.Ads.Runtime.Unity
{
    public class AdsUnityModule : AdsModule<AdsUnityModuleDescription>
    {
        private InitializeState m_state;
        private InitializeState m_ironSourceInitialize;
        private bool m_rewardedVideoInProgress;
        private IAdResult m_rewardedVideoResult;
        private bool m_interstitialInProgress;
        private IAdResult m_interstitialResult;
        private bool m_bannerInProgress;
        private IAdResult m_bannerResult;

        public AdsUnityModule(AdsUnityModuleDescription description, IApplication application) : base(description, application)
        {
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

#if UGF_MODULE_ADS_LEVELPLAY_INSTALLED
            IronSourceEvents.onSdkInitializationCompletedEvent += OnIronSourceInitializationCompleted;

            IronSourceRewardedVideoEvents.onAdRewardedEvent += OnIronSourceRewardedVideoRewarded;
            IronSourceRewardedVideoEvents.onAdClosedEvent += OnIronSourceRewardedVideoClosed;
            IronSourceRewardedVideoEvents.onAdShowFailedEvent += OnIronSourceRewardedVideoShowFailed;

            IronSourceInterstitialEvents.onAdOpenedEvent += OnIronSourceInterstitialOpened;
            IronSourceInterstitialEvents.onAdShowFailedEvent += OnIronSourceInterstitialShowFailed;
            IronSourceInterstitialEvents.onAdClosedEvent += OnIronSourceInterstitialClosed;

            IronSourceBannerEvents.onAdLoadedEvent += OnIronSourceBannerLoaded;
            IronSourceBannerEvents.onAdLoadFailedEvent += OnIronSourceBannerLoadFailed;
#else
            throw new NotSupportedException("Ads Unity Module: LevelPlay package required.");
#endif
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

#if UGF_MODULE_ADS_LEVELPLAY_INSTALLED
            IronSourceEvents.onSdkInitializationCompletedEvent -= OnIronSourceInitializationCompleted;

            IronSourceRewardedVideoEvents.onAdRewardedEvent -= OnIronSourceRewardedVideoRewarded;
            IronSourceRewardedVideoEvents.onAdClosedEvent -= OnIronSourceRewardedVideoClosed;
            IronSourceRewardedVideoEvents.onAdShowFailedEvent -= OnIronSourceRewardedVideoShowFailed;

            IronSourceInterstitialEvents.onAdOpenedEvent -= OnIronSourceInterstitialOpened;
            IronSourceInterstitialEvents.onAdShowFailedEvent -= OnIronSourceInterstitialShowFailed;
            IronSourceInterstitialEvents.onAdClosedEvent -= OnIronSourceInterstitialClosed;

            IronSourceBannerEvents.onAdLoadedEvent -= OnIronSourceBannerLoaded;
            IronSourceBannerEvents.onAdLoadFailedEvent -= OnIronSourceBannerLoadFailed;
#else
            throw new NotSupportedException("Ads Unity Module: LevelPlay package required.");
#endif
        }

        protected override async Task<bool> OnEnableAsync()
        {
#if UGF_MODULE_ADS_LEVELPLAY_INSTALLED
            m_state = m_state.Initialize();

            if (Description.ValidateIntegration)
            {
                Logger.Debug("Ads Unity validating integration.");

                IronSource.Agent.validateIntegration();
            }

            IronSource.Agent.init(Description.AppKey);

            if (!m_ironSourceInitialize)
            {
                await Task.Yield();
            }

            return true;
#else
            throw new NotSupportedException("Ads Unity Module: LevelPlay package required.");
#endif
        }

        protected override bool OnIsAvailable(GlobalId adId, IAdDescription description)
        {
#if UGF_MODULE_ADS_LEVELPLAY_INSTALLED
            return description switch
            {
                AdUnityRewardedVideoDescription => IronSource.Agent.isRewardedVideoAvailable(),
                AdUnityInterstitialDescription => IronSource.Agent.isInterstitialReady(),
                _ => throw new ArgumentException($"Ad description type is unknown: '{description}'.")
            };
#else
            throw new NotSupportedException("Ads Unity Module: LevelPlay package required.");
#endif
        }

        protected override async Task<IAdResult> OnShowAsync(GlobalId adId, IAdDescription description)
        {
#if UGF_MODULE_ADS_LEVELPLAY_INSTALLED
            switch (description)
            {
                case AdUnityRewardedVideoDescription rewardedVideoDescription:
                {
                    if (m_rewardedVideoInProgress || m_interstitialInProgress || m_bannerInProgress)
                    {
                        throw new InvalidOperationException("Ads Unity already showing an ad.");
                    }

                    m_rewardedVideoInProgress = true;

                    IronSource.Agent.showRewardedVideo(rewardedVideoDescription.PlacementName);

                    while (m_rewardedVideoResult == null)
                    {
                        await Task.Yield();
                    }

                    IAdResult result = m_rewardedVideoResult;

                    m_rewardedVideoInProgress = false;
                    m_rewardedVideoResult = null;

                    return result;
                }
                case AdUnityInterstitialDescription interstitialDescription:
                {
                    if (m_rewardedVideoInProgress || m_interstitialInProgress || m_bannerInProgress)
                    {
                        throw new InvalidOperationException("Ads Unity already showing an ad.");
                    }

                    m_interstitialInProgress = true;

                    IronSource.Agent.showInterstitial(interstitialDescription.PlacementName);

                    while (m_interstitialResult == null)
                    {
                        await Task.Yield();
                    }

                    IAdResult result = m_interstitialResult;

                    m_interstitialInProgress = false;
                    m_interstitialResult = null;

                    return result;
                }
                case AdUnityBannerDescription bannerDescription:
                {
                    if (m_rewardedVideoInProgress || m_interstitialInProgress || m_bannerInProgress)
                    {
                        throw new InvalidOperationException("Ads Unity already showing an ad.");
                    }

                    m_bannerInProgress = true;

                    IronSourceBannerPosition position = AdUnityUtility.GetBannerPosition(bannerDescription.Position);
                    IronSourceBannerSize size = AdUnityUtility.GetBannerSize(bannerDescription.Size, bannerDescription.SizeCustomWidth, bannerDescription.SizeCustomHeight);

                    IronSource.Agent.loadBanner(size, position, bannerDescription.PlacementName);

                    while (m_bannerResult == null)
                    {
                        await Task.Yield();
                    }

                    IAdResult result = m_bannerResult;

                    if (result.IsSuccessful)
                    {
                        IronSource.Agent.displayBanner();
                    }

                    m_bannerInProgress = false;
                    m_bannerResult = null;

                    return result;
                }
                default:
                {
                    throw new ArgumentException($"Ad description type is unknown: '{description}'.");
                }
            }
#else
            throw new NotSupportedException("Ads Unity Module: LevelPlay package required.");
#endif
        }

#if UGF_MODULE_ADS_LEVELPLAY_INSTALLED
        private void OnIronSourceInitializationCompleted()
        {
            m_ironSourceInitialize = m_ironSourceInitialize.Initialize();
        }

        private void OnIronSourceRewardedVideoRewarded(IronSourcePlacement placement, IronSourceAdInfo info)
        {
            m_rewardedVideoResult = new AdUnityRewardedVideoResult(true);
        }

        private void OnIronSourceRewardedVideoClosed(IronSourceAdInfo info)
        {
            m_rewardedVideoResult = new AdUnityRewardedVideoResult(false);
        }

        private void OnIronSourceRewardedVideoShowFailed(IronSourceError error, IronSourceAdInfo info)
        {
            m_rewardedVideoResult = AdResultError.Instance;
        }

        private void OnIronSourceInterstitialOpened(IronSourceAdInfo info)
        {
            m_interstitialResult = new AdUnityInterstitialResult(true);
        }

        private void OnIronSourceInterstitialShowFailed(IronSourceError error, IronSourceAdInfo info)
        {
            m_interstitialResult = new AdUnityInterstitialResult(false);
        }

        private void OnIronSourceInterstitialClosed(IronSourceAdInfo info)
        {
            m_interstitialResult = new AdUnityInterstitialResult(true);
        }

        private void OnIronSourceBannerLoaded(IronSourceAdInfo info)
        {
            m_bannerResult = new AdUnityBannerResult(true);
        }

        private void OnIronSourceBannerLoadFailed(IronSourceError info)
        {
            m_bannerResult = new AdUnityBannerResult(false);
        }
#endif
    }
}
