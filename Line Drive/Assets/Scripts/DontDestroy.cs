using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour {

    // Use this for initialization
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
        if(objs.Length > 1)
        {
            Destroy(this.gameObject);
            Debug.Log("YEE");
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
