using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    public GameObject mainMenu, lvlSelect, loadingScreen, lvlGroup, btnPre;
    public Image loadingBar;
    public Transform parent;

    private int maxLevelUnlocked;

	// Use this for initialization
	void Start () {
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

        for(int i = 0; i < SceneManager.sceneCountInBuildSettings - 1; i++)
        {
            GameObject inst = Instantiate(btnPre, parent);
            inst.GetComponentInChildren<Text>().text = (i + 1).ToString();
            int l = i;
            Button b = inst.GetComponent<Button>();
            if (i < maxLevelUnlocked)
                b.interactable = true;
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

            if(maxLevelUnlocked > 1)
            {
                GameObject.Find("Continue").GetComponent<Button>().interactable = true;
                GameObject.Find("Continue").GetComponentInChildren<Text>().text += "\n\nHole " + maxLevelUnlocked.ToString();
            }
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

    public void NewGame()
    {
        PlayerPrefs.SetInt("maxLevel", 1);
        PlayerPrefs.SetInt("hasViewedInstructions", 0);
        maxLevelUnlocked = 1;
        LoadLevel(1);
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
