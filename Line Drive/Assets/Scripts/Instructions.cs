using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour {
    public GameObject parent;

    int acriveScreen = 0;
	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetInt("hasViewedInstructions") == 1)
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
            parent.transform.GetChild(acriveScreen).gameObject.SetActive(false);
            acriveScreen++;
            if(acriveScreen == 6)
            {
                parent.SetActive(false);
                PlayerPrefs.SetInt("hasViewedInstructions", 1);
                Destroy(this);
            }
            else
                parent.transform.GetChild(acriveScreen).gameObject.SetActive(true);
        }
	}
}
