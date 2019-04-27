using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResetBall : MonoBehaviour {
    Vector3 startPos;
    private float botY; // the bottom of the screen in world position
    private GameObject[] noGrav;

    public float ballSpeedLimit;

    public Dictionary<int, GameObject> trails;
    
    private CustomizationManager customizationManager;

    // Use this for initialization
    void Start () {
        customizationManager = GameObject.Find("CustomizationManager").GetComponent<CustomizationManager>();
        startPos = this.transform.position;

        botY = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).y;

        // Setting the ball's custom attributes
        gameObject.AddComponent<SpriteRenderer>(customizationManager.GetCurrentBall());
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 10;
        noGrav = GameObject.FindGameObjectsWithTag("NoGrav");
        
        if(customizationManager.HasTrail())
        {
            gameObject.AddComponent<TrailRenderer>(customizationManager.GetCurrentTrail());
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(this.transform.position.y <= botY || Input.GetKeyDown("r"))
        {
            //Debug.Log(botY);
            ResetBallPosition();            
        }
        Rigidbody2D ballRB = gameObject.GetComponent<Rigidbody2D>();
        if (ballRB != null)
        {
            if(ballRB.velocity.magnitude > ballSpeedLimit)
                ballRB.velocity = ballRB.velocity.normalized * ballSpeedLimit;
        }
    }

    public void ResetBallPosition()
    {
        if (!this.GetComponent<Rigidbody2D>()) return;
        if(customizationManager.HasTrail())
        {
            gameObject.GetComponent<TrailRenderer>().time = 0f;
        }
        this.transform.position = startPos;
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        Destroy(this.GetComponent<Rigidbody2D>());
        GameObject.Find("BottomHole").GetComponent<Hole>().Kobe = true;
    }
}
