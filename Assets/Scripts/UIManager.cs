using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIManager : MonoBehaviour, IPointerClickHandler
{
    public GameObject RoadTileContent;
    public GameObject RoadTileNamePrefab;

    private GraphicRaycaster raycaster;

    private void Start()
    {
        raycaster = GetComponentInParent<GraphicRaycaster>();

        string[] names = PrefabManager.GetRoadNames();

        for (int count = 0; count < names.Length; count++)
        {
            RoadTileNamePrefab.GetComponent<TextMeshProUGUI>().text = names[count];

            GameObject go = Instantiate(RoadTileNamePrefab);
            go.transform.name = names[count];
            go.transform.SetParent(RoadTileContent.transform);
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
      
        List<RaycastResult> results = new List<RaycastResult>();

        raycaster.Raycast(pointerEventData, results);

        foreach (RaycastResult result in results)
            if (result.gameObject.tag.Equals("RoadTile"))
                GameManager.SetCurrentPrefab(result.gameObject.transform.GetSiblingIndex());
    }
}
