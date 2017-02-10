using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class GameCore : MonoBehaviour {

    bool isStart;
    float time;
    int finalScore;
    private GameObject endLabel;
    private String firebaseResult;
	// Use this for initialization
	void Start () {
        isStart = true;
        time = 0;
        endLabel = GameObject.FindGameObjectWithTag("EndLabel");
        endLabel.SetActive(false);
        firebaseResult = "";
    }
	
	// Update is called once per frame
	void Update () {
        if (isStart)
        {
            time += Time.deltaTime;
            GameObject.FindGameObjectWithTag("TimeText").GetComponent<Text>().text = (int) (60-time)+"";
        }
        if (IsEnd())
            DeactiveEnvironment();
        if (isWin())
        {
            endLabel.SetActive(true);
            endLabel.GetComponent<Text>().text = "YOU WIN\nScore : " + finalScore + firebaseResult + "\nPress R to play again.\n\nDeveloped By ReiiYuki";
        }
        if (IsOver())
        {
            endLabel.SetActive(true);
            endLabel.GetComponent<Text>().text = "YOU LOSE\nScore : " + finalScore + firebaseResult + "\nPress R to play again.\n\nDeveloped By ReiiYuki";
        }
        if (!IsEnd())
            Warp();
        RestartGame();

    }

    bool IsOver()
    {
        return GameObject.FindGameObjectWithTag("Player")==null;
    }

    bool IsEnd()
    {
        return IsOver() || time >= 60;
    }

    bool isWin()
    {
        return IsEnd() && !IsOver();
    }

    void DeactiveEnvironment()
    {
        if (GameObject.FindGameObjectWithTag("Factory") != null)
        {
            finalScore = GameObject.FindGameObjectWithTag("Factory").GetComponent<EnemyFactory>().GetLevel();
            UpdateScore();
            GameObject.FindGameObjectWithTag("Factory").SetActive(false);
        }
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
            if (obj.transform.position.x > 3)
                obj.transform.position = new Vector3(-3f,obj.transform.position.y);
            if (obj.transform.position.x < -3)
                obj.transform.position = new Vector3(3f, obj.transform.position.y);
            if (obj.transform.position.y > 2)
                obj.transform.position = new Vector3(obj.transform.position.x, -2f);
            if (obj.transform.position.y < -2)
                obj.transform.position = new Vector3(obj.transform.position.x, 2f);
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player.transform.position.x > 3)
            player.transform.position = new Vector3(-3f, player.transform.position.y);
        if (player.transform.position.x < -3)
            player.transform.position = new Vector3(3f, player.transform.position.y);
        if (player.transform.position.y > 2)
            player.transform.position = new Vector3(player.transform.position.x, -2f);
        if (player.transform.position.y < -2)
            player.transform.position = new Vector3(player.transform.position.x, 2f);
    }

    void RestartGame()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);
    }

    void UpdateScore()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://spaceshooter-6d899.firebaseio.com/");
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        Score currentScore = new Score(finalScore);
        string json = JsonUtility.ToJson(currentScore);
        reference.Child("score").Child(DateTime.Now.Ticks + "").SetRawJsonValueAsync(json);
        FirebaseDatabase.DefaultInstance
          .GetReference("score").OrderByChild("score").LimitToLast(20)
          .GetValueAsync().ContinueWith(task => {
              if (task.IsFaulted)
              {
                  firebaseResult = "";
              }
              else if (task.IsCompleted)
              {
                  DataSnapshot snapshot = task.Result;
                  int heightScore = 0;
                  long yourRank = 0;
                  long count = snapshot.ChildrenCount;
                  foreach (DataSnapshot snap in snapshot.Children)
                  {
                      heightScore = Int32.Parse(snap.Child("score").Value.ToString());
                      if (finalScore == heightScore)
                          yourRank = count;
                      count--;
                  }
                  string rank = yourRank == 0 ? "Out of Rank" : (yourRank + "");
                  firebaseResult = "\nRank : " + rank + "\nHeighest Score in Server : " + heightScore;
              }
          });
    }

    class Score
    {
        public int score;
        public Score(int inScore)
        {
            score = inScore;
        }
    }

    class ScoreList
    {
        public List<Score> list;
        public ScoreList(List<Score> inList)
        {
            list = inList;
        }
    }
}
