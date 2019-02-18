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
    public RectTransform arrow;
    public float panelTransitionTime;

    private Button selected = null;
    private Coroutine lastroutine;
    private bool uiHidden = false;
	// Use this for initialization
	void Start () {
        selected = line;
	}

    public void ToggleUIPanel()
    {
        uiHidden = !uiHidden;
        if(uiHidden)
        {
            SlideUIUp();
        }
        else
        {
            SlideUIDown();
        }
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
        SlideUIUp();
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
        if(lastroutine != null) StopCoroutine(lastroutine);
        uiHidden = true;
        lastroutine = StartCoroutine(SlideUp());
    }

    IEnumerator SlideUp()
    {
        float timer = 0f;
        float percent = 0f;
        Vector2 initPos = panel.anchoredPosition;
        Quaternion initrot = arrow.rotation;

        while (percent < 1f)
        {
            percent = timer / panelTransitionTime;
            panel.anchoredPosition = new Vector2(0f, Mathf.Lerp(initPos.y, 50f, percent));
            arrow.rotation = Quaternion.Slerp(initrot, Quaternion.Euler(0f, 0f, 0f), percent);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public void SlideUIDown()
    {
        StopCoroutine(lastroutine);
        uiHidden = false;
        lastroutine = StartCoroutine(SlideDown());
    }

    IEnumerator SlideDown()
    {
        float timer = 0f;
        float percent = 0f;
        Vector2 initPos = panel.anchoredPosition;
        Quaternion initrot = arrow.rotation;

        while (percent < 1f)
        {
            percent = timer / panelTransitionTime;
            panel.anchoredPosition = new Vector2(0f, Mathf.Lerp(initPos.y, -50f, percent));
            arrow.rotation = Quaternion.Slerp(initrot, Quaternion.Euler(0f, 0f, 180f), percent);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
