using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float forceAmt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.name == "Ball")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * forceAmt);
        }
    }
}
