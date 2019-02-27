using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KobeZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public bool CheckForKobe()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("line");
        BoxCollider2D b = gameObject.GetComponent<BoxCollider2D>();
        foreach(GameObject g in go)
        {
            EdgeCollider2D e = g.GetComponent<EdgeCollider2D>();
            foreach(Vector2 p in e.points)
            {
                if(b.OverlapPoint(p))
                {
                    return false;
                }
            }
        }
        return true;
    }
}
