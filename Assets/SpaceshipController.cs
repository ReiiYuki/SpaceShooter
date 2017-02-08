using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpaceShipControl();
    }

    void SpaceShipControl()
    {
        transform.Translate(Input.GetAxis("Horizontal") * Vector3.right * Time.deltaTime * 5f);
        transform.Translate(Input.GetAxis("Vertical") * Vector3.up * Time.deltaTime * 5f);
    }
}
