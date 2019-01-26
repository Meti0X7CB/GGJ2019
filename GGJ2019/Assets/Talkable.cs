using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talkable : MonoBehaviour {
    
    public string characterName;
    private int scriptRef;

	void Start () {
		
	}
	
    public int GetScriptRef() {
        int temp = scriptRef;
        scriptRef += 1;
        return temp;
    }

	void Update () {
		
	}
}
