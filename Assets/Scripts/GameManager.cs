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

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        levelManager = LevelManager.instance;
        winUIManager = WinUIManager.instance;
        golfBall = GolfBall.instance;
        winUIManager.RegisterLevelManager(levelManager);
    }

    public void Win()
    {
        winUIManager.OnWin();
    }
}
