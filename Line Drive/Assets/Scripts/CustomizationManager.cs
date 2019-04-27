using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizationManager : MonoBehaviour
{
    public GameObject[] balls;          // list of ball customizations
    public GameObject[] trails;         // list of trail customizations
    public float trailtime;             // how long for the trail to fade

    private GameObject currentBall;     // the currently equipped ball
    private GameObject currentTrail;    // the current trail. **NOTE** Can be null

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);  // don't destroy this instance
        currentBall = balls[PlayerPrefs.GetInt("Ball", 0)];
        int id = PlayerPrefs.GetInt("Trail", -1); 
        if(id > -1)
        {
            currentTrail = trails[id];
        }
        else
        {
            currentTrail = null;
        }
    }

    // checks if current trail is null
    public bool HasTrail()
    {
        return currentTrail != null;
    }

    // returns the current ball
    public SpriteRenderer GetCurrentBall()
    {
        return currentBall.GetComponent<SpriteRenderer>();
    }

    // returns the current trail
    public TrailRenderer GetCurrentTrail()
    {
        return currentTrail != null ? currentTrail.GetComponent<TrailRenderer>() : null;
    }

    // sets the current ball
    public void UpdateCurrentBall(int id)
    {
        if (id > balls.Length || id < 0) return;

        currentBall = balls[id];
    }

    // sets the current trail (non-null)
    public void UpdateCurrentTrail(int id)
    {
        if (id > trails.Length || id < 0) return;

        currentTrail = trails[id];
    }

    // sets the current trail to null
    public void ResetTrail()
    {
        currentTrail = null;
    }
}
