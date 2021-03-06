﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    private Eraser eraser;

    // Start is called before the first frame update
    void Start()
    {
        eraser = GameObject.Find("Eraser(Clone)").GetComponent<Eraser>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Get the name of the collision's associated gameobject
        string name = collision.gameObject.name;

        if (name != "Eraser(Clone)") return;

        // Erase the SpeedBoost
        eraser.RemoveItem(gameObject);
    }
}
