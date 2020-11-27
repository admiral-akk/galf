using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // This class manages the game state. It holds all of the other managers
    // and delegates between them.

    private LevelManager levelManager;
    private WinUIManager winUIManager;
    private GolfBall golfBall;
    private int strokes;
    
    public static GameManager instance;

    private void Awake()
    {
        strokes = 0;
        instance = this;
    }

    private void Start()
    {
        levelManager = LevelManager.instance;
        winUIManager = WinUIManager.instance;
        golfBall = GolfBall.instance;
        winUIManager.RegisterLevelManager(levelManager);
    }

    public void HitBall()
    {
        strokes++;
    }

    private void updateLevelScore(int score)
    {
        string levelName = levelManager.GetLevelName();
        if (!PlayerPrefs.HasKey(levelName) || PlayerPrefs.GetInt(levelName) > score) 
        {
            PlayerPrefs.SetInt(levelName, score);
            PlayerPrefs.Save();
        }
    }

    public void Win()
    {
        updateLevelScore(strokes);
        winUIManager.OnWin();
        strokes = 0;
    }
}
