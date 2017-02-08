using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {

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

}
