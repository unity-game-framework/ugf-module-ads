using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Ads.Runtime
{
    public interface IAdsModuleDescription : IApplicationModuleDescription
    {
        bool EnableOnInitializeAsync { get; }
        IReadOnlyDictionary<GlobalId, IAdDescription> Ads { get; }
    }
}
