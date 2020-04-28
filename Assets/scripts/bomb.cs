using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball top = collision.gameObject.GetComponent<Ball>();
        if (top == null) return;
        Destroy(gameObject);

        top.skorYap();
        if (top.direction)
        {
            top.transform.GetComponent<Rigidbody2D>().velocity = Vector2.right * 10;
        }
        else
        {
            top.transform.GetComponent<Rigidbody2D>().velocity = Vector2.left * 10;
        }
        
    }

}
