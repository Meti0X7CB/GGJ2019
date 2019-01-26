using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private MyGameController gc;
    private Rigidbody2D rb;
    public float force;

	void Start () {
        gc = GameObject.Find("GameController").GetComponent<MyGameController>();
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {

        if (gc.PlayerCanMove())
        {
            if (Input.GetKey("w") || Input.GetKey("up"))
            {
                rb.AddForce(Vector2.up);

            }
            if (Input.GetKey("d") || Input.GetKey("right"))
            {
                rb.AddForce(Vector2.right);

            }
            if (Input.GetKey("s") || Input.GetKey("down"))
            {
                rb.AddForce(Vector2.down);

            }
            if (Input.GetKey("a") || Input.GetKey("left"))
            {
                rb.AddForce(Vector2.left);

            }
        }
	}
}
