using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Paralaxe : MonoBehaviour
{
    private float comprimento;
    private float Posinicio;
    [SerializeField] private GameObject cam;
    [SerializeField] private float EfeitoParalaxe;

    // Start is called before the first frame update
    void Start()
    {
        Posinicio = transform.position.x; //pega o x do objeto
        comprimento = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distancia = (cam.transform.position.x *(1-EfeitoParalaxe));//posição da camera multiplicada pelo efeito
        float lugar = (cam.transform.position.x *EfeitoParalaxe);
        transform.position = new Vector3(Posinicio+ distancia, transform.position.y, transform.position.z);

        /*if(lugar>Posinicio+comprimento){
            Posinicio+=comprimento;
        }
        else if(lugar>Posinicio-comprimento){
            Posinicio-=comprimento;
        }*/
    }
}

/*public class Paralaxe : MonoBehaviour {
    [SerializeField] private float velocidade;
    private Vector3 posicaoInicial;
    private float tamanhoRealDaImagem ;
    private void Awake(){
        this.posicaoInicial = this.transform.position;
        float tamanhoDaImagem =

        this.GetComponent<SpriteRenderer>().size.x;
        float escala = this.transform.localScale.x;
        this.tamanhoRealDaImagem = tamanhoDaImagem * escala;
    }

    void Update() {
        float deslocamento = Mathf.Repeat(this.velocidade * Time.time, this.tamanhoRealDaImagem);
        this.transform.position = this.posicaoInicial + Vector3.left * deslocamento;
    }
}*/
