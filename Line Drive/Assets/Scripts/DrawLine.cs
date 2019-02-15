using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour {
    public GameObject linePre;
    public GameObject ball;
    public bool lineHasPhysics;
    public float minDrawDistance;
    
    bool drawing = false;
    bool wasDrawing = false;
    GameObject lineRef = null;
    Vector3 lastPoint = Vector3.zero;
    string toggle = "";
    ToolToggle tool;

    List<GameObject> lines = new List<GameObject>();

    Vector3 mDown;
    Vector3 mUp;
    float dragForce = 5;
    // Use this for initialization
    void Start () {
        tool = GameObject.Find("SceneManager").GetComponent<ToolToggle>();
    }
	
	// Update is called once per frame
	void Update () {
        toggle = tool.toggle;
        // quick reference to the current line we are drawing
        if (toggle == "Line")
        {
            LineRenderer l = null;
            if (lineRef != null)
                l = lineRef.GetComponent<LineRenderer>();

            Vector3 mousPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
            // while holding down the mouse or finger
            if (Input.GetMouseButton(0))
            {
                // get the point

                // if this is the first frame of drawing
                if (!drawing)
                {
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
            }

            // make the ball roll
            if (Input.GetKeyDown(KeyCode.Return))
            {
                ball.AddComponent<Rigidbody2D>();
                ball.GetComponent<Rigidbody2D>().mass = 0.2f;
            }

            // delete the last made line
            if (Input.GetKeyDown(KeyCode.Z) && lines.Count > 0)
            {
                Destroy(lines[lines.Count - 1]);
                lines.RemoveAt(lines.Count - 1);
            }
        }else if(toggle == "Shoot")
        {
            if (Input.GetMouseButtonDown(1))
            {
                mDown = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
            }
            if (Input.GetMouseButtonUp(1))
            {
                mUp = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
                ball.AddComponent<Rigidbody2D>();
                ball.GetComponent<Rigidbody2D>().mass = 0.2f;
                ball.GetComponent<Rigidbody2D>().AddForce(new Vector2((mDown.x - mUp.x * dragForce),0));
            }
        }
        
	}
}
