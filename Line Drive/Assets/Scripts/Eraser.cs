using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : MonoBehaviour
{
    public GameObject speedBoost;
    public GameObject canvas;
    public GameUIController uiController;

    public float radius;

    private CircleCollider2D eraserCol;

    ToggleState toggle;

    public CircleCollider2D EraserCol
    {
        get { return eraserCol; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        toggle = gameObject.GetComponent<ToolToggle>().toggle;

        // if it's not in the remove tool state then don't do anything
        if (toggle != ToggleState.Remove) return;

        ///HEY, KAT -- PLAN BELOW
        ///So you need to use the speed boost script (not the place script the one on the boost)
        ///And a new script on the Line prefab to check if they are colliding with the eraser
        ///The eraser is a circle 2d collider that can be checked using the IsTouching method they all have
        ///Then call the Remove Item method in this script which will take in the gameobject 
        ///That method will take care of removing it from the list of placed objects
        ///And restoring the corresponding boost number or line juice

        //if (Input.GetMouseButton(0))
        //{
        //    uiController.SlideUIUp();
        //    Vector3 mousPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));

        //    // Remove it from the stack of user-placed items
        //    RemoveItem();

            
        //}
        //if (Input.GetMouseButtonUp(0))
        //{
        //    uiController.SlideUIDown();

        //    // If there is nothing to delete, grey out this button
        //    if (uiController.placedObjects.Count < 0)
        //    {
        //        gameObject.GetComponent<ToolToggle>().toggle = ToggleState.None;
        //        uiController.eraser.interactable = false;
        //        return;
        //    }
        //}
    }

    /// <summary>
    /// If the player's pointer moves within the collider of an object it is removed
    /// Can remove anything that the player can place
    /// If moved over a line, the segment will be deleted (that's the goal anyway)
    /// </summary>
    public void RemoveItem()
    {
        // Initially just delete the entire object, can look at line segments and splitting lines later

        //uiController.placedObjects.Remove();
    }
}
