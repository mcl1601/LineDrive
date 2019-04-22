using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBall : MonoBehaviour {
    Vector3 startPos;
    private float botY; // the bottom of the screen in world position
    private GameObject[] noGrav;

    public float ballSpeedLimit;

    // Use this for initialization
    void Start () {
        startPos = this.transform.position;

        botY = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).y;

        // Setting the ball's custom attributes
        gameObject.GetComponent<SpriteRenderer>().color = new Color(PlayerPrefs.GetFloat("R", 1), PlayerPrefs.GetFloat("G", 1), PlayerPrefs.GetFloat("B", 1));
        noGrav = GameObject.FindGameObjectsWithTag("NoGrav");
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
        CheckGrav();
    }

    private void CheckGrav()
    {

        foreach(GameObject ng in noGrav)
        {
            if (ng.GetComponent<BoxCollider2D>().IsTouching(this.gameObject.GetComponent<CircleCollider2D>()))
            {
                Debug.Log("INNNNNNNN");
            }
        }
        
    }

    public void ResetBallPosition()
    {
        if (!this.GetComponent<Rigidbody2D>()) return;
        this.transform.position = startPos;
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        Destroy(this.GetComponent<Rigidbody2D>());
        GameObject.Find("BottomHole").GetComponent<Hole>().Kobe = true;
    }
}
