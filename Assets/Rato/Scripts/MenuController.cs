using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string FirstScene;
    [SerializeField] private GameObject MainMenu, JogarMenu, AcessibilidadeMenu, ConfiguracoesMenu, CreditosPanel, NovoJogoMenu, Message, ControlesMenu, TemasPanel, Submenus;
    private GameObject CurrentPanel;
    private string BackTo;

    [SerializeField] private Scrollbar ControleGamaScrollbar, VolumeAudioScrollbar, VolumeMusicaScrollbar;
    [SerializeField] private TMP_Dropdown DificuldadeDropdown, TamanhoFonteDropdown;
    [SerializeField] private TMP_InputField EsquerdaInputText, DireitaInputText, PularInputText, AbaixarInputText, CorrerInputText, InteragirInputText;
    [SerializeField] private Toggle ContrasteToggle, TelaCheiaToggle;

    void Awake(){
        InitialChanges();
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

    public void Temas(){
        this.TemasPanel.SetActive(true);
        this.CurrentPanel = TemasPanel;
        this.BackTo = "Acessibilidade";
    }

    public void SaveValues(){
        SavePrefs.SaveBool("contrast", GameController.Instance.contrast);
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
        SavePrefs.SaveString("backgroundColor", GameController.Instance.backgroundColor);                
    }

    void InitialChanges(){
        TamanhoFonteDropdown.value = GameController.Instance.fontSize;
        DificuldadeDropdown.value = GameController.Instance.difficulty;
        ContrasteToggle.isOn = GameController.Instance.contrast;
    }
    public void SetContrast(){
        GameController.Instance.contrast = ContrasteToggle.isOn;
    }

    public void SetDifficulty(){
        GameController.Instance.difficulty = DificuldadeDropdown.value;
    }

    public void SetFontSize(){
        GameController.Instance.fontSize = TamanhoFonteDropdown.value;
    }

    public void SetTheme1(){
        GameController.Instance.fontColor = "FFFFFF";
        ColorUtility.TryParseHtmlString("#" + GameController.Instance.fontColor, out GameController.Instance._fontColor);

        GameController.Instance.backgroundColor = "000000";
        ColorUtility.TryParseHtmlString("#" + GameController.Instance.backgroundColor, out GameController.Instance._backgroundColor);
    }

    public void SetTheme2(){
        GameController.Instance.fontColor = "F300FF";
        ColorUtility.TryParseHtmlString("#" + GameController.Instance.fontColor, out GameController.Instance._fontColor);

        GameController.Instance.backgroundColor = "00FFC2";
        ColorUtility.TryParseHtmlString("#" + GameController.Instance.backgroundColor, out GameController.Instance._backgroundColor);
    }

    public void SetTheme3(){
        GameController.Instance.fontColor = "A0B600";
        ColorUtility.TryParseHtmlString("#" + GameController.Instance.fontColor, out GameController.Instance._fontColor);

        GameController.Instance.backgroundColor = "FFFFFF";
        ColorUtility.TryParseHtmlString("#" + GameController.Instance.backgroundColor, out GameController.Instance._backgroundColor);
    }
}
