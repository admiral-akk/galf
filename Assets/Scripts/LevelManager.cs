using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private int levelNumber;
    public static LevelManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    public void TriggerWinScreen()
    {
        winScreen.SetActive(true);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("Level" + (1 + levelNumber).ToString());
    }
}
