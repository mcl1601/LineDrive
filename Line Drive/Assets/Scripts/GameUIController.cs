using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIController : MonoBehaviour {
    public ToolToggle toolToggle;
    public RectTransform panel;
    public Button line;
    public Button boost;
    public Button play;
    public RectTransform arrow;
    public float panelTransitionTime;
    public GameObject pauseScreen;

    // Stack used to track the objects last placed by the player
    public Stack<GameObject> placedObjects;

    private Button selected = null;
    private Coroutine lastroutine;
    private bool uiHidden = false;
    private float topY;
    private float bottomY;

    private Vector3 savedVelocity;
	// Use this for initialization
	void Start () {
        selected = line;
        topY = -panel.anchoredPosition.y;
        bottomY = -topY;

        // Init stack
        placedObjects = new Stack<GameObject>();
	}

    public void QuitToMain()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetBall()
    {
        GameObject.Find("Ball").GetComponent<ResetBall>().ResetBallPosition();
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

    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        Rigidbody2D r = GameObject.Find("Ball").GetComponent<Rigidbody2D>();
        if (r == null) return;
        savedVelocity = r.velocity;
        r.bodyType = RigidbodyType2D.Static;
    }

    public void ResumeGame()
    {
        pauseScreen.SetActive(false);
        Rigidbody2D r = GameObject.Find("Ball").GetComponent<Rigidbody2D>();
        if (r == null) return;
        r.bodyType = RigidbodyType2D.Dynamic;
        r.velocity = savedVelocity;
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

    /// <summary>
    /// Will reverse the last placement action the player took
    /// Includes drawing lines, placing boost pads, and placing bounce pads
    /// </summary>
    public void UndoLastPlacement()
    {
        GameObject last = placedObjects.Pop();
        Debug.Log(last);
        Debug.Log(last.tag);

        //check if its a line and if so, you need to return the amount of line juice corresponding to the removed line points
        switch (last.tag)
        {
            case "line":
                Camera.main.GetComponent<DrawLine>().lineJuice += last.GetComponent<EdgeCollider2D>().pointCount - 1;
                Camera.main.GetComponent<DrawLine>().UpdateJuiceBar();
                Debug.Log("triggered");
                break;
            case "speed":
                break;
            case "bounce":
                break;
        }

        Destroy(last);
    }

    /// <summary>
    /// If the player's pointer moves within the collider of an object it is removed
    /// Can remove anything that the player can place
    /// If moved over a line, the segment will be deleted (that's the goal anyway)
    /// </summary>
    public void RemoveLineSection()
    {

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
            panel.anchoredPosition = new Vector2(0f, Mathf.Lerp(initPos.y, topY, percent));
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
            panel.anchoredPosition = new Vector2(0f, Mathf.Lerp(initPos.y, bottomY, percent));
            arrow.rotation = Quaternion.Slerp(initrot, Quaternion.Euler(0f, 0f, 180f), percent);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
