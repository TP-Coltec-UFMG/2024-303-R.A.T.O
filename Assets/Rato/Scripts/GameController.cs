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
    [HideInInspector] public string left, right, jump, down, run, interact, fontColor;
    [HideInInspector] public bool contrast, fullScreen;
    [HideInInspector] public int difficulty, fontSize;
    [HideInInspector] public Color color;

    void Awake()
    {
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update(){
        ShowHealth();
        ChangeFontColor(color);
        ChangeFontSize(FixedFontSize(fontSize));
    }

    public void ChangeScene(string SceneName){
        SceneManager.LoadScene(SceneName);
    }

    public void ChangeFontColor(Color color){
        TMP_Text[] changeThisColour = FindObjectsOfType<TMP_Text>();

        foreach (TMP_Text element in changeThisColour){
            if (element.tag == "ChangeableFont"){
                element.color = color;
            }
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
            yield return new WaitForSecondsRealtime(0.2f);
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

    public void SetNewSceneOnKeyPress(string sceneName){
        StartCoroutine(WaitForKeyPress(sceneName));
    }

    private IEnumerator WaitForKeyPress(string sceneName){
        while (!Input.GetKeyDown(KeyCode.E)){
            yield return null;
        }
        
        ChangeScene(sceneName);
    }
}
