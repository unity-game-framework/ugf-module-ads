using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Ads.Runtime
{
    public class AdsModuleDescription : ApplicationModuleDescription, IAdsModuleDescription
    {
        public bool EnableOnInitializeAsync { get; }
        public IReadOnlyDictionary<GlobalId, IAdDescription> Ads { get; }

        public AdsModuleDescription(
            bool enableOnInitializeAsync,
            IReadOnlyDictionary<GlobalId, IAdDescription> ads)
        {
            EnableOnInitializeAsync = enableOnInitializeAsync;
            Ads = ads ?? throw new ArgumentNullException(nameof(ads));
        }
    }
}
