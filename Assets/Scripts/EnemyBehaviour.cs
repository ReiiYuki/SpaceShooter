using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    private int health;

	// Use this for initialization
	void Start () {
        health = 3;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        hit();
    }

    void hit()
    {
        health -= 1;
        if (health==0)
            Destroy(gameObject);
    }
}
