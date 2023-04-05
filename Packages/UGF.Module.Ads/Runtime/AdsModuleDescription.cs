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
            Type registerType,
            bool enableOnInitializeAsync,
            IReadOnlyDictionary<GlobalId, IAdDescription> ads)
        {
            RegisterType = registerType ?? throw new ArgumentNullException(nameof(registerType));
            EnableOnInitializeAsync = enableOnInitializeAsync;
            Ads = ads ?? throw new ArgumentNullException(nameof(ads));
        }
    }
}
