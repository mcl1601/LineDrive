using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TutorialHole : MonoBehaviour
{
    // Start is called before the first frame update
    //This script is just for the first hole.
    public GameObject winUI, loadingScreen;
    public Image loadingBar;
    public GameObject nextBtn;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Ball") return;
        GetComponent<AudioSource>().Play();
        winUI.SetActive(true);
        // disable the collider
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        GameObject.Find("WinText").GetComponent<TextMeshProUGUI>().text = "Hole in One!";
    }
    public void LoadNextLevel()
    {
        loadingScreen.SetActive(true);
        winUI.SetActive(false);
        IEnumerator l = LoadScene(1);
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
