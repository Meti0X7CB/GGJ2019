using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFadeController : MonoBehaviour {

    public SpriteRenderer buildings;

	void Start () {
		
	}
	
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            Color c = buildings.color;
            c.a = 0.5f;
            buildings.color = c;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Color c = buildings.color;
            c.a = 1f;
            buildings.color = c;
        }
    }

	void Update () {
		
	}
}
