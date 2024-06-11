using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rato : MonoBehaviour
{
    [SerializeField] private float OriginalSpeed, JumpForce;
    private float Speed;
    private Rigidbody2D rb;
    private bool isjumping;
    private bool doublejump;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Speed = OriginalSpeed;
    }

    void FixedUpdate(){
        Walk();
        Run();                                                                                             
    }

    void Update(){
        Jump();
    }

    void Walk(){
        //rb.velocity = new Vector2(Input.GetAxis("Horizontal"), 0f) * Speed;
        transform.position += new Vector3(Input.GetAxis("Horizontal"), 0f, 0f) * Speed * Time.deltaTime;

        if(Input.GetAxis("Horizontal") > 0f){
            if(!isjumping){
                animator.SetBool("walk", true);
            }
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }else if(Input.GetAxis("Horizontal") == 0f){
            animator.SetBool("walk", false);
        }else if(Input.GetAxis("Horizontal") < 0){
            if(!isjumping){
                animator.SetBool("walk", true);
            }
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    void Jump(){
        if(Input.GetButtonDown("Jump")){
            if(!isjumping){
                animator.SetBool("jump", true);
                rb.AddForce(new Vector2(/*Input.GetAxis("Horizontal") * Speed*/ 0f, JumpForce), ForceMode2D.Impulse);
                doublejump = true;
            }else if(doublejump){
                Doublejump();
            }
        }
    }

    void Doublejump(){
        rb.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        doublejump = false;
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.layer == 6){
            isjumping = false;
            animator.SetBool("jump", false);
        }    
    }

    void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.layer == 6){
            isjumping = true;
        }    
    }

    public void SetPosition(float x, float y, float z){
        transform.position = new Vector3(x, y, z);
    }

    void Run(){
        if(Input.GetAxis("Horizontal") * Input.GetAxis("Run") != 0){           
            Speed = OriginalSpeed * 1.5f;
        }else{
            Speed = OriginalSpeed;           
        }
    }
}