using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    private int levelNumber;
    public static LevelManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        levelNumber = Int32.Parse(SceneManager.GetActiveScene().name.Replace("Level", ""));
    }

    public void TriggerWinScreen()
    {
        winScreen.SetActive(true);
    }

    public void LoadLevel(Button b)
    {
        SceneManager.LoadScene(b.GetComponentsInChildren<Text>()[0].text.Replace(" ", ""));
    }

    public void LoadLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene("Level" + levelNumber.ToString());
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("Level" + (1 + levelNumber).ToString());
    }
}
