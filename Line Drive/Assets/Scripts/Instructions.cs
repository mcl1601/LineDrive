using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour {
    public GameObject parent;
    int currentScene;
    int activeScreen = 0;
	// Use this for initialization
	void Start () {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        if ((PlayerPrefs.GetInt("hasViewedInstructions") == 1 && currentScene == 1) ||
            (PlayerPrefs.GetInt("hasViewedBounceInstructions") == 1 && currentScene == 4) ||
            (PlayerPrefs.GetInt("hasViewedNoDrawInstructions") == 1 && currentScene == 5))
            Destroy(this);
        else
        {
            parent.SetActive(true);
            parent.transform.GetChild(0).gameObject.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            parent.transform.GetChild(activeScreen).gameObject.SetActive(false);
            activeScreen++;
            if(activeScreen == 1 && currentScene == 4)
            {
                parent.SetActive(false);
                PlayerPrefs.SetInt("hasViewedBounceInstructions", 1);
                Destroy(this);
            }
            if (activeScreen == 1 && currentScene == 5)
            {
                parent.SetActive(false);
                PlayerPrefs.SetInt("hasViewedNoDrawInstructions", 1);
                Destroy(this);
            }
            if (activeScreen == 6)
            {
                parent.SetActive(false);
                PlayerPrefs.SetInt("hasViewedInstructions", 1);
                Destroy(this);
            }
            else
                parent.transform.GetChild(activeScreen).gameObject.SetActive(true);
        }
	}
}
