using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour {

    public GameObject player;
    private Rigidbody2D rb;
    public float force;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {

        // get player direction
        Vector3 player_direction = (player.transform.position - transform.position).normalized;
        player_direction.z = transform.position.z;

        // apply force in player direction
        rb.AddForce(player_direction * force);
	}
}
