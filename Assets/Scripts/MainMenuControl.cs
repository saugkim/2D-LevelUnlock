using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuControl : MonoBehaviour {

    [SerializeField]
    Button GoToMapScene;
    [SerializeField]
    GameObject LoadingPanel;

    int mapSceneIndex;

	void Start ()
    {
        LoadingPanel.SetActive(false);
        GoToMapScene.onClick.AddListener(GoToMap);
        mapSceneIndex = 1;
	}

    private void GoToMap()
    {
        LoadingPanel.SetActive(true);
        SceneManager.LoadScene(mapSceneIndex, LoadSceneMode.Single);
    }    
}
