using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceList : MonoBehaviour
{
    public string ResourceType = "Leituras";

    public SimpleScroll simpleScroll;

    public GameObject ResourceButtonPrefab;
    // Start is called before the first frame update
    void Start()
    {
        UpdateList();
    }

    // Update is called once per frame
    void UpdateList()
    {
        Debug.Log(Game.Resources.resources.Count);
        List<GameObject> resourceButtons = new List<GameObject>();
        foreach (var resource in Game.Resources.resources)
        {
            var resourceButton = Instantiate(ResourceButtonPrefab);
            resourceButton.GetComponentInChildren<TextMeshProUGUI>().SetText(resource.name);
        }
        simpleScroll.AddList(resourceButtons);
    }
}
