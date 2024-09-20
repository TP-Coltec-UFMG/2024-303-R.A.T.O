using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    [SerializeField] private GameObject LeftWall, BottomWall, RightWall, TopWall, Entrance, Exit, path;
    [HideInInspector] public string color;
    [HideInInspector] public MazeCell parent, child;
    
    public void RemoveWall(string wall){
        switch (wall){
            case "left":
                LeftWall.SetActive(false);
                break;
            case "bottom":
                BottomWall.SetActive(false);
                break;
            case "right":
                RightWall.SetActive(false);
                break;
            case "top":
                TopWall.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void IsEntrance(bool isEntrance){
        if(isEntrance){
            this.Entrance.SetActive(true);
        } 
    }

    public void IsExit(bool isExit){
        if(isExit){
            this.Exit.SetActive(true);
        } 
    }

    public void Path(){
        this.path.SetActive(true);
        this.path.GetComponent<AudioSource>().volume = 0;
    }

    public void SetExit(string NewScene, Vector3 ratoPosition){
        this.Exit.GetComponent<ChangeSceneTrigger>().NewScene = NewScene;
        this.Exit.GetComponent<ChangeSceneTrigger>().RatoPosition = ratoPosition;
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            if(this.child != null){
                //this.child.path.GetComponent<AudioSource>().enabled = true;
                this.child.path.GetComponent<AudioSource>().volume = GameController.Instance.musicVolume;
            }

            //StartCoroutine(UnableSound());
            this.path.GetComponent<AudioSource>().volume = 0;    
        }
    }

    IEnumerator UnableSound(){
        yield return new WaitForSeconds(0.3f);
        //this.path.GetComponent<AudioSource>().enabled = false;
        this.path.GetComponent<AudioSource>().volume = 0;
    }
}