using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moeda : MonoBehaviour
{
    private int bIsOnTheMove;
    private float z;

    void Update(){
        StartCoroutine(CheckMoving());

        if(bIsOnTheMove == 1){
            //GetComponent<Animator>().SetBool("l", true);
            //GetComponent<Animator>().SetBool("r", false);
            z += -180 * Time.deltaTime; 
            transform.eulerAngles = new Vector3(0f, 0f, z);
        }else if(bIsOnTheMove == -1){
            //GetComponent<Animator>().SetBool("r", true);
            //GetComponent<Animator>().SetBool("l",false);
            transform.eulerAngles = new Vector3(0f, 0f, z);
            z += 180 * Time.deltaTime;
        }
    }

    private IEnumerator CheckMoving(){
        Vector3 startPos = transform.position;
        yield return new WaitForSeconds(0.0005f);
        Vector3 finalPos = transform.position;

        if(startPos.x < finalPos.x){
            bIsOnTheMove = 1;
            Debug.Log("aaaaaa");
        }else if(startPos.x > finalPos.x){
            bIsOnTheMove = -1;
            Debug.Log("bbbbbb");
        }else{
            bIsOnTheMove = 0;
        }
    }
}
