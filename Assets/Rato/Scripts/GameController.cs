using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public float PlayerInitialX;
    public float PlayerInitialY;
    public float PlayerInitialZ;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(string SceneName){
        SceneManager.LoadScene(SceneName);
    }
}
