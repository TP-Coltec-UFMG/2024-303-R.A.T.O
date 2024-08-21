using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creditos : MonoBehaviour
{
    [SerializeField] public bool restart;

    public void ExitCreditos(){
        if(restart){
            GameController.Instance.ChangeScene("Menu", new Vector3(0, 0, 0));
        }else{
            MenuController.Instance.Back();
        }
    }
}
