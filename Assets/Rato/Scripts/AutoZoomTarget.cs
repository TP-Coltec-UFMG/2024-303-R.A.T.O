using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoZoomTarget : MonoBehaviour
{
    public float Distance;
    public float Area;
    public Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       // AutoZoom();
    }
    void AutoZoom(){
        if((Player.position.y <= (transform.position.y + Area / 2f)) && (Player.position.y >= (transform.position.y - Area / 2f))){
            if(Player.position.x >= transform.position.x - Distance){
                    CameraController.instance.Zoom(15f, Area / 2f, 3f * Time.deltaTime);
                }
        }
    }
}
