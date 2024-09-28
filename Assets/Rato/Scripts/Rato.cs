using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rato : MonoBehaviour
{
    [SerializeField] private float OriginalSpeed, aOriginalSpeed, JumpForce, aJumpForce, Damage, RespawnX, RespawnY;
    private float Speed;
    private Rigidbody2D rb;
    private bool isjumping;
    private bool doublejump;
    private Animator animator;
    private bool isBiting, flip, canRun;
    [SerializeField] public float MaxHealth; 
    public float health;
    private GameObject attack;
    //[SerializeField] private GameObject ContrastFilter;
    public bool dead {get; set;}

    private float moveInput;
    private bool jumpInput;
    private bool interactInput;
    private bool attackInput;
    private float runInput;

    [SerializeField] private AudioClip AttackSound;

    // Start is called before the first frame update
    void Start()
    {
         
    }

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        aOriginalSpeed = OriginalSpeed;
        OriginalSpeed = 0;
        Speed = 0;
        aJumpForce = JumpForce;
        JumpForce = 0;
        health = MaxHealth;
        dead = false;
        flip = false;
        canRun = false;
    }

    void FixedUpdate(){
        Walk();
        if(canRun){
            Run(); 
        }                                                                                            
    }

    void Update(){
        Jump();
        Bite();
        if(this.health <= 0 && !dead){
            Die();
        }
    }

    void Walk(){
        this.moveInput = UserInput.Instance.MoveInput.x;

        transform.position += new Vector3(moveInput, 0f, 0f) * Speed * Time.deltaTime;

        if(flip){
            if(moveInput > 0f){
                if(!isjumping){
                    animator.SetBool("walk", true);
                }
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }else if(moveInput == 0f){
                animator.SetBool("walk", false);
            }else if(moveInput < 0){
                if(!isjumping){
                    animator.SetBool("walk", true);
                }
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
        }
    }

    void Jump(){
        jumpInput = UserInput.Instance.JumpInput;
        if(jumpInput){
            if(!isjumping){
                animator.SetTrigger("jump");
                rb.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doublejump = true;
            }else if(doublejump){
                Doublejump();
            }
        }
    }

    void Doublejump(){
        //animator.SetTrigger("jump");
        rb.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        doublejump = false;
    }

    void Bite(){
        attackInput = UserInput.Instance.AttackInput;

        if(attackInput && !isBiting){
            animator.SetTrigger("bite");
        }else{
            animator.ResetTrigger("bite");
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.layer == 6 || collision.gameObject.layer == 9 /*||collision.gameObject.tag == "Gato"*/){
            isjumping = false;
            //animator.SetBool("jump", false);
        }

        if(collision.gameObject.tag == "Gato"){
            attack = collision.gameObject;
        }

        Velha velha = FindObjectOfType<Velha>();
        if(velha != null){
            if(collision.gameObject.tag == "Barrier"){
                velha.walk = true;
            }
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
        runInput = UserInput.Instance.RunInput;
        if(this.runInput != 0 && this.moveInput != 0){           
            Speed = OriginalSpeed * 1.9f;
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
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut(){
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().enabled = true;
    }

    void Die(){
        GameController.Instance.GameOver(RespawnX, RespawnY);
    }

    public void ResetLife(){
        health = MaxHealth;
    }

    /*public void SetContrast(bool v){
        this.ContrastFilter.SetActive(v);
    }*/

    public void SetAwake(){
        GetComponent<Animator>().SetBool("awake", true);
        OriginalSpeed = aOriginalSpeed;       
        Speed = OriginalSpeed;
        JumpForce = aJumpForce;
        flip = true;
        canRun = true;
    }

    public void PlayAudio(AudioClip audio){
        GetComponent<AudioSource>().PlayOneShot(audio);
    }
}