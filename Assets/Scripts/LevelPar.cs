using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelPar 
{
    public static int GetPar(string levelName)
    {
        int levelNumber;
        try
        {
            levelNumber = Int32.Parse(levelName.Replace(" ", "").Replace("Level", ""));
        }
        catch (FormatException f)
        {
            return 0;
        }
        switch (levelNumber)
        {
            case 1:
                return 2;
        }
        return 0;
    } 
}
