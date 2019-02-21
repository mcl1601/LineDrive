using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum ToggleState
{
    Line, Boost, Shoot, None
}
public class ToolToggle : MonoBehaviour {

    public ToggleState toggle;
	// Use this for initialization
	void Start () {
        toggle = ToggleState.None;
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
