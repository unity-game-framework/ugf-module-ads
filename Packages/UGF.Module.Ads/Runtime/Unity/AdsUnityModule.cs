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
        public IronSourceIAgent Agent { get { return IronSource.Agent; } }

        private InitializeState m_state;
        private InitializeState m_ironSourceInitialize;
        private bool m_rewardedVideoInProgress;
        private IAdResult m_rewardedVideoResult;

        public AdsUnityModule(AdsUnityModuleDescription description, IApplication application) : base(description, application)
        {
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            IronSourceEvents.onSdkInitializationCompletedEvent += OnIronSourceInitializationCompleted;
            IronSourceRewardedVideoEvents.onAdRewardedEvent += OnIronSourceRewardedVideoRewarded;
            IronSourceRewardedVideoEvents.onAdClosedEvent += OnIronSourceRewardedVideoClosed;
            IronSourceRewardedVideoEvents.onAdShowFailedEvent += OnIronSourceRewardedVideoShowFailed;
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            IronSourceEvents.onSdkInitializationCompletedEvent -= OnIronSourceInitializationCompleted;
            IronSourceRewardedVideoEvents.onAdRewardedEvent -= OnIronSourceRewardedVideoRewarded;
            IronSourceRewardedVideoEvents.onAdClosedEvent -= OnIronSourceRewardedVideoClosed;
            IronSourceRewardedVideoEvents.onAdShowFailedEvent -= OnIronSourceRewardedVideoShowFailed;
        }

        protected override async Task<bool> OnEnableAsync()
        {
            m_state = m_state.Initialize();

            if (Description.ValidateIntegration)
            {
                Logger.Debug("Ads Unity validating integration.");

                Agent.validateIntegration();
            }

            Agent.init(Description.AppKey);

            if (!m_ironSourceInitialize)
            {
                await Task.Yield();
            }

            return true;
        }

        protected override bool OnIsAvailable(GlobalId adId, IAdDescription description)
        {
            switch (description)
            {
                case AdUnityRewardedVideoDescription:
                {
                    return Agent.isRewardedVideoAvailable();
                }
                default:
                {
                    throw new ArgumentException($"Ad description type is unknown: '{description}'.");
                }
            }
        }

        protected override async Task<IAdResult> OnShowAsync(GlobalId adId, IAdDescription description)
        {
            switch (description)
            {
                case AdUnityRewardedVideoDescription rewardedVideoDescription:
                {
                    if (m_rewardedVideoInProgress) throw new InvalidOperationException("Ads Unity already showing an ad.");

                    m_rewardedVideoInProgress = true;

                    Agent.showRewardedVideo(rewardedVideoDescription.PlacementName);

                    while (m_rewardedVideoResult == null)
                    {
                        await Task.Yield();
                    }

                    IAdResult result = m_rewardedVideoResult;

                    m_rewardedVideoInProgress = false;
                    m_rewardedVideoResult = null;

                    return result;
                }
                default:
                {
                    throw new ArgumentException($"Ad description type is unknown: '{description}'.");
                }
            }
        }

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
    }
}
