using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rato : MonoBehaviour
{
    [SerializeField] private float OriginalSpeed, JumpForce, Damage, RespawnX, RespawnY;
    private float Speed;
    private Rigidbody2D rb;
    private bool isjumping;
    private bool doublejump;
    private Animator animator;
    private bool isBiting;
    [SerializeField] public float MaxHealth; 
    public float health {get; private set;}
    private GameObject attack;
    public bool dead {get; set;}

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Speed = OriginalSpeed; 
    }

    void Awake(){
        health = MaxHealth;
        dead = false;
    }

    void FixedUpdate(){
        Walk();
        Run();                                                                                             
    }

    void Update(){
        Jump();
        Bite();
        if(this.health <= 0 && !dead){
            Die();
        }
    }

    void Walk(){
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
                rb.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
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

    void Bite(){
        if(Input.GetButtonDown("Fire") && !isBiting){
            animator.SetTrigger("bite");
        }else{
            animator.ResetTrigger("bite");
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.layer == 6 || collision.gameObject.layer == 9 ||collision.gameObject.tag == "Gato"){
            isjumping = false;
            animator.SetBool("jump", false);
        }

        if(collision.gameObject.tag == "Gato"){
            attack = collision.gameObject;
        }    
    }

    void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.layer == 6){
            isjumping = true;
        }
        
        attack = null;    
    }

    public void SetPosition(float x, float y, float z){
        transform.position = new Vector3(x, y, z);
    }

    void Run(){
        if(Input.GetAxis("Horizontal") * Input.GetAxis("Run") != 0){           
            Speed = OriginalSpeed * 1.5f;
            animator.speed = Speed / OriginalSpeed;
        }else{
            Speed = OriginalSpeed;           
        } 
    }

    public void Attack(){
        if(attack != null){
            attack.GetComponent<Gato>().TakeDamage(this.Damage);
        }      
    }

    public void TakeDamage(float damage){
        this.health -= damage;
    }

    void Die(){
        GameController.Instance.GameOver(RespawnX, RespawnY);
    }

    public void ResetLife(){
        health = MaxHealth;
    }
}