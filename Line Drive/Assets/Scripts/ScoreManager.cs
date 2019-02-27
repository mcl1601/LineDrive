using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    // Use this for initialization
    public int star3val;
    public int star2val;
    public int star1val;
    int lineJuice;
    public int stars = 0;
    public Sprite starObj;
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

        
       
    }
    IEnumerator SpawnStars()
    {
        while (stars != 0)
        {
            Instantiate(starObj);
            stars--;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
