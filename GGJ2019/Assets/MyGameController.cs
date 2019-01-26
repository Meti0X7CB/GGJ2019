using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameController : MonoBehaviour {

    private bool playerCanMove;
    private TextBoxController textBoxController;

	void Start () {
        playerCanMove = true;
	}
	
    public void setTextBoxController(TextBoxController obj) {
        textBoxController = obj;
    }

    public void setPlayerMovement(bool state) {
        playerCanMove = state;
    }

    public void TalkToPlayer(int scriptRef) {      
        // player is frozen while talking
        HideInfo();
        StartCoroutine(
            textBoxController.ShowText("Testing1\nTesting2\nTesting3\nTesting4\nTesting5\nTesting6\nTesting7")
        );
    }

    public bool PlayerCanMove() {
        return playerCanMove;
    }

    public void ShowInfo(string msg)
    {
        textBoxController.ShowInfo(msg);
    }

    public void HideInfo()
    {
        textBoxController.HideInfo();
    }

	void Update () {
		
	}
}
