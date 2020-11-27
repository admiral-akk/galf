using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private string levelName;
    [SerializeField] private RawImage starImage;
    [SerializeField] private RawImage checkImage;
    private Button button;

    // Start is called before the first frame update
    void Awake()
    {
        button = GetComponent<Button>();
        button.GetComponentInChildren<Text>().text = levelName;
    }

    private void Start()
    {
        button.onClick.AddListener(() => LevelManager.instance.LoadLevel(button));
        string normalizedLevelName = levelName.Replace(" ", "");
        if (normalizedLevelName != "Level1")
        {
            try
            {
                int levelNumber = Int32.Parse(levelName.Replace(" ", "").Replace("Level", ""));
                button.interactable = PlayerPrefs.HasKey("Level" + (levelNumber - 1).ToString());
            }
            catch (FormatException f)
            {
            }

        }

        if (PlayerPrefs.HasKey(normalizedLevelName)) {
           if (PlayerPrefs.GetInt(normalizedLevelName) <= LevelPar.GetPar(normalizedLevelName))
            {
                starImage.gameObject.SetActive(true);
                checkImage.gameObject.SetActive(false);
            } else
            {
                starImage.gameObject.SetActive(false);
                checkImage.gameObject.SetActive(true);
            }
        } else
        {
            starImage.gameObject.SetActive(false);
            checkImage.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
