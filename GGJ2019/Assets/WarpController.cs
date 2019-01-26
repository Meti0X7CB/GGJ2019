using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpController : MonoBehaviour {

    private MyGameController gc;

	void Start () {
        gc = GameObject.Find("GameController").GetComponent<MyGameController>();
	}

    public IEnumerator FadeOut() {

        GameObject.Find("Blackout").GetComponent<Animator>().Play("blackout");

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("HouseScene", LoadSceneMode.Single);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (gc.GetKey() && collision.tag == "House") {
            StartCoroutine(FadeOut());   
        }
	}

	void Update () {
		
	}
}
