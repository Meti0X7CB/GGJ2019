using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour {

    public GameObject player;

	void Start () {
	    	
	}
	
	void Update () {        

        // camera follows player
        transform.position = new Vector3(
            player.transform.position.x,
            player.transform.position.y,
            transform.position.z
        );
	}
}
