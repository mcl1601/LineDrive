﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    public GameObject mainMenu, lvlSelect, lvlGroup;

    //delegate void LoadLvl(int lvl);
    //LoadLvl myLoadLvl;
	// Use this for initialization
	void Start () {
		for(int i = 0; i < lvlGroup.transform.childCount; i++)
        {
            GameObject btn = lvlGroup.transform.GetChild(i).gameObject;
            //myLoadLvl = LoadLevel;
            //btn.GetComponent<Button>().onClick.AddListener(myLoadLvl(i+1));
        }
	}
	
	

    public void SwitchToLevelSelect()
    {
        mainMenu.SetActive(false);
        lvlSelect.SetActive(true);
    }

    public void SwitchToMainMenu()
    {
        mainMenu.SetActive(true);
        lvlSelect.SetActive(false);
    }

    public void LoadLevel(int level)
    {
        Debug.Log(level);
        IEnumerator co = LoadScene(level);
        StartCoroutine(co);
    }

    IEnumerator LoadScene(int level)
    {
        Debug.Log("Begin Loading Hole" + level);
        string lvl = "Hole" + level.ToString();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(lvl);

        while(!asyncLoad.isDone)
        {
            yield return null;
        }

        Debug.Log("Done Loading");
    }
}
