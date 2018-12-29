using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloomAttack : MonoBehaviour
{
    public GameObject death;

    public float damageRadius;
    public float damage;

    void Update()
    {
        if (death.activeInHierarchy == false)
            return;
        damageRadius = 0.001f;
        damage = 0.001f;
    }
}