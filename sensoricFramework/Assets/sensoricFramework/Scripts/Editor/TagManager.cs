using UnityEditor;

namespace SensoricFramework
{
    public static class TagManager
    {

        [InitializeOnLoadMethod]
        private static void Init()
        {
            CiliaDevice.OnTagExist = TagExist;
        }

        public static bool TagExist(string tag)
        {
            bool tagFound = false;
            UnityEngine.Object tagManager = AssetDatabase.LoadMainAssetAtPath("ProjectSettings/TagManager.asset");
            SerializedObject serializedTagManager = new SerializedObject(tagManager);
            SerializedProperty serializedProperty = serializedTagManager.FindProperty("tags");
            for (int i = 0; i < serializedProperty.arraySize; i++)
            {
                if (serializedProperty.GetArrayElementAtIndex(i).stringValue == tag)
                {
                    tagFound = true;
                    break;
                }
            }

            return tagFound;
        }
    }
}