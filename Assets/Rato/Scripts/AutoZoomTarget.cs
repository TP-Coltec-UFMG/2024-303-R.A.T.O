using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoZoomTarget : MonoBehaviour
{
    [SerializeField] private float VerticalRange, ZoomRange, RangeLeft, RangeRight, FinalSize;
    public static AutoZoomTarget instance;

    void Start(){
        instance = this;
    }

    void FixedUpdate()
    {
        CameraController.instance.AutoZoom(VerticalRange, ZoomRange, RangeLeft, RangeRight, FinalSize, this.transform);
    }
}
