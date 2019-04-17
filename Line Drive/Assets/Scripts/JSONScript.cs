using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class JSONObject
{
    public string name;
    public int score;
}


public class JSONScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        JSONObject score = new JSONObject();
        score.name = "Default";
        score.score = 1000;

        string json = JsonUtility.ToJson(score);

        Debug.Log(json);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
