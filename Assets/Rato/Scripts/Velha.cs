using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velha : MonoBehaviour
{
    public bool walk {get; set;}
    private Animator animator;
    [SerializeField] private float Speed;
    private bool walkBack;
    private Time VeiaTime;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        walk = false;
        walkBack = false;
        VeiaTime = new Time();
    }

    // Update is called once per frame
    void Update()
    {
        if(walk){
            Walk(-1);
        }else{
            Stop();
        }

        if(!DialogueManager.Instance.IsDialogueActive() && DialogueManager.Instance.dialogueHappened){
            Walk(1);
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Freeze"){
            this.walk = false;
            GameObject.FindGameObjectWithTag("Barrier").GetComponent<BoxCollider2D>().enabled = false;
            GameObject.FindGameObjectWithTag("Barrier1").GetComponent<BoxCollider2D>().enabled = true;
        }

        if(collider.gameObject.tag == "Adeus"){
            GameObject.FindGameObjectWithTag("Barrier1").GetComponent<BoxCollider2D>().enabled = false;
            gameObject.SetActive(false);
        }
    }

    void Walk(int direction){
        if(direction == -1){
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }else if(direction == 1){
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        animator.SetBool("walk", true);
        transform.position += new Vector3(direction, 0f, 0f) * Speed * Time.deltaTime;
    }

    void Stop(){
        //Speed = 0;
        animator.SetBool("walk", false);
    }
}
