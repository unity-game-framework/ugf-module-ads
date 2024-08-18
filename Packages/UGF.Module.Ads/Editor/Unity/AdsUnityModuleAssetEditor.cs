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

            m_listRewardedVideos.Enable();
            m_listRewardedVideosSelection.Enable();
        }

        private void OnDisable()
        {
            m_listRewardedVideos.Disable();
            m_listRewardedVideosSelection.Disable();
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
                m_listRewardedVideosSelection.DrawGUILayout();
            }

#if !UGF_MODULE_ADS_LEVELPLAY_INSTALLED
            EditorGUILayout.HelpBox("Ads Unity Module: LevelPlay package required.", MessageType.Warning);
#endif
        }
    }
}
