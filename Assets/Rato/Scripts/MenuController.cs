using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string FirstScene;
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject AcessibilidadeMenu;
    [SerializeField] private GameObject ConfiguracoesMenu;
    [SerializeField] private GameObject CreditosPanel;
    private GameObject CurrentPanel;

    public void Jogar(){
        SceneManager.LoadScene(FirstScene);
    }

    public void Acessibilidade(){
        this.MainMenu.SetActive(false);
        this.AcessibilidadeMenu.SetActive(true);
        this.CurrentPanel = AcessibilidadeMenu;
    }

    public void Configuracoes(){
        this.MainMenu.SetActive(false);
        this.ConfiguracoesMenu.SetActive(true);
        this.CurrentPanel = ConfiguracoesMenu;
    }

    public void Creditos(){
        this.MainMenu.SetActive(false);
        this.CreditosPanel.SetActive(true);
        this.CurrentPanel = CreditosPanel;
    }

    public void Back(){
        this.CurrentPanel.SetActive(false);
        this.MainMenu.SetActive(true);
    }
}
