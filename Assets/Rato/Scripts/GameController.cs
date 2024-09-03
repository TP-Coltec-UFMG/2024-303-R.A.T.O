using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    private Rato rato;
    private static int ratoHumanity = 0;
    private static float ratoHealth;
    private static Vector3 ratoPosition = new Vector3(0, 0, 0);

    public void IncreaseRatoHumanity(){
        ratoHumanity++;
        Debug.Log(ratoHumanity);
    }
    public int GetRatoHumanity(){
        return ratoHumanity;
    }

    public float GetRatoHealth(){
        return ratoHealth;
    }

    public void SetRatoHealth(float h){
        ratoHealth = h;
    }

    [HideInInspector] public static bool loadSavedData;

    public void SetLoadSavedData(bool v){
        loadSavedData = v;
    }

    [SerializeField] private Slider RatoHealthBar;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private TMP_Text GameOverTextUI;
    [SerializeField] [TextArea(1, 10)] private string GameOverMessage;

    [HideInInspector] public float audioVolume, musicVolume;
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
        rato = FindObjectOfType<Rato>();
    }

    void OnDestroy(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update(){
        ShowHealth();
        ChangeTheme();
        ChangeFontSize(FixedFontSize(fontSize));
        AtivaPorta();
        ChangeContrast();

        if(MenuController.Instance != null && MenuController.Instance.tag == "MenuInGame" && Input.GetKeyDown(KeyCode.H)){
            MenuController.Instance.OpenMenuInGame();
        }

        if(rato != null && rato.health != 0){
            ratoHealth = rato.health;
        }
    }

    public void ChangeScene(string SceneName, Vector3 rp){
        SceneManager.LoadScene(SceneName);
        ratoPosition = rp;
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
        if(rato != null){
            rato.dead = true;
        }

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

        /*if (rato != null){
            rato.transform.position = new Vector3(RespawnX, RespawnY, 0);
            rato.ResetLife();
            rato.dead = false;
        }

        foreach (Gato gato in FindObjectsOfType<Gato>()){
            gato.ResetLife();
        }*/
        
        if(SaveAndLoad.LoadData() != null && SceneManager.GetActiveScene().buildIndex == SaveAndLoad.LoadData().currentScene){
            loadSavedData = true;
        }else{
            loadSavedData = false;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
    
        if(RatoHealthBar != null){
            RatoHealthBar.gameObject.SetActive(rato != null);
        }
        
        if(GameObject.Find("GameOverTextUI")){
            GameOverTextUI = GameObject.Find("GameOverTextUI").GetComponent<TMP_Text>();
        }

        GameOverPanel = GameObject.Find("GameOverPanel");
        if (GameOverPanel != null){
            GameOverPanel.GetComponent<Image>().enabled = false;
        }

        if(SaveAndLoad.LoadData() != null){
            if(SceneManager.GetActiveScene().buildIndex == SaveAndLoad.LoadData().currentScene){
                if(loadSavedData){
                    Debug.Log("a");
                    rato.transform.position = new Vector3(SaveAndLoad.LoadData().currentPositionX, SaveAndLoad.LoadData().currentPositionY, 0);
                    rato.GetComponent<Animator>().SetBool("startAwake", false);
                    rato.GetComponent<Animator>().SetBool("awake", false);
                    ratoHumanity = SaveAndLoad.LoadData().ratoHumanity;
                    ratoHealth = SaveAndLoad.LoadData().ratoHealth;
                }else if(scene.name == "casateste"){
                    Debug.Log("b");
                    rato.GetComponent<Animator>().SetBool("startAwake", false);
                    rato.GetComponent<Animator>().SetBool("awake", false);
                }else{
                    Debug.Log("c");
                    rato.GetComponent<Animator>().SetBool("startAwake", true);
                }
            }else{
                if(rato != null){
                    rato.GetComponent<Animator>().SetBool("startAwake", true);
                }
            }
        }else if(scene.name == "casateste"){
            rato.GetComponent<Animator>().SetBool("startAwake", false);
            rato.GetComponent<Animator>().SetBool("awake", false);        
        }else{
            if(rato != null){
                rato.GetComponent<Animator>().SetBool("startAwake", true);
            }
        }

        if(ratoPosition != new Vector3(0, 0, 0)){
            rato.transform.position = ratoPosition; 
        }

        if(rato != null && ratoHealth != 0){
            rato.health = ratoHealth;
        }
    }

    void GetValues(){
        right = SavePrefs.GetString("right");
        left = SavePrefs.GetString("left");
        jump = SavePrefs.GetString("jump");
        down = SavePrefs.GetString("down");
        run = SavePrefs.GetString("run");
        interact = SavePrefs.GetString("interact");

        if(SavePrefs.HasKey("audioVolume")){
            audioVolume = SavePrefs.GetFloat("audioVolume");
        }else{
            audioVolume = 1;
        }

        if(SavePrefs.HasKey("musicVolume")){
            musicVolume = SavePrefs.GetFloat("musicVolume");
        }else{
            musicVolume = 1;
        }

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
        if(GameObject.FindGameObjectsWithTag("Gato").Length == 0 && GameObject.FindGameObjectsWithTag("Queijo").Length == 0 && GameObject.FindGameObjectWithTag("Porta") != null){
            GameObject.FindGameObjectWithTag("Porta").GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void Continue(){
        loadSavedData = true;
        SceneManager.LoadScene(SaveAndLoad.LoadData().currentScene);
    }

    public void Save(Vector3 position){
        rato = FindObjectOfType<Rato>();
        SaveAndLoad.SaveData(new Data(rato.transform.position.x, rato.transform.position.y, SceneManager.GetActiveScene().buildIndex, ratoHumanity, ratoHealth));
    }
}
