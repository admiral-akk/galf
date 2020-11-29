using UnityEngine;using UnityEngine.UI;public class WinUIManager : MonoBehaviour{    [SerializeField] private Button levelSelect;    [SerializeField] private Button retryLevel;    [SerializeField] private Button nextLevel;    [SerializeField] private Canvas winScreen;    [SerializeField] private Button pauseLevelSelect;    [SerializeField] private Button retryLevelSelect;    [SerializeField] private Canvas pauseScreen;
    [SerializeField] private TMPro.TextMeshProUGUI strokes;
    [SerializeField] private TMPro.TextMeshProUGUI par;
    [SerializeField] private TMPro.TextMeshProUGUI escapeText;
    [SerializeField] private Text winText;    public static WinUIManager instance;    private void Awake()    {        instance = this;    }    public void RegisterLevelManager(LevelManager levelManager)    {        levelSelect.onClick.AddListener(levelManager.LoadLevelSelect);        retryLevel.onClick.AddListener(levelManager.LoadCurrentLevel);        pauseLevelSelect.onClick.AddListener(PauseLevelSelect);        retryLevelSelect.onClick.AddListener(PausedRetry);        if (levelManager.HasNextLevel())        {            nextLevel.onClick.AddListener(levelManager.LoadNextLevel);        } else        {            nextLevel.gameObject.SetActive(false);        }
        par.text = "Par: " + LevelPar.GetPar(levelManager.GetLevelName());    }    public void SetStrokes(int strokeCount)
    {
        strokes.text = "Strokes: " + strokeCount.ToString();
    }    public void OnWin()    {        winScreen.gameObject.SetActive(true);    }    private void PausedRetry()
    {
        Time.timeScale = 1;
        LevelManager.instance.LoadCurrentLevel();
    }    private void PauseLevelSelect()
    {
        Time.timeScale = 1;
        LevelManager.instance.LoadLevelSelect();
    }    public void Unpause()
    {
        pauseScreen.gameObject.SetActive(false);
        escapeText.text = "Press 'Esc' to Pause";
    }    public void Pause()
    {
        pauseScreen.gameObject.SetActive(true);
        escapeText.text = "Press 'Esc' to Unpause";
    }}