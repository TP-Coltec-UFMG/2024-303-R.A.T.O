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
    private string left, right, jump, down, run, interact;
    private bool contrast, fullScreen;
    private Color fontColor;
    private int difficulty, fontSize;

    [SerializeField] private Scrollbar ControleGamaScrollbar, VolumeAudioScrollbar, VolumeMusicaScrollbar;
    [SerializeField] private TMP_Dropdown DificuldadeDropdown, TamanhoFonteDropdown;
    [SerializeField] private TMP_InputField EsquerdaInputText, DireitaInputText, PularInputText, AbaixarInputText, CorrerInputText, InteragirInputText;
    [SerializeField] private Toggle ContrasteToggle, TelaCheiaToggle; 

    void Start(){
        //GetValues();
    }
        
    void Update(){
        //ApplyChanges();
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

    /*public void SetValues(){
        gama = ControleGamaScrollbar.value;
        difficulty = DificuldadeDropdown.value;
        fontSize = TamanhoFonteDropdown.value;
        audioVolume = VolumeAudioScrollbar.value;
        musicVolume = VolumeMusicaScrollbar.value;
        right = DireitaInputText.text;
        left = EsquerdaInputText.text;
        jump = PularInputText.text;
        down = AbaixarInputText.text;
        run = CorrerInputText.text;
        interact = InteragirInputText.text;
        fontColor = ColourPickerPanel.GetComponent<ColourPickerController>().GetCurrentColour();        
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
        SavePrefs.SaveString("fontColor", ColorUtility.ToHtmlStringRGB(fontColor));                
    }

    void GetValues(){
        gama = SavePrefs.GetFloat("gama");
        difficulty = SavePrefs.GetInt("difficulty");
        fontSize = SavePrefs.GetInt("fontSize");
        audioVolume = SavePrefs.GetFloat("audioVolume");
        musicVolume = SavePrefs.GetFloat("musicVolume");
        right = SavePrefs.GetString("right");
        left = SavePrefs.GetString("left");
        jump = SavePrefs.GetString("jump");
        down = SavePrefs.GetString("down");
        run = SavePrefs.GetString("run");
        interact = SavePrefs.GetString("interact");
        ColorUtility.TryParseHtmlString(SavePrefs.GetString("fontColor"), out fontColor);
        ApplyChanges();
    }

    public void ApplyChanges(){
        ControleGamaScrollbar.value = gama;
        DificuldadeDropdown.value = difficulty;
        TamanhoFonteDropdown.value = fontSize;
        VolumeAudioScrollbar.value = audioVolume;
        VolumeMusicaScrollbar.value = musicVolume;
        DireitaInputText.text = right;
        EsquerdaInputText.text = left;
        PularInputText.text = jump;
        AbaixarInputText.text = down;
        CorrerInputText.text = run;
        InteragirInputText.text = interact;
        
        TMP_Text[] changeThisColour = FindObjectsOfType<TMP_Text>();
        foreach (TMP_Text element in changeThisColour){
            Color newColor;
            ColorUtility.TryParseHtmlString(SavePrefs.GetString("fontColor"), out newColor);
            element.color = newColor;
        }
    }*/
}
