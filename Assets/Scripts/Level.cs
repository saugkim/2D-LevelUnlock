using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Level
{ 
    public int LevelIndex { get; set; }
    public string Quest { get; set; }
    public int IsCompleted { get; set; }
    public int IsLocked { get; set; }
    public int Stars { get; set; }

    public int SceneBuildIndex { get; set; }

    public Level()
    {

    }

    public Level(int index, string quest, int isCompleted, int isLocked, int stars, int sceneIndex)
    {
        LevelIndex = index;
        Quest = quest;
        IsCompleted = isCompleted;
        IsLocked = isLocked;
        Stars = stars;
        SceneBuildIndex = sceneIndex;
    }

}

public class LevelContainer
{
    public List<Level> levelList = new List<Level>();

    public LevelContainer()
    {

    }
}
