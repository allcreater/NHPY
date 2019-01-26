using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class MathExtension
{
    //public int RandomWeightedIndex(IEnumerable<float> weights)
    //{
    //    float totalWeight = 0.0f;
    //    var weightSums = weights.Select(x => totalWeight += x).ToArray();

    //    float randomNumber = Random.Range(0, totalWeight);

    //    return weightSums.First(x => (x.Item2 > randomNumber)).prefab;
    //}

    public static System.Func<T> MakeWeightedRandomGenerator<T>(IEnumerable<(T, float)> weightedCollection)
    {
        float totalWeight = 0.0f;
        var weightSums = weightedCollection.Select(x => (x.Item1, totalWeight += x.Item2)).ToArray();

        return () => {
            float randomNumber = Random.Range(0, totalWeight);
            return weightSums.First(x => (x.Item2 > randomNumber)).Item1;
        };
    }

    public static T RandomWeightedSelect<T>(IEnumerable<(T, float)> weightedCollection) => MakeWeightedRandomGenerator(weightedCollection)();
}