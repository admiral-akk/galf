using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompletedLevels : MonoBehaviour
{
    private TMPro.TextMeshProUGUI text;

    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
    }



    // Update is called once per frame
    void Start()
    {
        int beatLevels = 0;

        for (int i = 1; i < 26; i++)
        {
            string levelName = "Level" + i.ToString();
            if (PlayerPrefs.HasKey(levelName) && PlayerPrefs.GetInt(levelName) <= LevelPar.GetPar(levelName))
            {
                beatLevels += 1;
            }

        }

        text.text = "On-Par Levels: " + beatLevels.ToString();
    }
}
