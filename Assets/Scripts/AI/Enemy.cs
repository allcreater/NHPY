using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyManager manager;

    private void Awake()
    {
        manager = GetComponentInParent<EnemyManager>();
    }

    private void Start()
    {
        manager.RegisterNpc(this);
    }

    private void OnDestroy()
    {
        manager.UnregisterNpc(this);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
