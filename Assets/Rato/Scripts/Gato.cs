using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gato : MonoBehaviour
{
    private Transform Rato;
    private Animator animator;
    [SerializeField] private float RunRange, Damage, queijoOffset; 
    public float AttackRange;
    [SerializeField] public float MaxHealth; 
    public float health {get; private set;}
    private bool attack; 
    public bool dead {get; private set;}
    public bool isjumping {get; private set;}
    private float posY;
    [SerializeField] private bool rotate;
    [SerializeField] GameObject Queijo, ContrastFilter, buraco;

    // Start is called before the first frame update
    void Start()
    {
        Rato = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        MaxHealth *= (GameController.Instance.difficulty + 1);
        Damage *= (GameController.Instance.difficulty + 1);
        health = MaxHealth;   
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Die();   
    }

    public void LookAtPlayer(){ 
        if(Rato.position.x > transform.position.x){
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }else if(Rato.position.x < transform.position.x){
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        if(this.rotate){
            if(Rato.position.x > transform.position.x){
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }else if(Rato.position.x < transform.position.x){
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
        }
    }

    public void Run(){
        if(Rato.position.x - transform.position.x > RunRange || Rato.position.x - transform.position.x < -RunRange){
            animator.SetBool("run", true);
        }else{
            animator.SetBool("run", false);
        }
    }

    public void Attack(){
        if(attack){
            FindObjectOfType<Rato>().TakeDamage(this.Damage);
        }      
    }

    public void TakeDamage(float damage){
        this.health -= damage;
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player"){
            attack = true;
        }

        if(collision.gameObject.layer == 6 || collision.gameObject.layer == 9){
            isjumping = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision){
        attack = false;
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Freeze" || collider.gameObject.tag == "TFreeze"){
            animator.SetBool("idle", true);
        }
    }

    void OnTriggerExit2D(){
        animator.SetBool("idle", false);
    }

    void Die(){
        if(this.health == 0){
            this.dead = true;
            animator.SetTrigger("die");
        }
    }

    void SetQueijo(){
        Queijo.transform.position = new Vector3(transform.position.x, transform.position.y - queijoOffset, transform.position.z);
        Queijo.transform.eulerAngles = transform.eulerAngles;
    }

    void TurnIntoQueijo(){
        SetQueijo();
        Queijo.SetActive(true);
        Destroy(gameObject);
    }

    public void ResetLife(){
        health = MaxHealth;
    }

    public void SetContrast(bool v){
        this.ContrastFilter.SetActive(v);
    }

    public void DesativaBuraco(){
        this.buraco.SetActive(false);
    }
}
