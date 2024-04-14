using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Camera cam;
    float v;
    public Transform Player;
    public float Speed;
    public float maxLimit;
    public float minLimit;
    Transform autoZoomTarget;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    void FixedUpdate(){
        FollowPlayer();
        Zoom(15f, 2f, Input.GetAxis("Zoom") * 0.5f);
        StopInLimits();
    }

    public void FollowPlayer(){
        transform.position = Vector2.Lerp(transform.position, Player.position, Speed);
    }

    public void Zoom(float maxSize, float minSize, float zoomSpeed){
        cam.orthographicSize -= zoomSpeed;

        if(cam.orthographicSize > maxSize){
             cam.orthographicSize = maxSize;
        }else if(cam.orthographicSize < minSize){
            cam.orthographicSize = minSize;
        }
    }

    void StopInLimits(){
        if((transform.position.x + cam.orthographicSize) > maxLimit){
            transform.position = new Vector3(maxLimit - cam.orthographicSize, transform.position.y, transform.position.z);
        }else if((transform.position.x - cam.orthographicSize) < minLimit){
            transform.position = new Vector3(minLimit + cam.orthographicSize, transform.position.y, transform.position.z);
        }
    }
} 