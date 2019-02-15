using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolToggle : MonoBehaviour {

    public string toggle = "Line";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {
            toggle = "Line";
            UpdateToggle();
        }
        if (Input.GetKeyDown(KeyCode.B)) {
            toggle = "Boost";
            UpdateToggle();
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            toggle = "Shoot";
            UpdateToggle();
        }


    }

    void UpdateToggle()
    {
        GameObject.FindGameObjectWithTag("Text").GetComponent<TextMeshPro>().text = toggle;
    }
}
