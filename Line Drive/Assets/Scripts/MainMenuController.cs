using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    public GameObject mainMenu, lvlSelect, customization, ballCustom, trailCustom, loadingScreen, lvlGroup, btnPre, starPre,credits;
    public Image loadingBar;
    public Transform parent;
    private CustomizationManager customizationManager;

    private int maxLevelUnlocked;

	// Use this for initialization
	void Start () {
        customizationManager = GameObject.Find("CustomizationManager").GetComponent<CustomizationManager>();
        CheckForData();
        DestroyMusic();
		/*for(int i = 0; i < lvlGroup.transform.childCount; i++)
        {
            GameObject btn = lvlGroup.transform.GetChild(i).gameObject;
            int l = i;
            Button b = btn.GetComponent<Button>();
            if (i < maxLevelUnlocked)
                b.interactable = true;
            else
                b.interactable = false;
            b.onClick.AddListener(() => { LoadLevel(l + 1); });
        }*/

        for(int i = 0; i < SceneManager.sceneCountInBuildSettings - 2; i++)
        {
            GameObject inst = Instantiate(btnPre, parent);
            inst.GetComponentInChildren<Text>().text = (i + 1).ToString();
            int l = i;
            Button b = inst.GetComponent<Button>();
            if (i < maxLevelUnlocked)
            {
                b.interactable = true;
                int stars = PlayerPrefs.GetInt("Hole" + (i + 1) + "Stars", 0);
                for(int j = 0; j < stars; j++)
                {
                    Instantiate(starPre, b.transform.GetChild(1));
                }
            }
            else
                b.interactable = false;
            b.onClick.AddListener(() => { LoadLevel(l + 1); });
        }
	}
	
	private void CheckForData()
    {
        if(!PlayerPrefs.HasKey("maxLevel"))
        {
            PlayerPrefs.SetInt("maxLevel", 1);
            maxLevelUnlocked = 1;
        }
        else
        {
            maxLevelUnlocked = PlayerPrefs.GetInt("maxLevel");

            if(maxLevelUnlocked > 0)
            {
                GameObject.Find("Continue").GetComponent<Button>().interactable = true;
                GameObject.Find("Continue").GetComponentInChildren<Text>().text += "\n\nHole " + maxLevelUnlocked.ToString();
            }
        }
    }

    public void SwitchToCredits()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }
    public void SwitchToMainMenuFromCredits()
    {
        mainMenu.SetActive(true);
        credits.SetActive(false);
    }
    public void SwitchToLevelSelect()
    {
        mainMenu.SetActive(false);
        lvlSelect.SetActive(true);
    }

    public void SwitchToBallCustomization()
    {
        mainMenu.SetActive(false);
        customization.SetActive(true);
    }

    public void SwitchToMainMenu()
    {
        mainMenu.SetActive(true);
        lvlSelect.SetActive(false);
    }
    public void SwitchToMainMenuFromCustom()
    {
        mainMenu.SetActive(true);
        customization.SetActive(false);
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("maxLevel", 1);
        PlayerPrefs.SetInt("hasViewedInstructions", 0);
        PlayerPrefs.SetInt("hasViewedBounceInstructions", 0);
        PlayerPrefs.SetInt("hasViewedNoDrawInstructions", 0);
        PlayerPrefs.SetInt("TotalStars", 0);
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings - 1; i++)
        {
            string key = "Hole" + i + "Stars";
            PlayerPrefs.SetInt(key, 0);
        }
        // Reset all customization
        for(int j = 0; j < ballCustom.transform.childCount; j++)
        {
            string key = ballCustom.transform.GetChild(j).name + "BuyState";
            PlayerPrefs.SetInt(key, 0);
        }
        for (int k = 0; k < trailCustom.transform.childCount; k++)
        {
            string key = trailCustom.transform.GetChild(k).name + "BuyState";
            PlayerPrefs.SetInt(key, 0);
        }
        // Reset ball customization to white
        PlayerPrefs.SetString("CurrentBall", "White");
        PlayerPrefs.SetInt("Trail", -1);
        PlayerPrefs.SetInt("Ball", 0);
        customizationManager.UpdateCurrentBall(0);
        customizationManager.ResetTrail();


        maxLevelUnlocked = 1;
        LoadLevel(0);
    }

    public void ContinueGame()
    {
        LoadLevel(PlayerPrefs.GetInt("maxLevel"));
    }

    private void DestroyMusic()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
        foreach(GameObject thing in objs)
        {
            Destroy(thing);
        }
    }
    public void LoadLevel(int level)
    {
        IEnumerator co = LoadScene(level);
        StartCoroutine(co);
        lvlSelect.SetActive(false);
        loadingScreen.SetActive(true);
    }

    IEnumerator LoadScene(int level)
    {
        Debug.Log("Begin Loading Hole" + level);
        string lvl = "Hole" + level.ToString();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(lvl);

        while(!asyncLoad.isDone)
        {
            loadingBar.fillAmount = asyncLoad.progress;
            yield return null;
        }
    }
}
