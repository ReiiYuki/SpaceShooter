﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {

    public Sprite[] bulletTypes;

	// Use this for initialization
	void Start () {
        RandomChosingSpriteType();
    }
	
	// Update is called once per frame
	void Update () {
        Move();
        BackToPool();
    }

    void Move()
    {
        if (gameObject.activeSelf)
            transform.Translate(Vector3.up * Time.deltaTime * 5f);
    }

    void BackToPool()
    {
        if (transform.position.y > Camera.main.transform.position.y + 2)
            gameObject.SetActive(false);
    }
    
    public void RandomChosingSpriteType()
    {
        int index = Random.Range(0, bulletTypes.Length-1);
        GetComponent<SpriteRenderer>().sprite = bulletTypes[index];
    }
}