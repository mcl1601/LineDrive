﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBall : MonoBehaviour {
    Vector3 startPos;
    private float botY; // the bottom of the screen in world position

    // Use this for initialization
    void Start () {
        startPos = this.transform.position;

        botY = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).y;
    }
	
	// Update is called once per frame
	void Update () {
		if(this.transform.position.y <= botY || Input.GetKeyDown("r"))
        {
            //Debug.Log(botY);
            ResetBallPosition();            
        }
	}

    public void ResetBallPosition()
    {
        this.transform.position = startPos;
        Destroy(this.GetComponent<Rigidbody2D>());
    }
}
