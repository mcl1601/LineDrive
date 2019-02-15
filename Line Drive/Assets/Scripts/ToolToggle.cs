using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolToggle : MonoBehaviour {

    public string toggle = "Line";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L)) toggle = "Line";
        if (Input.GetKeyDown(KeyCode.B)) toggle = "Boost";
        if (Input.GetKeyDown(KeyCode.S)) toggle = "Shoot";

    }
}
