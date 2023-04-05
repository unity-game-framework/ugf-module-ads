using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Ads.Runtime
{
    public interface IAdsModule : IApplicationModuleAsync
    {
        new IAdsModuleDescription Description { get; }

        Task<bool> EnableAsync();
        bool IsAvailable(GlobalId adId);
        Task<IAdResult> ShowAsync(GlobalId adId);
        IAdDescription GetAd(GlobalId adId);
        bool TryGetAd(GlobalId adId, out IAdDescription description);
    }
}
