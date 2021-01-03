using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private string levelName;
    [SerializeField] private Text buttonText;
    [SerializeField] private Text scoreText;
    [SerializeField] private RawImage starImage;
    [SerializeField] private RawImage checkImage;
    private Button button;

    // Start is called before the first frame update
    void Awake()
    {
        button = GetComponent<Button>();
        buttonText.text = levelName;
    }

    private void Start()
    {
        FileStream stream = File.Open(Application.persistentDataPath + "/test.json", FileMode.OpenOrCreate);

        StreamWriter write = new StreamWriter(stream);
        write.Write(JsonUtility.ToJson(state));
        write.Close();

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
           int par = LevelPar.GetPar(normalizedLevelName);
           int score = PlayerPrefs.GetInt(normalizedLevelName);
           if (score <= par)
            {
                starImage.gameObject.SetActive(true);
                checkImage.gameObject.SetActive(false);
            } else
            {
                starImage.gameObject.SetActive(false);
                checkImage.gameObject.SetActive(true);
            }
            scoreText.text = String.Format("{0}/{1}", score, par);
        } else
        {
            starImage.gameObject.SetActive(false);
            checkImage.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
