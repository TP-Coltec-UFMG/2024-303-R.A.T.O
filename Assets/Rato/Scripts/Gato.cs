using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gato : MonoBehaviour
{
    private Transform Rato;
    private Animator animator;
    [SerializeField] private float Range;

    // Start is called before the first frame update
    void Start()
    {
        Rato = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        Run();   
    }

    public void LookAtPlayer(){ 
        if(Rato.position.x > transform.position.x){
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            Debug.Log("rato na frente");
        }else if(Rato.position.x < transform.position.x){
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            Debug.Log("rato atras");
        }
    }

    public void Run(){
        if(Rato.position.x - transform.position.x > Range || Rato.position.x - transform.position.x < -Range){
            animator.SetBool("run", true);
        }else{
            animator.SetBool("run", false);
        }
    } 
}
