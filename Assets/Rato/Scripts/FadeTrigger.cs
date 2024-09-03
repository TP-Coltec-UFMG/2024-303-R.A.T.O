using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeTrigger : MonoBehaviour
{
    [SerializeField] private GameObject Change;
    [SerializeField] private float FinalOpacity;

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            StartCoroutine(Fade());
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private IEnumerator Fade(){
        SpriteRenderer image = this.Change.gameObject.GetComponent<SpriteRenderer>();
        Color color = image.color;
        float currentOpacity = 0;

        while(image.color.a != FinalOpacity){
            image.color = new Color(color.r, color.g, color.b, currentOpacity);
            yield return new WaitForSeconds(0.05f);
            currentOpacity += 0.01f;   
        }
    }
}
