using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class Data{
    //coisas pra salvar
    public float currentPositionX, currentPositionY;
    public int currentScene, ratoHumanity;

    public Data(float x, float y, int s, int r){
        currentPositionX = x;
        currentPositionY = y;
        currentScene = s;
        ratoHumanity = r;
    }
}
