using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreSetup : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Text>().text = "Highscore : "+( PlayerPrefs.GetInt("highscore") > 0 ? PlayerPrefs.GetInt("highscore") +"" : "0");
    }

}
