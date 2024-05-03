using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoZoomTarget : MonoBehaviour
{
    [SerializeField] private float VerticalRange, ZoomRange, RangeLeft, RangeRight, FinalSize;
    void FixedUpdate()
    {
        CameraController.instance.AutoZoom(VerticalRange, ZoomRange, RangeLeft, RangeRight, FinalSize, this.transform);
    }
}
