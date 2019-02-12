using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour {
    public float boostAmt;

    GameObject ball;
	// Use this for initialization
	void Start () {
        ball = GameObject.Find("Ball");
	}

    // Update is called once per frame
    /*void Update () {
		
	}*/


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Ball") return;
        ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.up.x * boostAmt, transform.up.y * boostAmt));
    }
}
