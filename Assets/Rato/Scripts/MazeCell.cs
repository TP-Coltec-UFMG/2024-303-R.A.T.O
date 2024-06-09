using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    [SerializeField] private GameObject LeftWall, BottomWall, RightWall, TopWall;
    
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
}
