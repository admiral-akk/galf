using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class LevelManager : MonoBehaviour
{
    private int levelNumber;
    public static LevelManager instance;

    void Awake()
    {
        instance = this;
        try { 
            levelNumber = Int32.Parse(SceneManager.GetActiveScene().name.Replace("Level", ""));
        } catch (FormatException f)
        {

        }
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

    public bool HasNextLevel()
    {
        return Application.CanStreamedLevelBeLoaded("Level" + (1 + levelNumber).ToString());
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("Level" + (1 + levelNumber).ToString());
    }
}
