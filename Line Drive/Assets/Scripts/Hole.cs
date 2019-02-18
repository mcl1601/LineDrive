using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hole : MonoBehaviour {
    GameObject ball;
    int currentLevel;
    int maxLevel = 1;
	// Use this for initialization
	void Start () {
        ball = GameObject.Find("Ball");
        //If we're at the first hole, initialize the current hole at 1
        if(SceneManager.GetActiveScene().name == "Hole1")
        {
            currentLevel = 1;
        }
        //If not, set the current level to what it's saved as in the playerPref
        else
        {
            currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        }
        //If the current level we're on exceeds the recorded max level, make this level the new max level
        if(currentLevel > maxLevel)
        {
            maxLevel = currentLevel;
            PlayerPrefs.SetInt("maxLevel", maxLevel);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Only check for ball collision
        if (collision.gameObject.name != "Ball") return;
        //Make it so we can't go past the max Level
        if(currentLevel == maxLevel)
        {
            maxLevel++;
            PlayerPrefs.SetInt("maxLevel", maxLevel);
        }
        //Increment the current level and use that to go to the next scene
        currentLevel += 1;
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        SceneManager.LoadScene("Hole" + currentLevel);
    }
}
