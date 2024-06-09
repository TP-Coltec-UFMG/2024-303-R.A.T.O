using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRato : MonoBehaviour
{
    [SerializeField] private float Speed;
    
    void Start()
    {
        this.transform.position = FindObjectOfType<RandomMazeGenerator>().Entrance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Walk();    
    }

    void Walk(){
        //rb.velocity = new Vector2(Input.GetAxis("Horizontal"), 0f) * Speed;
        transform.position += new Vector3(Input.GetAxis("Horizontal"), 0f, 0f) * this.Speed * Time.deltaTime;
        transform.position += new Vector3(0f, Input.GetAxis("Vertical"), 0f) * this.Speed * Time.deltaTime;
    }
}
