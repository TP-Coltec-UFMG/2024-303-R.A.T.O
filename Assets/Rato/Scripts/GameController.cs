using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public static void ChangeScene(string SceneName){
        SceneManager.LoadScene(SceneName);
    }

    public static void ChangeFontColor(Color color){
        TMP_Text[] changeThisColour = FindObjectsOfType<TMP_Text>();

        foreach(TMP_Text element in changeThisColour){
            if(element.tag == "ChangeableFont"){
                element.color = color;
            }
        }
    }

    public static void ChangeFontSize(int size){
        TMP_Text[] changeThisSize = FindObjectsOfType<TMP_Text>();

        foreach(TMP_Text element in changeThisSize){
            if(element.tag == "ChangeableFont"){
                element.fontSize = size;
            }
        }
    }
}
