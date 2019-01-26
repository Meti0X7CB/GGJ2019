using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MyGameController : MonoBehaviour {

    private bool playerCanMove;
    private TextBoxController textBoxController;
    private bool key;

	void Start () {
        playerCanMove = true;
        key = false;
	}
	
    public void setTextBoxController(TextBoxController obj) {
        textBoxController = obj;
    }

    public void setPlayerMovement(bool state) {
        playerCanMove = state;
    }

    private string ReadTextFile(int scriptRef) {
        string fileName = "Assets/Resources/" + scriptRef.ToString() + ".txt";
        StreamReader reader = new StreamReader(fileName);
        return reader.ReadToEnd();
    }

    public void SetKey(bool status) {
        key = status;
    }

    public bool GetKey () {
        return key;
    }

    public void TalkToPlayer(int scriptRef) {
        // player is frozen while talking
        HideInfo();
        StartCoroutine(
            textBoxController.ShowText(ReadTextFile(scriptRef))
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
