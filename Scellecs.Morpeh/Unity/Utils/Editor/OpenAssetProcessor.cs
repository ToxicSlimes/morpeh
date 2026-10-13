#if UNITY_EDITOR
namespace Scellecs.Morpeh.Utils.Editor {
    using Scellecs.Morpeh;
    using UnityEditor;
    using UnityEditor.Callbacks;
    using UnityEngine;

    public static class OpenAssetProcessor {
        [OnOpenAsset(-1000)]
#if UNITY_6000_3_OR_NEWER
        public static bool OnOpenAsset(EntityId entityId, int line) {
            var obj = EditorUtility.EntityIdToObject(entityId);
#else
        public static bool OnOpenAsset(int instanceID, int line) {
            var obj = EditorUtility.InstanceIDToObject(instanceID);
#endif
            if (obj is ScriptableObject so) {
                if (so is IInitializer) {
                    var monoScript = MonoScript.FromScriptableObject(so);
                    AssetDatabase.OpenAsset(monoScript);
                    return true;
                }
            }
            return false;
        }
    }
}
#endif