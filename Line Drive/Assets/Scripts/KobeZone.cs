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
        Collider2D[] col = new Collider2D[go.Length];
        for(int i = 0; i < go.Length; i++)
        {
            col[i] = go[i].GetComponent<Collider2D>();
        }
        Debug.Log("Num Lines: " + col.Length);
        if (gameObject.GetComponent<BoxCollider2D>().OverlapCollider(new ContactFilter2D(), col) > 0)
            return false;
        else
            return true;
    }
}
