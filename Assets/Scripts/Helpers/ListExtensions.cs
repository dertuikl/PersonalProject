using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public static class ListExtensions
{
    public static T RandomElement<T>(this IEnumerable<T> enumerable) => enumerable.ElementAt(Random.Range(0, enumerable.Count()));
}