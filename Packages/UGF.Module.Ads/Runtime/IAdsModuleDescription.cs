using System.Collections.Generic;
using UGF.Description.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Ads.Runtime
{
    public interface IAdsModuleDescription : IDescription
    {
        bool EnableOnInitializeAsync { get; }
        IReadOnlyDictionary<GlobalId, IAdDescription> Ads { get; }
    }
}
