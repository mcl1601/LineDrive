using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum ToggleState
{
    Line, Boost, Shoot
}
public class ToolToggle : MonoBehaviour {

    public ToggleState toggle = ToggleState.Line;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {
            toggle = ToggleState.Line;
            UpdateToggle();
        }
        if (Input.GetKeyDown(KeyCode.B)) {
            toggle = ToggleState.Boost;
            UpdateToggle();
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            toggle = ToggleState.Shoot;
            UpdateToggle();
        }


    }

    void UpdateToggle()
    {
        //GameObject.FindGameObjectWithTag("Text").GetComponent<TextMeshPro>().text = toggle;
    }
}
