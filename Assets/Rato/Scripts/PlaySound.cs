using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;
    private void OnMouseOver(){
        if(Input.GetMouseButtonDown(0)){
            Debug.Log("AAAAAAAAAAAA");
            MenuController.Instance.gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
