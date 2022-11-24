using System.IO;
using UnityEditor;
using UnityEngine;

namespace Ogxd.ProjectCurator
{
    [InitializeOnLoad]
    public static class ProjectWindowDetails
    {
        static ProjectWindowDetails()
        {
            EditorApplication.projectWindowItemOnGUI -= DrawAssetDetails;
            EditorApplication.projectWindowItemOnGUI += DrawAssetDetails;
        }

        static void DrawAssetDetails(string guid, Rect rect)
        {
            var r = new Rect(rect.x, rect.y, 1f, 16f);

            string path = AssetDatabase.GUIDToAssetPath(guid);

            if(Directory.Exists(path))
            {
                return;
            }

            AssetInfo assetInfo = ProjectCurator.GetAsset(path);

            if(assetInfo == null)
            {
                return;
            }
            
            int count = assetInfo.referencers.Count;
            bool hasRefs = count > 0;

            const string NoRefAddressable = "<color=#7FD6FC>A</color>";

            if(hasRefs || assetInfo.IsAddressable)
            {
                string countText = count.ToString();
                string text = hasRefs
                    ? assetInfo.IsAddressable ? $"<color=#7FD6FC>{countText}</color>" : countText
                    : NoRefAddressable;

                var content = new GUIContent(text);
                r.width = 0f;
                r.xMin -= 100f;
                GUI.Label(r, content, MiniLabelAlignRight);
            }
        }

        static GUIStyle _miniLabelAlignRight;
        static GUIStyle MiniLabelAlignRight => _miniLabelAlignRight ??= new GUIStyle(EditorStyles.miniLabel) { alignment = TextAnchor.MiddleRight, richText = true};
    }
}
