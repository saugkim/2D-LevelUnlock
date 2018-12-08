using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using LitJson;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    GameObject QuestPanel;
    [SerializeField]
    GameObject LoadingPanel;

    [SerializeField]
    Button GoToMainMenuButton;
    [SerializeField]
    Button StartGameButton;
    [SerializeField]
    Button CancelAndCloseButton;
    [SerializeField]
    Text questText;
    [SerializeField]
    int mainmenuBuildIndex;

    public List<Level> levels;

    public List<Button> levelButtons;

    int levelIndex;

    private void Awake()
    {
        Debug.Log("Is It First TIme? " + PlayerPrefs.GetInt("FIRSTTIMEOPENING"));
        DataMove();
    }

    private void Start()
    {
        CreateLevelList();
        QuestPanel.SetActive(false);
        LoadingPanel.SetActive(false);

        for (int i = 0; i < levelButtons.Count; i++)
        {
            levelButtons[i].GetComponent<LevelButton>().levelText.text = (i+1).ToString();

            if (levels[i].IsLocked == 1)
            {
                levelButtons[i].GetComponent<Button>().interactable = false;
                foreach (var item in levelButtons[i].GetComponent<LevelButton>().stars)
                {
                    item.SetActive(false);
                }
            }
            else if (levels[i].IsLocked == 0 && levels[i].Stars == 0)
            {
                levelButtons[i].GetComponent<Button>().interactable = true;
                foreach (var item in levelButtons[i].GetComponent<LevelButton>().stars)
                {
                    item.SetActive(false);
                }
            }
            else if (levels[i].IsLocked == 0 && levels[i].Stars > 0)
            {
                levelButtons[i].GetComponent<Button>().interactable = true;

                foreach (var item in levelButtons[i].GetComponent<LevelButton>().stars)
                {
                    item.SetActive(false);
                }
                for (int j = 0; j < levels[i].Stars; j++)
                {
                    levelButtons[i].GetComponent<LevelButton>().stars[j].SetActive(true);
                }
            }
            else
            {
                return;
            }
        }

        foreach (var item in levelButtons)
        {
            int temp = Int32.Parse(item.GetComponent<LevelButton>().levelText.text);
            item.onClick.AddListener(delegate { ShowQuest(temp); });
        }

        GoToMainMenuButton.onClick.AddListener(GoToMainMenu);
        StartGameButton.onClick.AddListener(StartGame);
        CancelAndCloseButton.onClick.AddListener(CancelAndClose);
    }

    private void DataMove()
    {

        TextAsset txtAsset = (TextAsset)Resources.Load("LevelDatabase", typeof(TextAsset));
        string tileFile = txtAsset.text;

        if (PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1)
        {
            Debug.Log("First Time Opening");

            PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);

            FileStream fs = new FileStream(Application.persistentDataPath + "/LevelDatabase.json", FileMode.Create);
            fs.Close();

            File.WriteAllText(Application.persistentDataPath + "/LevelDatabase.json", tileFile);

        }
        else
        {
            return;
        }
        
    }

    private void CreateLevelList()
    {
        LevelContainer levelContainer = new LevelContainer();

        string jsondata = File.ReadAllText(Application.persistentDataPath + "/LevelDatabase.json");
        levelContainer = JsonMapper.ToObject<LevelContainer>(jsondata);

        for (int i = 0; i < levelContainer.levelList.Count; i++)
        {
            levels.Add(levelContainer.levelList[i]);
        }
        Debug.Log(levels.Count);
    }

    private void CancelAndClose()
    {
        QuestPanel.SetActive(false);
    }

    private void StartGame()
    {
        LoadingPanel.SetActive(true);
        QuestPanel.SetActive(false);
        //Debug.Log("Show me loading scene build index" + levels[levelIndex - 1].SceneBuildIndex);
        SceneManager.LoadScene(levels[levelIndex - 1].SceneBuildIndex, LoadSceneMode.Single);
    }

    private void GoToMainMenu()
    {
        LoadingPanel.SetActive(true);
        //Debug.Log("Show me main menu build index " + mainmenuBuildIndex);
        SceneManager.LoadScene(mainmenuBuildIndex, LoadSceneMode.Single);
    }

    private void ShowQuest(int index)
    {
        levelIndex = index;
        QuestPanel.SetActive(true);
        questText.text = "Go To Level " + index ;
        PlayerPrefs.SetInt("Level Index", levelIndex);
    }
}