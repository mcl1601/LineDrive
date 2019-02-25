using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForKobe : MonoBehaviour {
    Hole h;
	// Use this for initialization
	void Start () {
        h = gameObject.transform.parent.GetChild(0).gameObject.GetComponent<Hole>();
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        h.Kobe = false;
    }
}
