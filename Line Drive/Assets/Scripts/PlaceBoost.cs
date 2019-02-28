using System.Collections;
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
    }
	
	// Update is called once per frame
	void Update () {
        toggle = gameObject.GetComponent<ToolToggle>().toggle;

        if (toggle != ToggleState.Boost) return;

        if(Input.GetMouseButtonDown(0))
        {
            instance = null;
            if (CheckMouseInput()) return;
            uiController.SlideUIUp();
            Vector3 mousPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
            instance = Instantiate(speedBoost, mousPos, Quaternion.identity);

            // Add it to the stack of user-placed items
            uiController.placedObjects.Push(instance);

            // Increment the boosts
            numBoosts++;
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
            uiController.SlideUIDown();

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

        return false;
    }
}
