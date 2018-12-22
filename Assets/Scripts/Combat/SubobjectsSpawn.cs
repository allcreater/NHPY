using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SubobjectsSpawn : MonoBehaviour
{
    public GameObject[] prefabs;
    public int numberOfObjects;
    public Vector3 spawnOffset;
    public float explodeDelay = 0.0f;

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

            StartCoroutine(TemporaryDisableColliders(obj));
        }

        GameObject.Destroy(gameObject, explodeDelay + 1f);
    }

    private void OnDestroy()
    {
        Debug.Log("Destroy");
    }

    IEnumerator TemporaryDisableColliders(GameObject self)
    {
        if (Mathf.Approximately(explodeDelay, 0.0f))
            yield break;

        var collidersThatShouldBeDisabled = self.GetComponentsInChildren<Collider>().Where(x => x.enabled).ToList();
        foreach (var collider in collidersThatShouldBeDisabled)
            collider.enabled = false;

        yield return new WaitForSeconds(explodeDelay);

        Debug.Log($"{gameObject.name} enabled");
        foreach (var collider in collidersThatShouldBeDisabled)
            collider.enabled = true;
    }
}
