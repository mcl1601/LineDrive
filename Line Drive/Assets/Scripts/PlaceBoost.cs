using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlaceBoost : MonoBehaviour {
    public GameObject speedBoost;
    public GameObject canvas;
    public GameUIController uiController;

    EventSystem eSys;
    GraphicRaycaster caster;
    PointerEventData pData;
    ToggleState toggle;

    GameObject instance;
	// Use this for initialization
	void Start () {
        caster = canvas.GetComponent<GraphicRaycaster>();
        eSys = canvas.GetComponent<EventSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        toggle = gameObject.GetComponent<ToolToggle>().toggle;
        if(Input.GetMouseButtonDown(0) && toggle == ToggleState.Boost)
        {
            instance = null;
            if (CheckMouseInput()) return;
            uiController.SlideUIUp();
            Vector3 mousPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
            instance = Instantiate(speedBoost, mousPos, Quaternion.Euler(new Vector3(0f, 0f, -90f)));

            // Add it to the stack of user-placed items
            uiController.placedObjects.Push(instance);
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
