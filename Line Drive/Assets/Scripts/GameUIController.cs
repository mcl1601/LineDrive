using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour {
    public ToolToggle toolToggle;
    public Button line;
    public Button boost;
    public Button play;

    private Button selected = null;
	// Use this for initialization
	void Start () {
        selected = line;
	}
	
	public void EnableDrawMode()
    {
        toolToggle.toggle = ToggleState.Line;
        ResetSelectedButton();
        selected = line;
        HighlightSelected();
    }

    public void EnableBoostMode()
    {
        toolToggle.toggle = ToggleState.Boost;
        ResetSelectedButton();
        selected = boost;
        HighlightSelected();
    }

    public void EnableShootMode()
    {
        toolToggle.toggle = ToggleState.Shoot;
        ResetSelectedButton();
        selected = play;
        HighlightSelected();
    }

    public void ResetSelectedButton()
    {
        ColorBlock c = selected.colors;
        c.normalColor = Color.white;
        selected.colors = c;
    }


    public void HighlightSelected()
    {
        ColorBlock c = selected.colors;
        c.normalColor = Color.red;
        c.highlightedColor = Color.red;
        selected.colors = c;
    }
}
