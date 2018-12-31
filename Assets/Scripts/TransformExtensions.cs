using UnityEngine;
using System.Collections.Generic;
using System.Linq;

internal static class Ext
{
    internal static Transform FindChildByRecursion(this Transform aParent, string aName)
    {
        if (aParent == null) return null;
        var result = aParent.Find(aName);
        if (result != null)
            return result;
        foreach (Transform child in aParent)
        {
            result = child.FindChildByRecursion(aName);
            if (result != null)
                return result;
        }
        return null;
    }

    internal static IEnumerable<Transform> GetChildren(this Transform parent)
    {
        return Enumerable.Range(0, parent.childCount).Select(i => parent.GetChild(i));
    }
}
