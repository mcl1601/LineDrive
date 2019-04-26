using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DrawLine : MonoBehaviour {
    public GameObject linePre;
    public GameObject ball;
    public bool lineHasPhysics;
    public float minDrawDistance;
    public GameObject powerLinePref;
    public GameObject canvas;
    public GameUIController uiController;
    public int lineJuice = 100;
    public Image juiceBar;

    bool drawing = false;
    bool wasDrawing = false;
    bool wasShooting = false;
    GameObject lineRef = null;
    Vector3 lastPoint = Vector3.zero;
    ToggleState toggle = ToggleState.Line;
    ToolToggle tool;
    float juiceInitial;

    EventSystem eSys;
    GraphicRaycaster caster;
    PointerEventData pData;

    List<GameObject> lines = new List<GameObject>();

    Vector3 mDown;
    Vector3 mCurrent;
    Vector3 mUp;
    GameObject powerLine;
    CustomizationManager customizationManager;
    float dragForce = 15f;
    // Use this for initialization
    void Start () {
        tool = GameObject.Find("SceneManager").GetComponent<ToolToggle>();
        caster = canvas.GetComponent<GraphicRaycaster>();
        eSys = canvas.GetComponent<EventSystem>();
        juiceInitial = (float)lineJuice;
        linePre.GetComponent<LineRenderer>().textureMode = LineTextureMode.Tile;
        customizationManager = GameObject.Find("CustomizationManager").GetComponent<CustomizationManager>();

        SetAspect();
        if (GameObject.Find("PaperBG") != null)
            GameObject.Find("PaperBG").GetComponent<ResizeBackground>().Resize();

        if (GameObject.Find("BG") != null)
            GameObject.Find("BG").GetComponent<ResizeBackground>().Resize();
    }

    void SetAspect()
    {
        float prefferedAspect = 16f / 9f;
        float actualAspect = Camera.main.aspect;
        Debug.Log(actualAspect);
        float size = (prefferedAspect / actualAspect) * 5f;
        Camera.main.orthographicSize = size;
    }
	
	// Update is called once per frame
	void Update () {
        toggle = tool.toggle;

        // Delete the last user placed item if there is one
        // Don't delete if in shoot mode
        if (Input.GetKeyDown(KeyCode.Z) && uiController.placedObjects.Count > 0 && toggle != ToggleState.Shoot)
        {
            //Destroy(lines[lines.Count - 1]);
            //lines.RemoveAt(lines.Count - 1);

            uiController.UndoLastPlacement();
        }

        // quick reference to the current line we are drawing
        if (toggle == ToggleState.Line)
        {
            LineRenderer l = null;
            if (lineRef != null)
                l = lineRef.GetComponent<LineRenderer>();

            Vector3 mousPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
            // while holding down the mouse or finger
            //Check to see if there's still line juice
            GameObject[] noDraw = GameObject.FindGameObjectsWithTag("NoDraw");
            bool drawable = true;
            foreach(GameObject g in noDraw)
            {
                if (g.GetComponent<BoxCollider2D>().bounds.Contains(new Vector3(mousPos.x, mousPos.y, 1)))
                {
                    drawable = false;
                    break;
                }
            }
            if (Input.GetMouseButton(0) && lineJuice > 0 && drawable)
            {
                // get the point

                // if this is the first frame of drawing
                if (!drawing)
                {
                    if (CheckMouseInput()) return;
                    // make a new line and save the reference
                    lineRef = Instantiate(linePre, Vector3.zero, Quaternion.identity);
                    drawing = true;
                    l = lineRef.GetComponent<LineRenderer>();
                    // set the frst point of the line and add it to the list
                    l.SetPosition(0, mousPos);
                    lastPoint = mousPos;
                    lines.Add(lineRef);
                    return;
                }

                wasDrawing = true;

                // not first frame of drawing
                // have we moved enough to add another segment?
                if ((lastPoint - mousPos).magnitude < minDrawDistance) return;

                // add a new point at the current location
                l.positionCount++;
                l.SetPosition(l.positionCount - 1, mousPos);
                lastPoint = mousPos;
                //Decrement Line Juice
                lineJuice--;
                UpdateJuiceBar();
            }
            // first frame after not drawing anymore
            else if (wasDrawing)
            {
                drawing = false;
                wasDrawing = false;
                if (l.positionCount == 1)
                {
                    l.positionCount++;
                    l.SetPosition(1, mousPos + new Vector3(0.1f, 0f, 0f));
                }
                // set up the edge collider using the line points
                EdgeCollider2D e = lineRef.GetComponent<EdgeCollider2D>();
                Vector2[] points = new Vector2[l.positionCount];
                for (int i = 0; i < l.positionCount; i++)
                {
                    points[i] = new Vector2(l.GetPosition(i).x, l.GetPosition(i).y);
                }
                e.points = points;

                // add a rigidbody for gravity
                if (lineHasPhysics)
                    lineRef.AddComponent<Rigidbody2D>();

                // Add it to the stack of user-placed items
                uiController.placedObjects.Add(lineRef);
            }
            

            // make the ball roll
            if (Input.GetKeyDown(KeyCode.Return))
            {
                ball.AddComponent<Rigidbody2D>();
                ball.GetComponent<Rigidbody2D>().mass = 0.2f;
            }
        }else if(toggle == ToggleState.Shoot)
        {
            if (ball.GetComponent<Rigidbody2D>()) return;
            if (Input.GetMouseButtonDown(0))
            {
                if (CheckMouseInput()) return;
                mDown = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
                powerLine = GameObject.Instantiate(powerLinePref, ball.transform.position, Quaternion.identity);
                wasShooting = true;
            }
            else if (Input.GetMouseButton(0))
            {
                if (CheckMouseInput()) return;
                mCurrent = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
                powerLine.transform.localScale = new Vector3((mDown.x - mCurrent.x)/2, .25f, 0);
            }
            else if (Input.GetMouseButtonUp(0) || wasShooting)
            {
                if (CheckMouseInput()) return;
                Destroy(powerLine);
                GetComponent<AudioSource>().Play();
                mUp = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
                ball.AddComponent<Rigidbody2D>();
                ball.GetComponent<Rigidbody2D>().mass = 0.2f;
                ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(((mDown.x - mUp.x) * dragForce),0));
                if(customizationManager.HasTrail())
                {
                    ball.GetComponent<TrailRenderer>().enabled = true;
                }
                mDown = Vector3.zero;
                mCurrent = Vector3.zero;
                mUp = Vector3.zero;
                wasShooting = false;
            }
        }
	}

    public bool CheckMouseInput()
    {
        pData = new PointerEventData(eSys);
        pData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        caster.Raycast(pData, results);

        if(results.Count > 0)
        {
            return true;
        }

        return false;
    }

    public void UpdateJuiceBar()
    {
        float percent = (float)lineJuice / juiceInitial;
        juiceBar.fillAmount = percent;
        if(percent > 0.5f)
            juiceBar.color = Color.Lerp(Color.yellow, Color.green, (percent - 0.5f) / 0.5f);
        else
            juiceBar.color = Color.Lerp(Color.red, Color.yellow, (percent) / 0.5f);
    }
}
