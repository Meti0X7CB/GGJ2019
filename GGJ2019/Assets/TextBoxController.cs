using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxController : MonoBehaviour {

    public float PrintSpeed;
    private MyGameController gc;
    private Text text;
    private bool nextEvent;
    private GameObject nextPromptContinue;
    private GameObject nextPromptEnd;
    private bool isPrinting;
    private RectTransform backgroundRect;
    private GameObject infoBox;
    private Text infoText;

	void Start () {
        gc = GameObject.Find("GameController").GetComponent<MyGameController>();
        text = transform.Find("Text").gameObject.GetComponent<Text>();
        nextEvent = false; // if the user has requested the next dialogue
        nextPromptContinue = transform.Find("NextContinue").gameObject;
        nextPromptEnd = transform.Find("NextEnd").gameObject;
        backgroundRect = transform.Find("TextBoxBackground").GetComponent<RectTransform>();

        // info box
        infoBox = transform.Find("InfoBox").gameObject;
        infoText = transform.Find("InfoText").GetComponent<Text>();

        // game controller
        gc.setTextBoxController(GetComponent<TextBoxController>());

        // DEBUG
        //string max_msg = "Lorem Lorem\nLorem Lorem\nipsum dolor\nsit amet, consectetur\nadipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";
        //StartCoroutine(ShowText(max_msg));
	}
	
    //
    // Public
    //

    public void ShowInfo(string msg) {
        infoText.text = msg;
        infoBox.SetActive(true);
    }

    public void HideInfo() {
        infoText.text = "";
        infoBox.SetActive(false);
    }

    public void ClickEvent() {
        nextEvent = true;
        HideNextPrompt();
    }

    public IEnumerator ShowText(string msg)
    {
        // will show the given text on the box delimited by 
        // click events, and hide the text box once complete
        RaiseBox();

        // display msg on the text box in chunks of maxMsgLength
        List<string> chunks = ChunkMsg(msg);

        /*
        foreach (string chunk in chunks) {
            Debug.Log(chunk);
        } */

        // show next dialogue until there is none left
        for (int i = 0; i < chunks.Count; i++) {

            // show the next text
            bool isLast = (i == chunks.Count - 1);

            isPrinting = true;
            StartCoroutine(PushText(chunks[i], isLast));
            yield return new WaitUntil(() => !isPrinting);
            ShowNextPrompt(isLast);

            // wait for the user to press next
            yield return new WaitUntil(() => nextEvent);
            nextEvent = false;

        }

        // clear text box
        text.text = "";

        // hide the text box
        LowerBox();

    }

    //
    // Private
    //

    private IEnumerator PushText(string msg, bool isFinal) {
        text.text = "";
        foreach (char c in msg) {
            yield return new WaitForSeconds(PrintSpeed);
            text.text += c;
        }
        isPrinting = false;
    }

    private void RaiseBox() {
        // the text box should animate up onto the screen
        gc.SetPlayerMovement(false);
        backgroundRect.anchoredPosition = new Vector2(
            backgroundRect.anchoredPosition.x,
            50f
        );
    } 

    private void LowerBox() {
        gc.SetPlayerMovement(true);
        // the text box should lower until it is hidden off screen
        backgroundRect.anchoredPosition = new Vector2(
            backgroundRect.anchoredPosition.x,
            -50f
        );
    }

    private List<string> ChunkMsg(string msg) {        

        List<string> chunks = new List<string>();
        int length = 4; // number of lines to show
        List<string> msgBlocks = msg.Split('\n').ToList<string>();
        int numChunks = (int)Mathf.Ceil( msgBlocks.Count / length);

        for (int i = 0; i < numChunks; i++)
        {
            chunks.Add(string.Join("\n", msgBlocks.Take(4).ToArray()));

            int times = Mathf.Min(4, msgBlocks.Count);
            while (times > 0) {
                msgBlocks.RemoveAt(0);
                times -= 1;
            }
        }
        return chunks;
    }

    private void ShowNextPrompt(bool isFinal) {
        if (!isFinal) {
            nextPromptContinue.SetActive(true);

        } else {
            nextPromptEnd.SetActive(true);

        }
    }

    private void HideNextPrompt() {
        nextPromptContinue.SetActive(false);
        nextPromptEnd.SetActive(false);
    }

    /*
    private int MaxMsgLength() {
        // returns the max text length that the box can show
        string max_msg = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";
        return max_msg.Length;
    } */

	void Update () {
        // only react to text advancment if the player can't move
        if (!gc.PlayerCanMove())
        {
            if (!isPrinting && Input.GetKeyDown("space"))
            {
                ClickEvent();
            }
        }
	}
}
