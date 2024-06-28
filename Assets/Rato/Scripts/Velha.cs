using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velha : MonoBehaviour
{
    public bool walk {get; set;}
    private bool freeze;
    private Animator animator;
    [SerializeField] private float Speed;

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
        if(walk){
            Walk();
        }else{
            Stop();
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Freeze"){
            this.walk = false;
            GameObject.FindGameObjectWithTag("Barrier").GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void Walk(){
        animator.SetBool("walk", true);
        transform.position += new Vector3(-1, 0f, 0f) * Speed * Time.deltaTime;
    }

    void Stop(){
        //Speed = 0;
        animator.SetBool("walk", false);
    }
}
