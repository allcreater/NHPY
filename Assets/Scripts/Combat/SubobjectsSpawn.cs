using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubobjectsSpawn : MonoBehaviour
{
    public GameObject[] prefabs;
    public int numberOfObjects;
    public Vector3 spawnOffset;

    [Header("For RigidBodies")]
    public float scatteringSpeed = 2.0f;
    public float verticalSpeed = 2.0f;

    void CollidedWith(Collider other)
    {
        for (int i = 0; i < numberOfObjects; ++i)
        {
            var obj = GameObject.Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform.position + spawnOffset, new Quaternion(), null);

            var rigidBody = obj.GetComponent<Rigidbody>();
            if (rigidBody)
                rigidBody.velocity = Random.insideUnitSphere * scatteringSpeed + Vector3.up * verticalSpeed;
        }

        GameObject.Destroy(gameObject);
    }

}
