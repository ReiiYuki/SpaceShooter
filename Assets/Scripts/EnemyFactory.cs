using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour {

    public GameObject enemyPrototype;

    private int level;

    private List<GameObject> enemyPool;

	// Use this for initialization
	void Start () {
        enemyPool = new List<GameObject>();
        level = 1;
        AddEnemy();
	}
	
	// Update is called once per frame
	void Update () {
        RespawnEnemy();
	}

    void RespawnEnemy()
    {
        foreach (GameObject enemy in enemyPool)
            if (!enemy.activeSelf)
            {
                level++;
                Vector3 position = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-1.5f, 1.5f), 0);
                enemy.SetActive(true);
                enemy.GetComponent<EnemyBehaviour>().SetLevel(level);
                AddEnemy();
                return;
            }
    }
    void AddEnemy()
    {
        Vector3 position = new Vector3(Random.Range(-2.5f,2.5f),Random.Range(-1.5f,1.5f),0);
        enemyPool.Add(Instantiate(enemyPrototype, position, Quaternion.identity).GetComponent<EnemyBehaviour>().SetLevel(level));
    }
}
