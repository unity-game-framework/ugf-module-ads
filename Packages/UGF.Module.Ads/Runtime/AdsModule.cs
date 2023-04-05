using System;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;
using UGF.Logs.Runtime;

namespace UGF.Module.Ads.Runtime
{
    public abstract class AdsModule<TDescription> : ApplicationModuleAsync<TDescription>, IAdsModule where TDescription : class, IAdsModuleDescription
    {
        IAdsModuleDescription IAdsModule.Description { get { return Description; } }

        protected AdsModule(TDescription description, IApplication application) : base(description, application)
        {
        }

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            if (Description.EnableOnInitializeAsync)
            {
                Log.Debug("Ads enabling.");

                await EnableAsync();
            }
        }

        public Task<bool> EnableAsync()
        {
            return OnEnableAsync();
        }

        public bool IsAvailable(GlobalId adId)
        {
            if (!adId.IsValid()) throw new ArgumentException("Value should be valid.", nameof(adId));

            IAdDescription description = GetAd(adId);

            return OnIsAvailable(adId, description);
        }

        public Task<IAdResult> ShowAsync(GlobalId adId)
        {
            if (!adId.IsValid()) throw new ArgumentException("Value should be valid.", nameof(adId));

            IAdDescription description = GetAd(adId);

            Log.Debug("Ads showing", new
            {
                adId,
                description.GetType().Name
            });

            return OnShowAsync(adId, description);
        }

        public IAdDescription GetAd(GlobalId adId)
        {
            return TryGetAd(adId, out IAdDescription description) ? description : throw new ArgumentException($"Ad description not found by the specified id: '{adId}'.");
        }

        public bool TryGetAd(GlobalId adId, out IAdDescription description)
        {
            if (!adId.IsValid()) throw new ArgumentException("Value should be valid.", nameof(adId));

            return Description.Ads.TryGetValue(adId, out description);
        }

        protected abstract Task<bool> OnEnableAsync();
        protected abstract bool OnIsAvailable(GlobalId adId, IAdDescription description);
        protected abstract Task<IAdResult> OnShowAsync(GlobalId adId, IAdDescription description);
    }
}
