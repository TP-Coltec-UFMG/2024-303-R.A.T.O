using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velha : MonoBehaviour
{
    public bool walk {get; set;}
    private bool freeze;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        walk = false;
        freeze = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(walk && !freeze){
            animator.SetBool("walk", true);
        }else{
            animator.SetBool("walk", false);
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Freeze"){
            this.freeze = true;
            GameObject.FindGameObjectWithTag("Barrier").GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
