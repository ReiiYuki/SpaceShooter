using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public GameObject bulletPrototype;
    public GameObject beamPrototype;

    private List<GameObject> bulletPool;
    private GameObject beam;

    private bool beamReady;

    // Use this for initialization
    void Start()
    {
        bulletPool = new List<GameObject>();
        beamReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        SpaceShipControl();
        Fire();
        FireBeam();
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
        Vector3 position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
        GameObject pickedBullet = pickupBulletFromPool();
        if (pickedBullet == null)
            bulletPool.Add(Instantiate(bulletPrototype, position, Quaternion.identity).GetComponent<BulletMovement>().SetDirection(1));
        else
            ActivateBullet(pickedBullet, position);
    }

    void ActivateBullet(GameObject bullet,Vector3 position)
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
    }

    void FireBeam()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)&&beamReady)
        {
            Vector3 position = new Vector3(transform.position.x, transform.position.y + 1.8f, transform.position.z);
            if (beam == null)
                beam = Instantiate(beamPrototype, position, Quaternion.identity).GetComponent<BulletMovement>().SetDirection(1);
            else
            {
                beam.SetActive(true);
                beam.transform.position = position;
            }
            beamReady = false;
            StartCoroutine(CoolDown());
        }   
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(3);
        beamReady = true;
    }
}   
