using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelControl : MonoBehaviour
{


    public List<Level> levels;
    public List<bool> completed;
    public List<bool> unlocked;

    public GameObject LoadingPanel;
    public List<Button> LevelButton;


    private void Start()
    {
        levels = new List<Level>();
        
        for (int i = 0; i < LevelButton.Count; i++)
        {
            if(levels[i].IsLocked == 1)
            {
                LevelButton[i].enabled = false;
            }
            else
            {
                LevelButton[i].enabled = true;
            }
        }

        for (int i = 0; i < LevelButton.Count; i++)
        {
            int temp = i + 1;
            LevelButton[i].onClick.AddListener(delegate { EnterLevel(temp); });
        }
    }

    public void Unlock(int level)
    {
        if (levels[level - 1].IsCompleted ==1 )
        {
            levels[level].IsLocked = 0;
        }
        
        
    }

    public void Complete(int level, int numberOfStars)
    {
        levels[level - 1].Stars = numberOfStars;
    }
    public void EnterLevel(int level)
    {
        if (levels[level-1].IsLocked == 0)
        {
            int temp = level + 1;
            LoadingPanel.SetActive(true);
            SceneManager.LoadScene(temp, LoadSceneMode.Single);
        }
        else
        {
            return;
        }
    }

    //public void updateButtonsUI()
    //{
    //    for (i = 0; i < LevelButton.Count; i++)
    //    {
    //        if (unlocked[i] == false)
    //        {
    //            LevelButton[i].GetComponent<Button>.enabled = false;
    //        }
    //        else
    //        {
    //            LevelButton[i].GetComponent<Button>.enabled = true;
    //        }
    //    }
    //}
}
