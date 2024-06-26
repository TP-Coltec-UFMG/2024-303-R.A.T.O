using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    private Rato rato;

    [SerializeField] private Slider RatoHealthBar;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private TMP_Text GameOverTextUI;
    [SerializeField] [TextArea(1, 10)] private string GameOverMessage;

    [HideInInspector] public float gama, audioVolume, musicVolume;
    [HideInInspector] public string left, right, jump, down, run, interact, fontColor, backgroundColor;
    [HideInInspector] public bool contrast, fullScreen;
    [HideInInspector] public int difficulty, fontSize;
    [HideInInspector] public Color _fontColor, _backgroundColor;

    void Awake()
    {
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        GetValues();
    }

    void OnDestroy(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update(){
        ShowHealth();
        ChangeTheme();
        ChangeFontSize(FixedFontSize(fontSize));
        AtivaPorta();
        DesativaBuraco();
    }

    public void ChangeScene(string SceneName){
        SceneManager.LoadScene(SceneName);
    }

    public void ChangeTheme(){
        TMP_Text[] changeThisColour = FindObjectsOfType<TMP_Text>();

        foreach (TMP_Text text in changeThisColour){
            if (text.tag == "ChangeableFont"){
                text.color = _fontColor;
            }
        }

        foreach (GameObject background in GameObject.FindGameObjectsWithTag("ChangeableBackground")){
            background.GetComponent<Image>().color = _backgroundColor;
        }
    }

    public void ChangeFontSize(int size){
        TMP_Text[] changeThisSize = FindObjectsOfType<TMP_Text>();

        foreach (TMP_Text element in changeThisSize){
            if (element.tag == "ChangeableFont"){
                element.fontSize = size;
            }
        }
    }

    void ShowHealth(){
        if (RatoHealthBar != null && rato != null){
            RatoHealthBar.maxValue = rato.MaxHealth;
            RatoHealthBar.value = rato.health;
        }
    }

    public void StopGame(){
        Time.timeScale = 0;
    }

    public void Resume(){
        Time.timeScale = 1;
    }

    public void GameOver(float RespawnX, float RespawnY){
        StartCoroutine(SetGameOver(RespawnX, RespawnY));
    }

    IEnumerator SetGameOver(float RespawnX, float RespawnY){
        rato = FindObjectOfType<Rato>();
        rato.dead = true;

        StopGame();

        if (GameOverPanel != null){
            GameOverPanel.GetComponent<Image>().enabled = true;
        }

        foreach (char c in GameOverMessage){ 
            GameOverTextUI.text += c;
            yield return new WaitForSecondsRealtime(0.01f);
        }

        while (!Input.GetKeyDown(KeyCode.Return)){
            yield return null;
        }

        Resume();
        if (GameOverPanel != null){
            GameOverPanel.GetComponent<Image>().enabled = false;
        }

        if (GameOverTextUI != null){
            GameOverTextUI.text = "";
        }

        if (rato != null){
            rato.transform.position = new Vector3(RespawnX, RespawnY, 0);
            rato.ResetLife();
            rato.dead = false;
        }

        foreach (Gato gato in FindObjectsOfType<Gato>()){
            gato.ResetLife();
        }
    }

    public int FixedFontSize(int size){
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
                size = 35;
                break;
        }

        return size;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        rato = FindObjectOfType<Rato>();

        if (scene.name == "casateste"){
            RatoHealthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
            GameOverPanel = GameObject.Find("GameOverPanel");
            GameOverTextUI = GameObject.Find("GameOverTextUI").GetComponent<TMP_Text>();

            if (GameOverPanel != null){
                GameOverPanel.GetComponent<Image>().enabled = false;
            }
        }
    }

    public void SetNewSceneOnKeyPress(string sceneName, KeyCode keyCode){
        StartCoroutine(WaitForKeyPress(sceneName, keyCode));
    }

    private IEnumerator WaitForKeyPress(string sceneName, KeyCode keyCode){
        while (!Input.GetKeyDown(keyCode)){
            yield return null;
        }
        
        ChangeScene(sceneName);
    }

    void GetValues(){
        gama = SavePrefs.GetFloat("gama");
        audioVolume = SavePrefs.GetFloat("audioVolume");
        musicVolume = SavePrefs.GetFloat("musicVolume");
        right = SavePrefs.GetString("right");
        left = SavePrefs.GetString("left");
        jump = SavePrefs.GetString("jump");
        down = SavePrefs.GetString("down");
        run = SavePrefs.GetString("run");
        interact = SavePrefs.GetString("interact");

        if(SavePrefs.HasKey("difficulty")) {
            difficulty = SavePrefs.GetInt("difficulty");
        }else{
            difficulty = 1;
        }

        if(SavePrefs.HasKey("fontSize")) {
            fontSize = SavePrefs.GetInt("fontSize");
        }else{
            fontSize = 2;
        }

        if(SavePrefs.HasKey("fontColor")){
            fontColor = SavePrefs.GetString("fontColor");
            ColorUtility.TryParseHtmlString("#" + fontColor, out _fontColor);
        }else{
            fontColor = "FFFFFF";
            ColorUtility.TryParseHtmlString("#" + fontColor, out _fontColor);
        }

        if(SavePrefs.HasKey("backgroundColor")){
            backgroundColor = SavePrefs.GetString("backgroundColor");
            ColorUtility.TryParseHtmlString("#" + backgroundColor, out _backgroundColor);
        }else{
            backgroundColor = "000000";
            ColorUtility.TryParseHtmlString("#" + backgroundColor, out _backgroundColor);
        }
        //ColourPickerPanel.GetComponent<ColourPickerController>().SetCurrentColour(GameController.Instance.color);
    }

    void AtivaPorta(){
        if(GameObject.FindGameObjectsWithTag("Gato").Length == 0 && GameObject.FindGameObjectWithTag("Porta") != null){
            GameObject.FindGameObjectWithTag("Porta").GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    void DesativaBuraco(){
        if(GameObject.FindGameObjectsWithTag("Gato").Length != 2 && GameObject.FindGameObjectWithTag("Buraco") != null){
            GameObject.FindGameObjectWithTag("Buraco").GetComponent<BoxCollider2D>().enabled = false;
            //GameObject.FindGameObjectWithTag("Freeze").GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
