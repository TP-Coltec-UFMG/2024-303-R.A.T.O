using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string FirstScene;
    [SerializeField] private GameObject MainMenu, JogarMenu, AcessibilidadeMenu, ConfiguracoesMenu, CreditosPanel, NovoJogoMenu, Message, ControlesMenu, ColourPickerPanel, Submenus;
    private GameObject CurrentPanel;
    private string BackTo;

    private float gama, audioVolume, musicVolume; 
    private string left, right, jump, down, run, interact, fontColor;
    private bool contrast, fullScreen;
    private int difficulty, fontSize;

    [SerializeField] private Scrollbar ControleGamaScrollbar, VolumeAudioScrollbar, VolumeMusicaScrollbar;
    [SerializeField] private TMP_Dropdown DificuldadeDropdown, TamanhoFonteDropdown;
    [SerializeField] private TMP_InputField EsquerdaInputText, DireitaInputText, PularInputText, AbaixarInputText, CorrerInputText, InteragirInputText;
    [SerializeField] private Toggle ContrasteToggle, TelaCheiaToggle; 

    void Awake(){
        GetValues();
    }
    
    public void Main(){
        this.MainMenu.SetActive(true);
    }

    public void Jogar(){
        this.MainMenu.SetActive(false);
        this.JogarMenu.SetActive(true);
        this.CurrentPanel = JogarMenu;
        this.BackTo = "Main";
    }

    public void Acessibilidade(){
        this.MainMenu.SetActive(false);
        this.AcessibilidadeMenu.SetActive(true);
        this.CurrentPanel = AcessibilidadeMenu;
        this.BackTo = "Main";
    }

    public void Configuracoes(){
        this.MainMenu.SetActive(false);
        this.ConfiguracoesMenu.SetActive(true);
        this.CurrentPanel = ConfiguracoesMenu;
        this.BackTo = "Main";
    }

    public void Creditos(){
        this.MainMenu.SetActive(false);
        this.CreditosPanel.SetActive(true);
        this.CurrentPanel = CreditosPanel;
        this.BackTo = "Main";
    }

    public void Back(){
        this.CurrentPanel.SetActive(false);
        Invoke(BackTo, 0f);
    }

    public void NovoJogo(){
        if(SaveAndLoad.LoadData() != null){
            this.NovoJogoMenu.SetActive(true);
            this.CurrentPanel = NovoJogoMenu;
            this.BackTo = "Jogar";
        }else{
            //carrega primeira cena       
        }
    }

    public void Continuar(){
        if(SaveAndLoad.LoadData() != null){
            //carrega jogo de onde parou
        }else{
            this.Message.SetActive(true);
            StartCoroutine(DisableMessage());       
        }
    }

    IEnumerator DisableMessage(){
        yield return new WaitForSeconds(0.5f);
        this.Message.SetActive(false);
    }

    public void Controles(){
        this.ControlesMenu.SetActive(true);
        this.CurrentPanel = ControlesMenu;
        this.BackTo = "Acessibilidade";
    }

    public void ColourPicker(){
        this.ColourPickerPanel.SetActive(true);
        this.CurrentPanel = ColourPickerPanel;
        this.BackTo = "Acessibilidade";
    }

    public void SaveValues(){
        SavePrefs.SaveFloat("gama", gama);
        SavePrefs.SaveInt("difficulty", difficulty);
        SavePrefs.SaveInt("fontSize", fontSize);
        SavePrefs.SaveFloat("audioVolume", audioVolume);
        SavePrefs.SaveFloat("musicVolume", musicVolume);
        SavePrefs.SaveString("right", right);
        SavePrefs.SaveString("left", left);
        SavePrefs.SaveString("jump", jump);
        SavePrefs.SaveString("down", down);
        SavePrefs.SaveString("run", run);
        SavePrefs.SaveString("interact", interact);
        SavePrefs.SaveString("fontColor", fontColor);                
    }

    void GetValues(){
        this.gama = SavePrefs.GetFloat("gama");
        this.difficulty = SavePrefs.GetInt("difficulty");
        this.audioVolume = SavePrefs.GetFloat("audioVolume");
        this.musicVolume = SavePrefs.GetFloat("musicVolume");
        this.right = SavePrefs.GetString("right");
        this.left = SavePrefs.GetString("left");
        this.jump = SavePrefs.GetString("jump");
        this.down = SavePrefs.GetString("down");
        this.run = SavePrefs.GetString("run");
        this.interact = SavePrefs.GetString("interact");

        this.fontSize = SavePrefs.GetInt("fontSize");
        this.TamanhoFonteDropdown.value = fontSize;
        GameController.ChangeFontSize(FixedFontSize(fontSize));

        this.fontColor = SavePrefs.GetString("fontColor");
        Color color;
        ColorUtility.TryParseHtmlString("#" + fontColor, out color);        
        ColourPickerPanel.GetComponent<ColourPickerController>().SetCurrentColour(color);
        GameController.ChangeFontColor(color);
    }

    public void SetFontSize(){
        this.fontSize = this.TamanhoFonteDropdown.value;
        GameController.ChangeFontSize(FixedFontSize(fontSize));
    }

    int FixedFontSize(int size){
        switch (size){
            case 0:
                size = 30;
                break;
            case 1:
                size = 35;
                break;
            case 2:
                size = 40;
                break;
            default:
                size = 40;
                break;
        }

        return size;
    }

    public void SetFontColor(){
        this.fontColor = ColorUtility.ToHtmlStringRGBA(ColourPickerPanel.GetComponent<ColourPickerController>().GetCurrentColour());
        Color color;
        ColorUtility.TryParseHtmlString("#" + this.fontColor, out color);
        GameController.ChangeFontColor(color);
    }
}
