﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public GameObject bulletPrototype;

    private List<GameObject> bulletPool;

    // Use this for initialization
    void Start()
    {
        bulletPool = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        SpaceShipControl();
        Fire();
    }

    void SpaceShipControl()
    {
        transform.Translate(Input.GetAxis("Horizontal") * Vector3.right * Time.deltaTime * 5f);
        transform.Translate(Input.GetAxis("Vertical") * Vector3.up * Time.deltaTime * 5f);
    }

    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SpawnBullet();       
    }

    void SpawnBullet()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z);
        GameObject pickedBullet = pickupBulletFromPool();
        if (pickedBullet == null)
            bulletPool.Add(Instantiate(bulletPrototype, position, Quaternion.identity));
        else
            ActivateBullet(pickedBullet, position);
    }

    void ActivateBullet(GameObject bullet,Vector3 position)
    {
        bullet.transform.position = position;
        bullet.GetComponent<BulletMovement>().active = false;
    }

    GameObject pickupBulletFromPool()
    {
        foreach (GameObject bullet in bulletPool)
        {
            if (bullet.GetComponent<BulletMovement>().active)
                return bullet;
        }
        return null;
    }

}   
