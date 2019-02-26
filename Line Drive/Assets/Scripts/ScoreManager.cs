using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    // Use this for initialization
    public GameObject winUI;
    [Header("Score Thresholds")]
    public int star3val;
    public int star2val;
    public int star1val;
    int lineJuice;
	void Start () {
        lineJuice = GameObject.Find("Main Camera").GetComponent<DrawLine>().lineJuice;
	}
	
	// Update is called once per frame
	void Update () {
        if(winUI.active == true)
        {
            lineJuice = GameObject.Find("Main Camera").GetComponent<DrawLine>().lineJuice;
            if (lineJuice >= star3val) Debug.Log("You've got 3 stars!");
            else if (lineJuice >= star2val) Debug.Log("You've got 2 stars!");
            else if (lineJuice >= star1val) Debug.Log("You've got 1 star!");
            else Debug.Log("You've got 0 stars!");
        }
       
    }
}
