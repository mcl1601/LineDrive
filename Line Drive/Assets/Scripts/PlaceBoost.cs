using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBoost : MonoBehaviour {
    public GameObject speedBoost;
    ToggleState toggle;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        toggle = this.gameObject.GetComponent<ToolToggle>().toggle;
        if(Input.GetMouseButtonDown(0) && toggle == ToggleState.Boost)
        {
            Debug.Log("Yeet");
            Vector3 mousPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
            Instantiate(speedBoost, mousPos, Quaternion.identity);
        }
    }
   
}
