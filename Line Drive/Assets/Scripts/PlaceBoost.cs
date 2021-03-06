﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlaceBoost : MonoBehaviour {
    public GameObject speedBoost;
    public GameObject canvas;
    public GameUIController uiController;

    public int maxBoosts = 3; // amount of speed boosts allowed in this level
    private int numBoosts; // number of boosts currently in the scene
    private Text boostText;

    EventSystem eSys;
    GraphicRaycaster caster;
    PointerEventData pData;
    ToggleState toggle;

    private ScoreManager score;

    GameObject instance;

    // Accessor for num of boosts currently in
    public int RemainingBoosts
    {
        get { return maxBoosts - numBoosts; }
    }
    public int RemoveBoosts
    {
        set { numBoosts -= value; }
    }

	// Use this for initialization
	void Start () {
        caster = canvas.GetComponent<GraphicRaycaster>();
        eSys = canvas.GetComponent<EventSystem>();
        boostText = uiController.boost.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        boostText.text = "" + RemainingBoosts;
    }
	
	// Update is called once per frame
	void Update () {
        toggle = gameObject.GetComponent<ToolToggle>().toggle;

        if (toggle != ToggleState.Boost) return;

        if(Input.GetMouseButtonDown(0))
        {
            instance = null;
            if (CheckMouseInput()) return;
            Vector3 mousPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
            instance = Instantiate(speedBoost, mousPos, Quaternion.identity);

            // Add it to the stack of user-placed items
            uiController.placedObjects.Add(instance);

            // Increment the boosts
            numBoosts++;
            boostText.text = "" + RemainingBoosts;
        }
        if(Input.GetMouseButton(0))
        {
            if(instance != null)
            {
                instance.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
            }
        }
        if(Input.GetMouseButtonUp(0) && instance != null)
        {

            // If this was the last boost, exit this mode and disable the button
            if (numBoosts >= maxBoosts)
            {
                gameObject.GetComponent<ToolToggle>().toggle = ToggleState.None;
                uiController.boost.interactable = false;
                return;
            }
        }
    }

    public bool CheckMouseInput()
    {
        pData = new PointerEventData(eSys);
        pData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        caster.Raycast(pData, results);

        if (results.Count > 0)
        {
            return true;
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameObject[] boosts = GameObject.FindGameObjectsWithTag("speed");
        for(int i = 0; i < boosts.Length; i++)
        {
            if (boosts[i].GetComponent<BoxCollider2D>().OverlapPoint(mousePos))
                return true;
        }

        return false;
    }
}
