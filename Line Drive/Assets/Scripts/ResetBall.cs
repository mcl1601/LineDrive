using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBall : MonoBehaviour {
    Vector3 startPos;
    private float botY; // the bottom of the screen in world position
    private GameObject[] noGrav;

    public float ballSpeedLimit;

    public Dictionary<int, GameObject> trails;

    // Use this for initialization
    void Start () {
        startPos = this.transform.position;

        botY = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).y;

        // Setting the ball's custom attributes
        gameObject.GetComponent<SpriteRenderer>().color = new Color(PlayerPrefs.GetFloat("R", 1), PlayerPrefs.GetFloat("G", 1), PlayerPrefs.GetFloat("B", 1));
        noGrav = GameObject.FindGameObjectsWithTag("NoGrav");
    }

    public void CheckForTrailCustomization()
    {
        int id = PlayerPrefs.GetInt("Trail", -1);
        if(id > -1)
        {
            TrailRenderer tr = trails[id].GetComponent<TrailRenderer>();
            TrailRenderer thisTrail = gameObject.AddComponent<TrailRenderer>();
            thisTrail = tr;
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
        if(gameObject.GetComponent<TrailRenderer>())
        {
            gameObject.GetComponent<TrailRenderer>().enabled = false;
        }
        this.transform.position = startPos;
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        Destroy(this.GetComponent<Rigidbody2D>());
        GameObject.Find("BottomHole").GetComponent<Hole>().Kobe = true;
    }
}
