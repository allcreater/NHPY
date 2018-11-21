using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour 
{
    public GameObject bulletPrototype;
    public float bulletSpeed = 10.0f;
    public float reloadTime = 0.5f;

    private float reloadingTimer = 0.0f;

    private Vector3 m_prevPos;
    // Use this for initialization
    void Start () 
    {
        m_prevPos = transform.position;
    }

    // Update is called once per frame
    void Update ()
    {

    }

    void FixedUpdate()
    {
        //V = dx/dt. We can not just ask rigid body because it's position could be manually changed, also not every shooter could have rigidbody
        var pos = transform.position;
        var velocity = (pos - m_prevPos) / Time.fixedDeltaTime;
        m_prevPos = pos;

        if (Input.GetAxis("Fire1") > 0.0f && reloadingTimer > reloadTime)
        {
            var bullet = GameObject.Instantiate(bulletPrototype, transform.position, Random.rotation);
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed + velocity;

            reloadingTimer = 0.0f;
        }

        reloadingTimer += Time.deltaTime;
    }
}
