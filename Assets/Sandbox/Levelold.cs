using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Levelold
{

    public int levelId { get; set; }
    public string levelName { get; set; }
    public bool isCompleted  { get; set; }
    public bool isLocked { get; set; }
    public int stars { get; set; }

    public int SceneLevel { get; set; }

    public Levelold(int id, string name, bool completed, bool locked, int stars, int sceneLevel)
    {
        levelId = id;
        levelName = name;
        isCompleted = completed;
        isLocked = locked;
        this.stars = stars;
        SceneLevel = sceneLevel;

    }

    public Levelold()
    {
    }

    public void Complete()
    {
        isCompleted = true;
    }
    
    public void Complete(int numberOfStar)
    {
        isCompleted = true;
        stars = numberOfStar;
    }

    public void Lock()
    {
        isLocked = true;
    }
    
    public void Unlock()
    {
        isLocked = false;
    }
}
