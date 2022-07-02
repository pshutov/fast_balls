using System;
using UnityEngine;

namespace _CrossProjects.Tools
{
    public static class UPlayerPrefs
    {
        public static int GetInt(string key, int defaultValue = default(int))
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }
        
        public static void SetInt(string key, int value, bool autoSave = false)
        {
            PlayerPrefs.SetInt(key, value);
            
            if (autoSave)
            {
                PlayerPrefs.Save();
            }
        }
        
        public static float GetFloat(string key, float defaultValue = default(float))
        {
            return PlayerPrefs.GetFloat(key, defaultValue);
        }
        
        public static void SetFloat(string key, float value, bool autoSave = false)
        {
            PlayerPrefs.SetFloat(key, value);
            
            if (autoSave)
            {
                PlayerPrefs.Save();
            }
        }
        
        public static string GetString(string key, string defaultValue = "")
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }
        
        public static void SetString(string key, string value, bool autoSave = false)
        {
            PlayerPrefs.SetString(key, value);
            
            if (autoSave)
            {
                PlayerPrefs.Save();
            }
        }
        
        public static bool GetBool(string key, bool defaultValue = default(bool))
        {
            int convertedDefaultValue = defaultValue ? 1 : 0;
            return GetInt(key, convertedDefaultValue) == 1;
        }
        
        public static void SetBool(string key, bool value, bool autoSave = false)
        {
            int convertedValue = value ? 1 : 0;
            SetInt(key, convertedValue, autoSave);
        }

        public static T GetObject<T>(string key, T defaultValue = default(T))
        {
            string data = GetString(key);
            if (string.IsNullOrEmpty(data))
            {
                return defaultValue;
            }

            try
            {
                var result = JsonUtility.FromJson<T>(data);
                return result;
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }

            return defaultValue;
        }

        public static void SetObject<T>(string key, T value, bool autoSave = false)
        {
            string data = JsonUtility.ToJson(value);
            SetString(key, data, autoSave);
        }
    }
}