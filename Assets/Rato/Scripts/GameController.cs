using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    private Rato rato;
    private Vector3 ratoPosition;

    [SerializeField] private Slider RatoHealthBar;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private TMP_Text GameOverTextUI;
    [SerializeField] [TextArea(1, 10)] private string GameOverMessage;

    [HideInInspector] public float audioVolume, musicVolume;
    [HideInInspector] public string left, right, jump, down, run, interact, fontColor, backgroundColor;
    [HideInInspector] public bool contrast, fullScreen;
    [HideInInspector] public int difficulty, fontSize;
    [HideInInspector] public Color _fontColor, _backgroundColor;

    void Start(){
        ratoPosition = new Vector3(100f, 100f, 100f);
    }

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
        ChangeContrast();
    }

    public void ChangeScene(string SceneName){
        SceneManager.LoadScene(SceneName);
    }

    public void ChangeContrast(){
        Rato rato = FindObjectOfType<Rato>();
        if(rato != null){
            rato.SetContrast(contrast);
        }

        MazeRato mazeRato = FindObjectOfType<MazeRato>();
        if(mazeRato != null){
            mazeRato.SetContrast(contrast);
        }

        Gato[] gatos = FindObjectsOfType<Gato>();
        foreach (Gato gato in gatos){
            gato.SetContrast(contrast);
        }
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
            yield return new WaitForSecondsRealtime(0.05f);
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
    
        if(GameObject.Find("HealthBar") != null){
            RatoHealthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        }
        
        if(GameObject.Find("GameOverTextUI")){
            GameOverTextUI = GameObject.Find("GameOverTextUI").GetComponent<TMP_Text>();
        }

        GameOverPanel = GameObject.Find("GameOverPanel");
        if (GameOverPanel != null){
            GameOverPanel.GetComponent<Image>().enabled = false;
        }

        if(SceneManager.GetActiveScene().buildIndex == SaveAndLoad.LoadData().currentScene && ratoPosition != new Vector3(100f, 100f, 100f)){
            Debug.Log("a");
            rato.transform.position = new Vector3(SaveAndLoad.LoadData().currentPositionX, SaveAndLoad.LoadData().currentPositionY, 0);
            rato.GetComponent<Animator>().SetBool("startAwake", false);
            rato.GetComponent<Animator>().SetBool("awake", false);
        }else if(scene.name == "casateste"){
            Debug.Log("b");
            rato.GetComponent<Animator>().SetBool("startAwake", false);        
        }else{
            Debug.Log("c");
            if(rato != null){
                rato.GetComponent<Animator>().SetBool("startAwake", true);
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
        audioVolume = SavePrefs.GetFloat("audioVolume");
        musicVolume = SavePrefs.GetFloat("musicVolume");
        right = SavePrefs.GetString("right");
        left = SavePrefs.GetString("left");
        jump = SavePrefs.GetString("jump");
        down = SavePrefs.GetString("down");
        run = SavePrefs.GetString("run");
        interact = SavePrefs.GetString("interact");

        if(SavePrefs.HasKey("contrast")) {
            contrast = SavePrefs.GetBool("contrast");
        }else{
            contrast = false;
        }

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

    public void Continue(){
        SceneManager.LoadScene(SaveAndLoad.LoadData().currentScene);
        Debug.Log("x: " + SaveAndLoad.LoadData().currentPositionX);
        Debug.Log("y: " + SaveAndLoad.LoadData().currentPositionY);
    }

    public void Save(Vector3 position){
        rato = FindObjectOfType<Rato>();
        SaveAndLoad.SaveData(new Data(rato.transform.position.x, rato.transform.position.y, SceneManager.GetActiveScene().buildIndex));
    }
}
