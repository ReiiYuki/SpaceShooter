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
        if (IsOver())
            Debug.Log("Lose");
        if (!IsEnd())
            Warp();
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

    void Warp()
    {
        if (IsEnd())
            return;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy")){
            if (obj.transform.position.x > 3.5)
                obj.transform.position = new Vector3(-3.5f,obj.transform.position.y);
            if (obj.transform.position.x < -3.5)
                obj.transform.position = new Vector3(3.5f, obj.transform.position.y);
            if (obj.transform.position.y > 2)
                obj.transform.position = new Vector3(obj.transform.position.x, -2f);
            if (obj.transform.position.y < -2)
                obj.transform.position = new Vector3(obj.transform.position.x, 2f);
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player.transform.position.x > 3.5)
            player.transform.position = new Vector3(-3.5f, player.transform.position.y);
        if (player.transform.position.x < -3.5)
            player.transform.position = new Vector3(3.5f, player.transform.position.y);
        if (player.transform.position.y > 2)
            player.transform.position = new Vector3(player.transform.position.x, -2f);
        if (player.transform.position.y < -2)
            player.transform.position = new Vector3(player.transform.position.x, 2f);
    }
}
