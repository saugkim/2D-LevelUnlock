using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LitJson;
using System.IO;

public class GamePlay : MonoBehaviour
{

    [SerializeField]
    InputField inputField;
    [SerializeField]
    Button GoToMapSceneButton;
    [SerializeField]
    Button SaveDataButton;
    [SerializeField]
    Text LevelInfomationText;
    [SerializeField]
    GameObject LoadingPanel;

    int currentLevelIndex;
    int mapSceneIndex;
    int inputData;

    void Start ()
    {
        LoadingPanel.SetActive(false);
        currentLevelIndex = PlayerPrefs.GetInt("Level Index");
        GoToMapSceneButton.onClick.AddListener(GoToMapScene);
        SaveDataButton.onClick.AddListener(SaveData);
        LevelInfomationText.text = "YOU are in scene Level " + currentLevelIndex.ToString();
        mapSceneIndex = 1;
    }

    private void GoToMapScene()
    {
        LoadingPanel.SetActive(true);
        SceneManager.LoadScene(mapSceneIndex, LoadSceneMode.Single);
    }

    void SaveData()
    {
        if (int.TryParse(inputField.text, out inputData))
        {
            if (inputData == 1 || inputData == 2 || inputData == 3)
            {
                UpdateStars(inputData);
                UpdateUnlockState();
            }
        }
    }

    void UpdateStars(int stars)
    {
        LevelContainer levelContainer = new LevelContainer();

        string jsonData = File.ReadAllText(Application.persistentDataPath + "/LevelDatabase.json");
        levelContainer = JsonMapper.ToObject<LevelContainer>(jsonData);

        Level tempLevel = levelContainer.levelList.Find(x => x.LevelIndex == currentLevelIndex);

        tempLevel.Stars = stars; 

        string json = JsonMapper.ToJson(levelContainer);
        File.WriteAllText(Application.persistentDataPath + "/LevelDatabase.json", json);
    }

    void UpdateUnlockState()
    {
        LevelContainer levelContainer = new LevelContainer();
        string jsonData = File.ReadAllText(Application.persistentDataPath + "/LevelDatabase.json");
        levelContainer = JsonMapper.ToObject<LevelContainer>(jsonData);

        Level tempLevel = levelContainer.levelList.Find(x => x.LevelIndex == (currentLevelIndex + 1));

        tempLevel.IsLocked = 0; //Now next level IsLocked is false (set as 0) eli, its unlocked

        string json = JsonMapper.ToJson(levelContainer);
        File.WriteAllText(Application.persistentDataPath + "/LevelDatabase.json", json);
    }
}