using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    [SerializeField] private GameObject LeftWall, BottomWall, RightWall, TopWall, Entrance, Exit, path;
    [HideInInspector] public string color;
    [HideInInspector] public MazeCell parent;
    
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
    }
}