using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRato : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private Transform AudioListener;
    Animator animator;
    
    void Start(){
        this.transform.position = FindObjectOfType<RandomMazeGenerator>().Entrance.transform.position;
        animator = GetComponent<Animator>();
        transform.eulerAngles = new Vector3(0f, 0f, 90f);
    }

    // Update is called once per frame
    void Update(){
        Walk();
        Rotate();    
    }

    void Walk(){
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        transform.position += movement * Speed * Time.deltaTime;
        animator.SetBool("walk", (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0));
        this.AudioListener.position = transform.position;
    }

    void Rotate(){
        Vector3 movementDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f).normalized;
        
        if(movementDirection != Vector3.zero){
            float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

}
