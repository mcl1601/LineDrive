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

    List<GameObject> lines = new List<GameObject>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // quick reference to the current line we are drawing
        LineRenderer l = null;
        if (lineRef != null)
            l = lineRef.GetComponent<LineRenderer>();

        // while holding down the mouse or finger
        if (Input.GetMouseButton(0))
        {
            // get the point
            Vector3 mousPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));

            // if this is the first frame of drawing
            if(!drawing)
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

            // not first frame of drawing
            // have we moved enough to add another segment?
            if ((lastPoint - mousPos).magnitude < minDrawDistance) return;
            
            // add a new point at the current location
            l.positionCount++;
            l.SetPosition(l.positionCount - 1, mousPos);
            lastPoint = mousPos;

            wasDrawing = true;
        }
        // first frame after not drawing anymore
        else if(wasDrawing)
        {
            drawing = false;
            wasDrawing = false;
            // set up the edge collider using the line points
            EdgeCollider2D e = lineRef.GetComponent<EdgeCollider2D>();
            Vector2[] points = new Vector2[l.positionCount];
            for(int i = 0; i < l.positionCount; i++)
            {
                points[i] = new Vector2(l.GetPosition(i).x, l.GetPosition(i).y);
            }
            e.points = points;

            // add a rigidbody for gravity
            if (lineHasPhysics)
                lineRef.AddComponent<Rigidbody2D>();
        }

        // make the ball roll
        if(Input.GetKeyDown(KeyCode.Return))
        {
            ball.AddComponent<Rigidbody2D>();
            ball.GetComponent<Rigidbody2D>().mass = 0.2f;
        }

        // delete the last made line
        if(Input.GetKeyDown(KeyCode.Z) && lines.Count > 0)
        {
            Destroy(lines[lines.Count - 1]);
            lines.RemoveAt(lines.Count - 1);
        }
	}
}
