using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRato : MonoBehaviour
{
    [SerializeField] private float Speed;
    Animator animator;
    
    void Start()
    {
        this.transform.position = FindObjectOfType<RandomMazeGenerator>().Entrance.transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Walk();    
    }

    void Walk(){
        if(Input.GetAxis("Horizontal") != 0){
            transform.position += new Vector3(Input.GetAxis("Horizontal"), 0f, 0f) * this.Speed * Time.deltaTime;
            animator.SetBool("walking", true);

            if(Input.GetAxis("Horizontal") > 0){
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }else if(Input.GetAxis("Horizontal") < 0){
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
        }else if(Input.GetAxis("Vertical") != 0){
            transform.position += new Vector3(0f, Input.GetAxis("Vertical"), 0f) * this.Speed * Time.deltaTime;
            animator.SetBool("walking", true);

            if(Input.GetAxis("Vertical") > 0){
                transform.eulerAngles = new Vector3(0f, 0f, 90f);
            }else if(Input.GetAxis("Vertical") < 0){
                transform.eulerAngles = new Vector3(0f, 0f, 270f);
            }
        }else{
            animator.SetBool("walking", false);
        }
    }
}
