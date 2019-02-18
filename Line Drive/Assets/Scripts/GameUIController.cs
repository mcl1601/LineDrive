using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour {
    public ToolToggle toolToggle;
    public RectTransform panel;
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

    public void SlideUIUp()
    {
        StartCoroutine(SlideUp());
    }

    IEnumerator SlideUp()
    {
        float timer = 0f;
        float percent = 0f;

        while(percent < 1f)
        {
            percent = timer / 0.5f;
            panel.anchoredPosition = new Vector2(0f, Mathf.Lerp(-50f, 50f, percent));
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public void SlideUIDown()
    {
        StartCoroutine(SlideDown());
    }

    IEnumerator SlideDown()
    {
        float timer = 0f;
        float percent = 0f;

        while (percent < 1f)
        {
            percent = timer / 0.5f;
            panel.anchoredPosition = new Vector2(0f, Mathf.Lerp(50f, -50f, percent));
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
