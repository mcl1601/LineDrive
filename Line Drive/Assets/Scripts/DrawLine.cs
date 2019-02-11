using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour {
    public GameObject linePre;
    public GameObject ball;
    
    bool drawing = false;
    bool wasDrawing = false;
    GameObject lineRef = null;
    Vector3 lastPoint = Vector3.zero;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        LineRenderer l = null;
        if (lineRef != null)
            l = lineRef.GetComponent<LineRenderer>();

        if (Input.GetMouseButton(0))
        {
            Vector3 mousPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));

            if(!drawing)
            {
                lineRef = Instantiate(linePre, Vector3.zero, Quaternion.identity);
                drawing = true;
                l = lineRef.GetComponent<LineRenderer>();
                l.SetPosition(0, mousPos);
                lastPoint = mousPos;
                return;
            }

            if ((lastPoint - mousPos).magnitude < 0.1) return;
            
            l.positionCount++;
            l.SetPosition(l.positionCount - 1, mousPos);
            lastPoint = mousPos;

            wasDrawing = true;
        }
        else if(wasDrawing)
        {
            drawing = false;
            wasDrawing = false;
            EdgeCollider2D e = lineRef.GetComponent<EdgeCollider2D>();
            Vector2[] points = new Vector2[l.positionCount];
            for(int i = 0; i < l.positionCount; i++)
            {
                points[i] = new Vector2(l.GetPosition(i).x, l.GetPosition(i).y);
            }
            e.points = points;
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            ball.AddComponent<Rigidbody2D>();
        }
	}
}
