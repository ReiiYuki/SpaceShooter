﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public GameObject bulletPrototype;

    private List<GameObject> bulletPool;

    private int health;

	// Use this for initialization
	void Start () {
        health = 3;
        bulletPool = new List<GameObject>();
        InvokeRepeating("SpawnBullet", 2, 2);
    }
	
	// Update is called once per frame
	void Update () {
        Follow();
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

    void SpawnBullet()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z);
        GameObject pickedBullet = pickupBulletFromPool();
        if (pickedBullet == null)
            bulletPool.Add(Instantiate(bulletPrototype, position, Quaternion.identity).GetComponent<BulletMovement>().SetDirection(-1));
        else
            ActivateBullet(pickedBullet, position);
    }

    void ActivateBullet(GameObject bullet, Vector3 position)
    {
        bullet.transform.position = position;
        bullet.SetActive(true);
        bullet.GetComponent<BulletMovement>().RandomChosingSpriteType();
    }

    GameObject pickupBulletFromPool()
    {
        foreach (GameObject bullet in bulletPool)
            if (!bullet.activeSelf)
                return bullet;
        return null;
    }

    void Follow()
    {
        Vector3 position = GameObject.FindGameObjectWithTag("Player").transform.position-transform.position;
        transform.Translate(position * Time.deltaTime * 2f ,Space.World);
    }
}