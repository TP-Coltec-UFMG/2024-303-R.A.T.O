using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePrefs : MonoBehaviour
{
    public static void SaveFloat(string Name, float v){
        PlayerPrefs.SetFloat(Name, v);
        PlayerPrefs.Save();
    }

    public static void SaveString(string Name, string v){
        PlayerPrefs.SetString(Name, v);
        PlayerPrefs.Save();
    }

    public static void SaveBool(string Name, bool v){
        PlayerPrefs.SetInt(Name, boolToInt(v));
        PlayerPrefs.Save();
    }

    public static void SaveInt(string Name, int v){
        PlayerPrefs.SetInt(Name, v);
        PlayerPrefs.Save();
    }

    public static string GetString(string Name){
        return PlayerPrefs.GetString(Name);
    }

    public static float GetFloat(string Name){
        return PlayerPrefs.GetFloat(Name);
    }

    public static bool GetBool(string Name){
        return intToBool(PlayerPrefs.GetInt(Name));
    }

    public static int GetInt(string Name){
        return PlayerPrefs.GetInt(Name);
    }

    public static int boolToInt(bool v){
        if (v){
            return 1;
        }else{
            return 0;
        }
    }
    
    public static bool intToBool(int v){
        if (v != 0){
            return true;
        }
        else{
            return false;
        }
}
}
