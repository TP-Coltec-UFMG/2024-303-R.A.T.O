using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance {get; private set;}
    private Rato rato;
    [SerializeField] private Slider RatoHealthBar;
    void Awake(){
        if (Instance == null){
            Instance = this;
        }else{
            Destroy(gameObject);
        }

        rato = FindObjectOfType<Rato>();
    }

    void Update(){
        ShowHealth();
    }

    public void ChangeScene(string SceneName){
        SceneManager.LoadScene(SceneName);
    }

    public void ChangeFontColor(Color color){
        TMP_Text[] changeThisColour = FindObjectsOfType<TMP_Text>();

        foreach(TMP_Text element in changeThisColour){
            if(element.tag == "ChangeableFont"){
                element.color = color;
            }
        }
    }

    public void ChangeFontSize(int size){
        TMP_Text[] changeThisSize = FindObjectsOfType<TMP_Text>();

        foreach(TMP_Text element in changeThisSize){
            if(element.tag == "ChangeableFont"){
                element.fontSize = size;
            }
        }
    }

    void ShowHealth(){
        RatoHealthBar.maxValue = rato.MaxHealth;
        RatoHealthBar.value = rato.health;        
    }

    public void StopGame(){

        Time.timeScale = 0;
    }

    public void Resume(){
        Time.timeScale = 1;
    }
}
