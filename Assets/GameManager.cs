using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class Map
    {
        [System.Serializable]
        public class Road
        {
            public int id;
            public int x, y, z;

            public Vector3Int position => new Vector3Int(x, y, z);

            public Road(int id, Vector3Int position)
            {
                this.id = id;

                x = position.x;
                y = position.y;
                z = position.z;
            }
        }

        public List<Road> roads;

        public Map(Dictionary<Vector3Int, int> map)
        {
            roads = new List<Road>(map.Count);

            foreach (var road in map)
                roads.Add(new Road(road.Value, road.Key));
        }

        public Dictionary<Vector3Int, int> ToDictionary()
        {
            Dictionary<Vector3Int, int> dictionary = new Dictionary<Vector3Int, int>(roads.Count);

            foreach (Road road in roads)
                dictionary.Add(road.position, road.id);

            return dictionary;
        }
    }

    private static GameManager instance;

    private int currentId;
    private GameObject current;

    private bool pointerInUI = false;

    private Dictionary<Vector3Int, int> roads;

    private void Awake()
    {
        instance = this;

        if (File.Exists(Path.Combine(Application.persistentDataPath, "save.json")))
        {
            Map map = JsonUtility.FromJson<Map>(File.ReadAllText(Path.Combine(Application.persistentDataPath, "save.json")));

            roads = map.ToDictionary();
        }
        else
            roads = new Dictionary<Vector3Int, int>();
    }

    private void Start()
    {
        foreach (KeyValuePair<Vector3Int, int> road in roads)
            Instantiate(PrefabManager.GetRoad(road.Value), transform).transform.position = road.Key;

        CreateCurrent();
    }

    private void Update()
    {
        if (!current)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.transform.tag);

            if (hit.transform.tag == "Board" && !pointerInUI)
            {
                current.SetActive(true);

                Vector3Int position = Vector3Int.RoundToInt(hit.point);

                current.transform.position = position;

                if (Input.GetMouseButton(0))
                {
                    if (!roads.ContainsKey(position))
                    {
                        roads.Add(Vector3Int.RoundToInt(current.transform.position), currentId);

                        FreeCurrent();
                    }
                    else if (Input.GetMouseButtonDown(0))
                    {
                        // Selectionner, surligner, tourner
                    }
                }
            }
            else
                current.SetActive(false);
        }
        else if (current)
            current.SetActive(false);
    }

    private void FreeCurrent()
    {
        if (current)
        {
            current.transform.parent = null;
            current = null;
        }

        CreateCurrent();
    }

    private void CreateCurrent()
    {
        current = Instantiate(PrefabManager.GetRoad(currentId), transform);
    }

    public static void SetCurrentPrefab(int id)
    {
        instance.currentId = id;

        if (instance.current)
            Destroy(instance.current);

        instance.CreateCurrent();
    }

    private void OnDestroy()
    {
        Map map = new Map(roads);

        File.WriteAllText(Path.Combine(Application.persistentDataPath, "save.json"), JsonUtility.ToJson(map, true));
    }

    public static void PointerInUI()
    {
        instance.pointerInUI = true;
    }

    public static void PointerNotInUI()
    {
        instance.pointerInUI = false;
    }
}