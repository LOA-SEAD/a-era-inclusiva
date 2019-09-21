using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuRM : MonoBehaviour
{
    private List<string> _types;
    public GameObject buttonPrefab;
    private bool _loaded = false;
    public GameObject ButtonsParent;
    public RMController controller;
    
    // Start is called before the first frame update

    public void SetTypes()
    {
        foreach (var recurso in GameManager.GameData.RecursosRM)
        {
            if (!_types.Contains(recurso.Tipo))
                _types.Add(recurso.Tipo);
        }

        _loaded = true;
    }

    private void Awake()
    {
        _types = new List<string>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!_loaded && GameManager.GameData.Loaded)
        {
            SetTypes();
            SetButtons();
        }
    }

    private void SetButtons()
    {
        foreach (var type in _types)
        {
            var button = Instantiate(buttonPrefab, ButtonsParent.transform).GetComponent<Button>();
            button.transform.SetAsFirstSibling();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => controller.OnSelectType(type));
            button.GetComponentInChildren<TextMeshProUGUI>().text = type;
        }
    }
}