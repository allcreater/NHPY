using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class Shooting : MonoBehaviour 
{
    public GameObject bulletPrototype;
    public float bulletSpeed = 10.0f;
    public float reloadTime = 0.5f;

    private float reloadingTimer = 0.0f;

    private Vector3 m_prevPos, m_velocity;
    // Use this for initialization
    void Start () 
    {
        m_prevPos = transform.position;
    }

    // Update is called once per frame
    void Update ()
    {
        reloadingTimer += Time.deltaTime;
    }

    void ShootTo(Vector3 direction)
    {
        if (reloadingTimer > reloadTime)
        {
            var bullet = GameObject.Instantiate(bulletPrototype, transform.position, Random.rotation);
            bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed + m_velocity;

            reloadingTimer = 0.0f;
        }
    }

    void FixedUpdate()
    {
        //V = dx/dt. We can not just ask rigid body because it's position could be manually changed, also not every shooter could have rigidbody
        m_velocity = (transform.position - m_prevPos) / Time.fixedDeltaTime;
        m_prevPos = transform.position;
    }
}
