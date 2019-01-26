using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkController : MonoBehaviour {

    private MyGameController gc;
    private Talkable currentPerson;
    private bool hasTalked;

	void Start () {
        gc = GameObject.Find("GameController").GetComponent<MyGameController>();
        currentPerson = null;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.tag == "Talkable") {
            hasTalked = false;
            currentPerson = collision.GetComponent<Talkable>();
        }
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Talkable")
        {            
            currentPerson = null;
        }
    }

    private void Talk() {
        if (currentPerson != null) {
            hasTalked = true;
            gc.TalkToPlayer(currentPerson.scriptRef);
        }
    }

	void Update () {
        if (gc.PlayerCanMove())
        {
            if (!hasTalked && currentPerson != null && Input.GetKeyUp("space"))
            {
                Talk();
            }
        }
	}
}
