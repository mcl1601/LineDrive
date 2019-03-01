using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour {

    // Use this for initialization
    public int star3val;
    public int star2val;
    public int star1val;

    private int lineJuice;
    private int extraBoosts;
    public int stars = 0;
    public GameObject starObj;
    int totalScore = 0;
    
    int totalBoost = 0;
	void Start () {
        lineJuice = GameObject.Find("Main Camera").GetComponent<DrawLine>().lineJuice;
        totalBoost = gameObject.GetComponent<PlaceBoost>().maxBoosts;
	}
	
	// Update is called once per frame
	void Update () {
        
       
    }
    public void showScore()
    {
        extraBoosts = gameObject.GetComponent<PlaceBoost>().RemainingBoosts;
        lineJuice = GameObject.Find("Main Camera").GetComponent<DrawLine>().lineJuice;
        if (lineJuice >= star3val) stars = 3;
        else if (lineJuice >= star2val) stars = 2;
        else if (lineJuice >= star1val) stars = 1;
        else stars = 0;
        totalScore = lineJuice * 100;
        GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = "Score: 0";
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
        StartCoroutine(ShowScoreText());
    }

    IEnumerator ShowScoreText()
    {
        TextMeshProUGUI s = GameObject.Find("ScorePerComponent").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI total = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        s.text = "Line Meter Remaining = +" + (lineJuice * 100);
        yield return new WaitForSeconds(0.5f);
        float percent = 0f;
        float timer = 0f;
        float displayVal = 0;
        while(percent < 1f)
        {
            percent = timer / 0.5f;
            displayVal = Mathf.Floor(Mathf.Lerp(0f, lineJuice * 100f, percent));
            total.text = "Total Score: " + displayVal;
            timer += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        timer = 0f;
        percent = 0f;
        s.text = "Boosts Leftover ("+extraBoosts+" / "+totalBoost+") = +" + (extraBoosts * 500);
        float initScore = displayVal;
        displayVal = 0;
        while (percent < 1f)
        {
            percent = timer / 0.5f;
            displayVal = Mathf.Floor(Mathf.Lerp(initScore, (extraBoosts * 500) + initScore, percent));
            total.text = "Total Score: " + displayVal;
            timer += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        s.text = "";
    }
}
