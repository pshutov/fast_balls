using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace _CrossProjects.Editor
{
    public static class SavedDataCleaner
    {
        [MenuItem("Tools/Data/Clear PlayerPrefs", false, int.MinValue + 101)]
        private static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
        
        [MenuItem("Tools/Data/Clear PersistentData", false, int.MinValue + 201)]
        private static void ClearPersistentData()
        {
            try
            {
                string path = Application.persistentDataPath;
                Directory.Delete(path, true);
            }
            catch (Exception _)
            {
            }
        }
    }
}