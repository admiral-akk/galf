using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalSwings : MonoBehaviour
{
    private TMPro.TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Start()
    {
        int strokes = 0;

        for (int i = 1; i < 26; i++)
        {
            string levelName = "Level" + i.ToString();
            if (PlayerPrefs.HasKey(levelName))
            {
                strokes += PlayerPrefs.GetInt(levelName);
            }

        }

        text.text = "Total Strokes: " + strokes.ToString();
    }
}
