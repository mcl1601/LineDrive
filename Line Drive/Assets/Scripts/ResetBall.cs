using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBall : MonoBehaviour {
    Vector3 startPos;
    // Use this for initialization
    void Start () {
        startPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(this.transform.position.y <= -6 || Input.GetKeyDown("r"))
        {
            ResetBallPosition();            
        }
	}

    public void ResetBallPosition()
    {
        this.transform.position = startPos;
        Destroy(this.GetComponent<Rigidbody2D>());
    }
}
