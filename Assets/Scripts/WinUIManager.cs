﻿using UnityEngine;
using UnityEngine.UI;

public class WinUIManager : MonoBehaviour
{
    [SerializeField] private Button levelSelect;
    [SerializeField] private Button retryLevel;
    [SerializeField] private Button nextLevel;
    [SerializeField] private Canvas winScreen;

    public static WinUIManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void RegisterLevelManager(LevelManager levelManager)
    {
        levelSelect.onClick.AddListener(levelManager.LoadLevelSelect);
        retryLevel.onClick.AddListener(levelManager.LoadCurrentLevel);
        if (levelManager.HasNextLevel())
        {
            nextLevel.onClick.AddListener(levelManager.LoadNextLevel);
        } else
        {
            nextLevel.gameObject.SetActive(false);
        }
    }

    public void OnWin()
    {
        winScreen.gameObject.SetActive(true);
    }
}
