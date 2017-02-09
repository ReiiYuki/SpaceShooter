using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviour {

    bool isStart;
    float time;

	// Use this for initialization
	void Start () {
        isStart = true;
        time = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (isStart)
            time += Time.deltaTime;
        if (IsEnd())
            DeactiveEnvironment();
        if (isWin())
            Debug.Log("Win");
	}

    bool IsOver()
    {
        return GameObject.FindGameObjectWithTag("Player")==null;
    }

    bool IsEnd()
    {
        return IsOver() || time >= 15;
    }

    bool isWin()
    {
        return IsEnd() && !IsOver();
    }

    void DeactiveEnvironment()
    {
        if (GameObject.FindGameObjectWithTag("Factory")!=null)
            GameObject.FindGameObjectWithTag("Factory").SetActive(false);
        if (GameObject.FindGameObjectsWithTag("Enemy").Length>0)
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                enemy.SetActive(false);
        if (GameObject.FindGameObjectWithTag("Player") != null)
            GameObject.FindGameObjectWithTag("Player").SetActive(false);
        if (isStart)
            isStart = false;
    }
}
