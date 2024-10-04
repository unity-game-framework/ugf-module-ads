using UGF.EditorTools.Editor.Assets;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.Ads.Runtime.Unity;
using UnityEditor;

namespace UGF.Module.Ads.Editor.Editor.Unity
{
    [CustomEditor(typeof(AdsUnityModuleAsset), true)]
    internal class AdsUnityModuleAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyEnableOnInitializeAsync;
        private SerializedProperty m_propertyAppKey;
        private SerializedProperty m_propertyValidateIntegration;
        private AssetIdReferenceListDrawer m_listRewardedVideos;
        private ReorderableListSelectionDrawerByPath m_listRewardedVideosSelection;
        private AssetIdReferenceListDrawer m_listInterstitials;
        private ReorderableListSelectionDrawerByPath m_listInterstitialsSelection;
        private AssetIdReferenceListDrawer m_listBanners;
        private ReorderableListSelectionDrawerByPath m_listBannersSelection;

        private void OnEnable()
        {
            m_propertyEnableOnInitializeAsync = serializedObject.FindProperty("m_enableOnInitializeAsync");
            m_propertyAppKey = serializedObject.FindProperty("m_appKey");
            m_propertyValidateIntegration = serializedObject.FindProperty("m_validateIntegration");

            m_listRewardedVideos = new AssetIdReferenceListDrawer(serializedObject.FindProperty("m_rewardedVideos"));

            m_listRewardedVideosSelection = new ReorderableListSelectionDrawerByPath(m_listRewardedVideos, "m_asset")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listInterstitials = new AssetIdReferenceListDrawer(serializedObject.FindProperty("m_interstitials"));

            m_listInterstitialsSelection = new ReorderableListSelectionDrawerByPath(m_listInterstitials, "m_asset")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listBanners = new AssetIdReferenceListDrawer(serializedObject.FindProperty("m_banners"));

            m_listBannersSelection = new ReorderableListSelectionDrawerByPath(m_listBanners, "m_asset")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listRewardedVideos.Enable();
            m_listRewardedVideosSelection.Enable();
            m_listInterstitials.Enable();
            m_listInterstitialsSelection.Enable();
            m_listBanners.Enable();
            m_listBannersSelection.Enable();
        }

        private void OnDisable()
        {
            m_listRewardedVideos.Disable();
            m_listRewardedVideosSelection.Disable();
            m_listInterstitials.Disable();
            m_listInterstitialsSelection.Disable();
            m_listBanners.Disable();
            m_listBannersSelection.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                EditorGUILayout.PropertyField(m_propertyEnableOnInitializeAsync);
                EditorGUILayout.PropertyField(m_propertyAppKey);
                EditorGUILayout.PropertyField(m_propertyValidateIntegration);

                m_listRewardedVideos.DrawGUILayout();
                m_listInterstitials.DrawGUILayout();
                m_listBanners.DrawGUILayout();

                m_listRewardedVideosSelection.DrawGUILayout();
                m_listInterstitialsSelection.DrawGUILayout();
                m_listBannersSelection.DrawGUILayout();
            }

#if !UGF_MODULE_ADS_LEVELPLAY_INSTALLED
            EditorGUILayout.HelpBox("Ads Unity Module: LevelPlay package required.", MessageType.Warning);
#endif
        }
    }
}
