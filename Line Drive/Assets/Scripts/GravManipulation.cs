using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravManipulation : MonoBehaviour
{

    public float gravLevel;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(this.gameObject.GetComponent<BoxCollider2D>().isTrigger);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        other.gameObject.GetComponent<Rigidbody2D>().gravityScale = gravLevel;
        Debug.Log("NoGRAV");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
        Debug.Log("NoGRAV");
    }


}
