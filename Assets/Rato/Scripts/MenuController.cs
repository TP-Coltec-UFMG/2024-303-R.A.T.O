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

    /*private float gama, audioVolume, musicVolume; 
    private string left, right, jump, down, run, interact, fontColor;
    private bool contrast, fullScreen;
    private int difficulty, fontSize;
    private Color color;*/

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
            GameController.Instance.ChangeScene(this.FirstScene);       
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
        SavePrefs.SaveFloat("gama", GameController.Instance.gama);
        SavePrefs.SaveInt("difficulty", GameController.Instance.difficulty);
        SavePrefs.SaveInt("fontSize", GameController.Instance.fontSize);
        SavePrefs.SaveFloat("audioVolume", GameController.Instance.audioVolume);
        SavePrefs.SaveFloat("musicVolume", GameController.Instance.musicVolume);
        SavePrefs.SaveString("right", GameController.Instance.right);
        SavePrefs.SaveString("left", GameController.Instance.left);
        SavePrefs.SaveString("jump", GameController.Instance.jump);
        SavePrefs.SaveString("down", GameController.Instance.down);
        SavePrefs.SaveString("run", GameController.Instance.run);
        SavePrefs.SaveString("interact", GameController.Instance.interact);
        SavePrefs.SaveString("fontColor", GameController.Instance.fontColor);                
    }

    void GetValues(){
        GameController.Instance.gama = SavePrefs.GetFloat("gama");
        GameController.Instance.difficulty = SavePrefs.GetInt("difficulty");
        GameController.Instance.audioVolume = SavePrefs.GetFloat("audioVolume");
        GameController.Instance.musicVolume = SavePrefs.GetFloat("musicVolume");
        GameController.Instance.right = SavePrefs.GetString("right");
        GameController.Instance.left = SavePrefs.GetString("left");
        GameController.Instance.jump = SavePrefs.GetString("jump");
        GameController.Instance.down = SavePrefs.GetString("down");
        GameController.Instance.run = SavePrefs.GetString("run");
        GameController.Instance.interact = SavePrefs.GetString("interact");

        if(SavePrefs.HasKey("fontSize")) {
            GameController.Instance.fontSize = SavePrefs.GetInt("fontSize");
        }else{
            GameController.Instance.fontSize = 14;
        }
        TamanhoFonteDropdown.value = GameController.Instance.fontSize;

        if(SavePrefs.HasKey("fontColor")){
            GameController.Instance.fontColor = SavePrefs.GetString("fontColor");
            ColorUtility.TryParseHtmlString("#" + GameController.Instance.fontColor, out GameController.Instance.color);
        }else{
            GameController.Instance.fontColor = "FFFFFF";
            ColorUtility.TryParseHtmlString("#" + GameController.Instance.fontColor, out GameController.Instance.color);
        }
        ColourPickerPanel.GetComponent<ColourPickerController>().SetCurrentColour(GameController.Instance.color);
    }

    public void SetFontSize(){
        GameController.Instance.fontSize = TamanhoFonteDropdown.value;
    }

    public void SetFontColor(){
        GameController.Instance.fontColor = ColorUtility.ToHtmlStringRGBA(ColourPickerPanel.GetComponent<ColourPickerController>().GetCurrentColour());
        ColorUtility.TryParseHtmlString("#" + GameController.Instance.fontColor, out GameController.Instance.color);
    }
}
