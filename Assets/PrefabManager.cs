using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    private static PrefabManager instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private List<GameObject> prefabs;

    public static string[] GetRoadNames()
    {
        return instance.prefabs.Select(prefab => prefab.name).ToArray();
    }

    public static GameObject GetRoad(int id)
    {
        return instance.prefabs[id];
    }
}
