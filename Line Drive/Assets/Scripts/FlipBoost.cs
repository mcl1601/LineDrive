using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipBoost : MonoBehaviour
{
    public float resetTimer;
    private float timer;
    private int touchCount;
    private bool countdown;
    // Start is called before the first frame update
    void Start()
    {
        touchCount = 0;
        timer = 0f;
        countdown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown)
        {
            timer += Time.deltaTime;
            if(timer >= resetTimer)
            {
                touchCount = 0;
                countdown = false;
                timer = 0f;
            }
        }
    }

    private void OnMouseDown()
    {
        touchCount++;
        countdown = true;

        if(touchCount == 2)
        {
            transform.RotateAround(transform.position, new Vector3(0f, 0f, 1f), 180f);
            touchCount = 0;
        }
    }
}
