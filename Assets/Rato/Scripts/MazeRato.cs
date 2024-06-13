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
        Rotate();    
    }

    void Walk(){
        transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f) * this.Speed * Time.deltaTime;
        animator.SetBool("walk", true);
    }

    void Rotate(){
        if(Input.GetAxis("Horizontal") > 0){
            if(Input.GetAxis("Vertical") > 0){
                transform.eulerAngles = new Vector3(0f, 45f, 0f);
            }else if(Input.GetAxis("Vertical") < 0){
                transform.eulerAngles = new Vector3(0f, 0f, 135f);
            }else{
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
        }else if(Input.GetAxis("Horizontal") < 0){
            if(Input.GetAxis("Vertical") > 0){
                transform.eulerAngles = new Vector3(0f, 0f, -45f);
            }else if(Input.GetAxis("Vertical") > 0){
                transform.eulerAngles = new Vector3(0f, 0f, -135f);
            }else{
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
        }else{
            if(Input.GetAxis("Vertical") > 0){
                transform.eulerAngles = new Vector3(0f, 0f, 90f);
            }else if(Input.GetAxis("Vertical") < 0){
                transform.eulerAngles = new Vector3(0f, 0f, 270f);
            }
        }
    }
}
