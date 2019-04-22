using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Eraser requires the GameObject to have a CircleCollider2D component
[RequireComponent(typeof(CircleCollider2D))]
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
        eraserCol = gameObject.GetComponent<CircleCollider2D>();
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
        

        // HERE we need to re-write the code to track mouse position and set the gameobjects position to be the same

        if (Input.GetMouseButtonUp(0))
        {
            // slide the ui back down
            uiController.SlideUIDown();

            // If there is nothing to delete, switch tool to none
            if (uiController.placedObjects.Count < 0)
            {
                gameObject.GetComponent<ToolToggle>().toggle = ToggleState.None;
                //uiController.eraser.interactable = false;
                return;
            }
        }
    }

    /// <summary>
    /// If the player's pointer moves within the collider of an object it is removed
    /// Can remove anything that the player can place
    /// If moved over a line, the segment will be deleted (that's the goal anyway)
    /// </summary>
    public void RemoveItem(GameObject itemToErase)
    {
        // Slide up the UI so you can erase under it
        uiController.SlideUIUp();

        // Initially just delete the entire object, can look at line segments and splitting lines later

        //check if its a line and if so, you need to return the amount of line juice corresponding to the removed line points
        switch (itemToErase.tag)
        {
            case "line":
                Camera.main.GetComponent<DrawLine>().lineJuice += itemToErase.GetComponent<EdgeCollider2D>().pointCount - 1;
                Camera.main.GetComponent<DrawLine>().UpdateJuiceBar();
                break;
            case "speed": // if its a speed boost then return one usable boost to the player
                uiController.GetComponent<PlaceBoost>().RemoveBoosts = 1;
                uiController.boost.interactable = true;
                uiController.boost.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "" + uiController.GetComponent<PlaceBoost>().RemainingBoosts;
                break;
        }

        // Remove the erased object from the user's placed objects list
        uiController.placedObjects.Remove(itemToErase);

        // Destroy the game object completely
        Destroy(itemToErase);
    }
}
