using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour 
{
    public GameObject bulletPrototype;
    public float bulletSpeed = 10.0f;
    public float reloadTime = 0.5f;

    private float reloadingTimer = 0.0f;
    // Use this for initialization
    void Start () 
    {
        
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetAxis("Fire1") > 0.0f && reloadingTimer > reloadTime)
        {
            var bullet = GameObject.Instantiate(bulletPrototype, transform.position, Random.rotation);
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;

            reloadingTimer = 0.0f;
        }

        reloadingTimer += Time.deltaTime;
    }
}
