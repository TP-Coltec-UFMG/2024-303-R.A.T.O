using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContrastFilter : MonoBehaviour
{
    private Color OriginalColor;
    [SerializeField] private Color ContrastColor;

    // Start is called before the first frame update
    void Start()
    {
        this.OriginalColor = GetComponent<SpriteRenderer>().color;
    }

    public void SetContrast(bool v){
        if(v){
            GetComponent<SpriteRenderer>().color = this.ContrastColor;
        }else{
            GetComponent<SpriteRenderer>().color = this.OriginalColor;
        }
    }
}
