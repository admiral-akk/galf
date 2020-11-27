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
            case 2:
                return 2;
            case 3:
                return 3;
            case 4:
                return 3;
            case 5:
                return 3;
            case 6:
                return 2;
            case 7:
                return 2;
            case 8:
                return 3;
            case 9:
                return 3;
            case 10:
                return 2;
            case 11:
                return 3;
            case 12:
                return 2;
            case 13:
                return 2;
            case 14:
                return 2;
            case 15:
                return 3;
            case 16:
                return 3;
            case 17:
                return 2;
            case 18:
                return 3;
            case 19:
                return 3;
            case 20:
                return 3;
            case 21:
                return 2;
            case 22:
                return 2;
            case 23:
                return 6;
            case 24:
                return 2;
            case 25:
                return 4;
        }
        return 0;
    } 
}
