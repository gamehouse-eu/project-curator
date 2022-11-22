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
            int count = assetInfo.referencers.Count;

            if(count > 0)
            {
                var content = new GUIContent(count.ToString());
                r.width = 0f;
                r.xMin -= 100f;
                GUI.Label(r, content, MiniLabelAlignRight);
            }
        }

        static GUIStyle _miniLabelAlignRight;
        static GUIStyle MiniLabelAlignRight => _miniLabelAlignRight ??= new GUIStyle(EditorStyles.miniLabel) { alignment = TextAnchor.MiddleRight };
    }
}
