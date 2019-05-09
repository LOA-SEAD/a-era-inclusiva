using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceList : MonoBehaviour
{
    private string _resourceCategory;

    public string ResourceCategory
    {
        get => _resourceCategory;
        set { _resourceCategory = value;  UpdateList();}
        
    }

    public SimpleScroll simpleScroll;

    public GameObject resourceButtonPrefab;
    // Start is called before the first frame update
    void Start()
    {
        ResourceCategory = "Leitura";
    }

    // Update is called once per frame
    void UpdateList()
    {
        simpleScroll.Clear();
        var resourceButtons = new List<GameObject>();
        foreach (var resource in Game.Resources.resources.FindAll(x=>x.category==_resourceCategory))
        {
            var resourceButton = Instantiate(resourceButtonPrefab);
            resourceButton.GetComponentInChildren<TextMeshProUGUI>().SetText(resource.name);
            resourceButtons.Add(resourceButton);
        }
        simpleScroll.AddList(resourceButtons);
    }
}
