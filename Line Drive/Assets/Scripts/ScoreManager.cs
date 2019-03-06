using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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

    bool kobe = false;
    float star2Pos;
    float star1Pos;
    bool spawn1star = false;
    bool spawn2star = false;
    bool spawn3star = false;
    void Start () {
        lineJuice = GameObject.Find("Main Camera").GetComponent<DrawLine>().lineJuice;
        totalBoost = gameObject.GetComponent<PlaceBoost>().maxBoosts;
	}
	
	// Update is called once per frame
	void Update () {
        
       
    }
    public void showScore()
    {
        // get references
        extraBoosts = gameObject.GetComponent<PlaceBoost>().RemainingBoosts;
        lineJuice = GameObject.Find("Main Camera").GetComponent<DrawLine>().lineJuice;

        // calculate total score
        kobe = GameObject.Find("BottomHole").GetComponent<Hole>().Kobe;
        totalScore = (lineJuice * 100) + (extraBoosts * 500) + (kobe ? 2500 : 0);

        // how many stars is this score worth?
        if (totalScore >= star3val) stars = 3;
        else if (totalScore >= star2val) stars = 2;
        else if (totalScore >= star1val) stars = 1;
        else stars = 0;

        // save the star value
        int holeNum = SceneManager.GetActiveScene().buildIndex;
        string key = "Hole" + holeNum + "Stars";
        if (PlayerPrefs.GetInt(key) < stars)
        {
            PlayerPrefs.SetInt(key, stars);

            // Updated total Stars
            int tempTotal = PlayerPrefs.GetInt("TotalStars", 0);
            PlayerPrefs.SetInt("TotalStars", tempTotal + (stars - PlayerPrefs.GetInt(key)));
        }

        // add the stars to the score bar
        star2Pos = 1f - ((float)star2val / star3val);
        star1Pos = 1f - ((float)star1val / star3val);
        GameObject.Find("2Star").GetComponent<RectTransform>().anchoredPosition = new Vector2(-300f * star2Pos, 0f);
        GameObject.Find("1Star").GetComponent<RectTransform>().anchoredPosition = new Vector2(-300f * star1Pos, 0f);
        //GameObject.Find("StarProgressFG").GetComponent<Image>().fillAmount = totalScore / (float)star3val;

        // display stars and score
        //GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = "Score: 0";
        StartCoroutine(PresentScore());
       
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

    void CheckForStar(float percent, Transform parent)
    {
        if(percent >= 1f - star1Pos && !spawn1star)
        {
            Instantiate(starObj, parent);
            spawn1star = true;
        }
        if(percent >= 1f - star2Pos && !spawn2star)
        {
            Instantiate(starObj, parent);
            spawn2star = true;
        }
        if(percent == 1 && !spawn3star)
        {
            Instantiate(starObj, parent);
            spawn3star = true;
        }
    }

    IEnumerator PresentScore()
    {
        TextMeshProUGUI s = GameObject.Find("ScorePerComponent").GetComponent<TextMeshProUGUI>();
        Transform starParent = GameObject.Find("StarGroup").transform;
        Image slider = GameObject.Find("StarProgressFG").GetComponent<Image>();
        float timer = 0f;
        float percent = 0f;
        float fillPercent = (lineJuice * 100f) / star3val;
        int currentScore = lineJuice * 100;

        // wait a bit
        yield return new WaitForSeconds(0.5f);

        // add what they got score for
        // Line juice remaining
        s.text = "Line Meter Remaining +" + lineJuice * 100;
        while(percent < 1f)
        {
            percent = timer / 0.75f;
            slider.fillAmount = Mathf.Lerp(0f, fillPercent, percent);
            timer += Time.deltaTime;
            CheckForStar(slider.fillAmount, starParent);
            yield return null;
        }
        // wait a bit
        yield return new WaitForSeconds(0.5f);

        // boosts leftover
        s.text += "\nBoosts Leftover (" + extraBoosts + " / " + totalBoost + ") +" + extraBoosts * 500;
        // reset vars
        timer = 0f;
        percent = 0f;
        // get new current score
        float init = slider.fillAmount;
        currentScore += (extraBoosts * 500);
        fillPercent = currentScore / (float)star3val;
        while (percent < 1f)
        {
            percent = timer / 0.75f;
            slider.fillAmount = Mathf.Lerp(init, fillPercent, percent);
            timer += Time.deltaTime;
            CheckForStar(slider.fillAmount, starParent);
            yield return null;
        }

        if (kobe)
        {
            yield return new WaitForSeconds(0.5f);
            s.text += "\nKOBE! +" + 2500;
            // reset vars
            timer = 0f;
            percent = 0f;
            // get new current score
            init = slider.fillAmount;
            currentScore += 2500;
            fillPercent = currentScore / (float)star3val;
            while (percent < 1f)
            {
                percent = timer / 0.75f;
                slider.fillAmount = Mathf.Lerp(init, fillPercent, percent);
                timer += Time.deltaTime;
                CheckForStar(slider.fillAmount, starParent);
                yield return null;
            }
        }
    }
}
