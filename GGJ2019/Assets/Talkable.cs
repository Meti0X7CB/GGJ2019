using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talkable : MonoBehaviour {
    
    public string characterName;
    public int scriptRef;
    public int scriptLimit;
    private MyGameController gc;

	void Start () {
        gc = GameObject.Find("GameController").GetComponent<MyGameController>();
	}
	
    public int GetScriptRef() {
        int temp = scriptRef;

        if (scriptRef == 10) { // you get the key for talking to this guy
            gc.SetKey(true);
        }

        if (scriptRef == 1) { // dummy script where triangle is waiting for the key

            if (gc.GetKey()) {
                scriptRef = 3;
                temp = 2;
            }

        } else {
            
            if (scriptRef < scriptLimit) // increment until you reach the upper limit
            {
                scriptRef += 1;
            }            
        }

        return temp;
    }

	void Update () {
		
	}
}
