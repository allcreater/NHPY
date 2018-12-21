using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDrop : MonoBehaviour
{
    public PrefabWithProbability[] possibleDrop;

    private bool quitFlag = false;

    private void OnApplicationQuit()
    {
        quitFlag = true;
    }

    private void OnDestroy()
    {
        if (quitFlag)
            return;

        for (int i = 0; i < possibleDrop.Length; ++i)
        {
            if (possibleDrop != null && Random.value <= possibleDrop[i].probabilityWeight)
                Instantiate(possibleDrop[i].prefab, transform.position, transform.rotation);
        }
    }
}
