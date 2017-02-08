using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public GameObject bullet;

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
            Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
    }

    void SpawnBullet()
    {

    }
}
