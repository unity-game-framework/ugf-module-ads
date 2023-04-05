using System;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Ads.Runtime.Unity
{
    public class AdsUnityModuleDescription : AdsModuleDescription
    {
        public string AppKey { get; }
        public bool ValidateIntegration { get; }

        public AdsUnityModuleDescription(
            Type registerType,
            bool enableOnInitializeAsync,
            IReadOnlyDictionary<GlobalId, IAdDescription> ads,
            string appKey,
            bool validateIntegration) : base(registerType, enableOnInitializeAsync, ads)
        {
            if (string.IsNullOrEmpty(appKey)) throw new ArgumentException("Value cannot be null or empty.", nameof(appKey));

            AppKey = appKey;
            ValidateIntegration = validateIntegration;
        }
    }
}
