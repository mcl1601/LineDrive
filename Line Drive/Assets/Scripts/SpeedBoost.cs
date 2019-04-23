using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour {
    public float boostAmt;

    private GameObject ball;

    private Eraser eraser;

	// Use this for initialization
	void Start () {
        ball = GameObject.Find("Ball");
        eraser = GameObject.Find("Eraser(Clone)").GetComponent<Eraser>();
	}

    // Update is called once per frame
    /*void Update () {
		
	}*/


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Get the name of the collision's associated gameobject
        string name = collision.gameObject.name;

        if (name != "Ball" && name != "Eraser(Clone)") return;

        // Speed up the ball
        if(name == "Ball")
        {
            GetComponent<AudioSource>().Play();
            ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.right.x * boostAmt, transform.right.y * boostAmt));
        }
        else // Erase the SpeedBoost
        {
            eraser.RemoveItem(gameObject);
        }
    }
}
