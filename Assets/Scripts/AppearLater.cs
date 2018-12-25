using System.Linq;
using System.Collections.Generic;
using UnityEngine;


//TODO: rewrite!!!!
public class AppearLater : MonoBehaviour
{
    public string tag = "AppearLater";
    public bool enabledAtStart = false;

    private List<GameObject> controlledObjects;

    public void SetActiveAll(bool enable)
    {
        foreach (var obj in controlledObjects)
        {
            obj.SetActive(enable);
        }
    }

    private void Awake()
    {
        controlledObjects = GameObject.FindGameObjectsWithTag(tag).ToList();

        SetActiveAll(enabledAtStart);
    }


}
