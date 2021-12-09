using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIManager : MonoBehaviour, IPointerClickHandler
{
    public List<GameObject> RoadTilePrefab;
    private List<GameObject> _RoadTileName = new List<GameObject>();

    public GameObject RoadTileContent;
    public GameObject RoadTileNamePrefab;

    private GraphicRaycaster raycaster;

    private void Start()
    {
        InitializeScrollViewContent();

        raycaster = GetComponentInParent<GraphicRaycaster>();
    }

    private void InitializeScrollViewContent()
    {
        for(int count = 0; count<RoadTilePrefab.Count; count++)
        {
            RoadTileNamePrefab.GetComponent<TextMeshProUGUI>().text = RoadTilePrefab[count].gameObject.name;
            _RoadTileName.Add(RoadTileNamePrefab);
            GameObject go = Instantiate(RoadTileNamePrefab);
            go.transform.name = RoadTilePrefab[count].gameObject.name;
            go.transform.SetParent(RoadTileContent.transform);
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
      
        List<RaycastResult> results = new List<RaycastResult>();

        raycaster.Raycast(pointerEventData, results);

        foreach (RaycastResult result in results)
            if (result.gameObject.tag.Equals("RoadTile"))
                Debug.Log(result.gameObject.transform.GetSiblingIndex());

        
    }
}
