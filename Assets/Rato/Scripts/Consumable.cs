using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    [SerializeField] private GameObject ConsumirTutorial;

    void Update(){
        ConsumirTutorial.transform.position = this.transform.position;
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            ConsumirTutorial.SetActive(true);
            StartCoroutine(WaitForKeyPress());
        }
    }

    void OnTriggerExit2D(){
        if(ConsumirTutorial != null){
            ConsumirTutorial.SetActive(false);
        } 
        StopAllCoroutines();
    }

    private IEnumerator WaitForKeyPress(){
        while (!UserInput.Instance.AttackInput){
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        if(gameObject.tag == "Queijo"){
            GameController.Instance.IncreaseRatoHumanity();
        }

        Destroy(gameObject);
    }
}
