using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    string axisName = "horizontal1";
    public float moveSpeed = 10;
    public Rigidbody2D bombPrefab;
    public Transform bombSpawn;
    public Transform lastPos;
  
    // Update is called once per frame
    void FixedUpdate()
    {
        //Playerin hareketi
        float moveAxis = Input.GetAxis(axisName) * moveSpeed;
        GetComponent<Rigidbody2D>().velocity = new Vector2(moveAxis, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var bomb = Instantiate(bombPrefab, bombSpawn.position, Quaternion.identity);
            bomb.AddForce(transform.up * 1000f);
        }
    }
}
