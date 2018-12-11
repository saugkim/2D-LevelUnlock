using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;


public class MainMenuControl : MonoBehaviour {

    [SerializeField]
    Button GoToMapSceneButton;
    [SerializeField]
    Button ResetGameButton;
    [SerializeField]
    GameObject LoadingPanel;

    int mapSceneIndex;

	void Start ()
    {
        LoadingPanel.SetActive(false);
        GoToMapSceneButton.onClick.AddListener(GoToMap);
        ResetGameButton.onClick.AddListener(ResetGame);
        mapSceneIndex = 1;
	}

    private void GoToMap()
    {
        LoadingPanel.SetActive(true);
        SceneManager.LoadScene(mapSceneIndex, LoadSceneMode.Single);
    }    

    void ResetGame()
    {

        TextAsset txtAsset = (TextAsset)Resources.Load("LevelDatabase", typeof(TextAsset));
        string tileFile = txtAsset.text;

        File.WriteAllText(Application.persistentDataPath + "/LevelDatabase.json", tileFile);

    }
}
