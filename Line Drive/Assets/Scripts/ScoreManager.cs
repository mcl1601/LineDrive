using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour {

    // Use this for initialization
    public int star3val;
    public int star2val;
    public int star1val;
    int lineJuice;
    public int stars = 0;
    public GameObject starObj;
    int totalScore = 0;
	void Start () {
        lineJuice = GameObject.Find("Main Camera").GetComponent<DrawLine>().lineJuice;
	}
	
	// Update is called once per frame
	void Update () {
        
       
    }
    public void showScore()
    {

        lineJuice = GameObject.Find("Main Camera").GetComponent<DrawLine>().lineJuice;
        if (lineJuice >= star3val) stars = 3;
        else if (lineJuice >= star2val) stars = 2;
        else if (lineJuice >= star1val) stars = 1;
        else stars = 0;
        totalScore = lineJuice * 100;
        GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = "Score: " + totalScore;
        StartCoroutine(SpawnStars());
       
    }
    IEnumerator SpawnStars()
    {
        // get the container to spawn the stars in
        Transform parent = GameObject.Find("StarGroup").transform;
        // wait a bit to spawn the first star
        yield return new WaitForSeconds(0.5f);
        while (stars != 0)
        {
            // spawn the star in the parent container
            Instantiate(starObj, parent);
            stars--;
            yield return new WaitForSeconds(0.4f);
        }
    }
}
