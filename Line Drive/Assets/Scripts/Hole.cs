using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Hole : MonoBehaviour {
    public GameObject winUI, loadingScreen;
    public Image loadingBar;
    public GameObject nextBtn;

    GameObject ball;
    int currentLevel;
    int maxLevel = 1;

    bool kobe = true;

    private ScoreManager scoreScript;

    public bool Kobe
    {
        get { return kobe; }
        set { kobe = value; }
    }
	// Use this for initialization
	void Start () {
        ball = GameObject.Find("Ball");
        //Set the hole to the number at the end of the scene name
        currentLevel = Int32.Parse(SceneManager.GetActiveScene().name.Split('e')[1]);
        Debug.Log("Current Level: " + currentLevel);
        //Make sure max level is accurate
        maxLevel = PlayerPrefs.GetInt("maxLevel");
        Debug.Log("Max Level: " + maxLevel);
        scoreScript = GameObject.Find("SceneManager").GetComponent<ScoreManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Only check for ball collision
        if (collision.gameObject.name != "Ball") return;
        if(SceneManager.GetActiveScene().buildIndex + 1 == SceneManager.sceneCountInBuildSettings)
        {
            nextBtn.SetActive(false);

        }
        else
        {
            //Increment Max Level
            if (currentLevel >= maxLevel)
            {
                maxLevel++;
                Debug.Log("Incrementing max level to: " + maxLevel);
                PlayerPrefs.SetInt("maxLevel", maxLevel);
            }
            //Increment the current level and use that to go to the next scene
            currentLevel += 1;
            PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        }
      

        winUI.SetActive(true);
        scoreScript.showScore();
        // disable the collider
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        // if the ball didn't touch the ledges
        if (kobe)
        {
            // check if there are lines above the hole
            if (transform.parent.GetChild(3).gameObject.GetComponent<KobeZone>().CheckForKobe())
            {
                // good kobe, change the text
                GameObject.Find("WinText").GetComponent<TextMeshProUGUI>().text = "KOBE!";
            }
            else
            {
                // no kobe, reset text
                Debug.Log("Line in Kobe zone, no kobe");
                GameObject.Find("WinText").GetComponent<TextMeshProUGUI>().text = "Hole in One!";
            }
        }
        else
        {
            // no kobe, reset text
            Debug.Log("Hit Ledges, no Kobe");
            GameObject.Find("WinText").GetComponent<TextMeshProUGUI>().text = "Hole in One!";
        }

    }

    public void LoadNextLevel()
    {
        loadingScreen.SetActive(true);
        winUI.SetActive(false);
        IEnumerator l = LoadScene(currentLevel);
        StartCoroutine(l);
    }

    IEnumerator LoadScene(int level)
    {
        string lvl = "Hole" + level.ToString();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(lvl);

        while (!asyncLoad.isDone)
        {
            loadingBar.fillAmount = asyncLoad.progress;
            yield return null;
        }
    }
}
