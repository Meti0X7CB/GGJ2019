using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class MyGameController : MonoBehaviour {

    private bool playerCanMove;
    private TextBoxController textBoxController;
    private bool key;
    private bool willVanish;
    private bool gameEnds;

	void Start () {
        playerCanMove = SceneManager.GetActiveScene().name == "HouseScene";
        key = false;
        willVanish = false;
        gameEnds = false;
	}
	
    public void setTextBoxController(TextBoxController obj) {
        textBoxController = obj;

        // intro text
        if (SceneManager.GetActiveScene().name != "HouseScene") {            
            int intro = -1;
            TalkToPlayer(intro);
        }
    }

    public void SetPlayerMovement(bool state) {
        playerCanMove = state;
    }

    private string ReadTextFile(int scriptRef) {
        TextAsset txt = (TextAsset)Resources.Load(scriptRef.ToString(), typeof(TextAsset));
        //string content = txt.text;
        //string fileName = "Assets/Resources/" + scriptRef.ToString() + ".txt";
        //StreamReader reader = new StreamReader(fileName, System.Text.Encoding.UTF8);
        //return reader.ReadToEnd();
        return txt.text;
    }

    public void SetKey(bool status) {
        key = status;
    }

    public bool GetKey () {
        return key;
    }

    private IEnumerator GameComplete() {
        GameObject.Find("Redout").GetComponent<Animator>().Play("Redout");
        GameObject.Find("ParticleSystem2").GetComponent<ParticleSystem>().Play();
        GameObject.Find("YouWin").GetComponent<Canvas>().enabled = true;
        yield return null;
    }

    public void dialogFinishes() {
        if (willVanish) StartCoroutine(Smoke());
        if (gameEnds) StartCoroutine(GameComplete());
    }

    private IEnumerator Smoke()
    {
        GameObject.Find("ParticleSystem1").GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.5f);
        Destroy(GameObject.Find("circle").gameObject);
    }

    public void TalkToPlayer(int scriptRef) {
        // player is frozen while talking
        HideInfo();
        if (scriptRef == 2) willVanish = true; // prep circle to vanish
        if (scriptRef == 4) gameEnds = true; // prep game to end 
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
