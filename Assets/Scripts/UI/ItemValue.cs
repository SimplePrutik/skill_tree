using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemValue : MonoBehaviour
{
    public string itemName;
    public Text value;

    void SetValue(string _itemName, string textValue)
    {
        if (itemName != _itemName) return;
        value.text = textValue;
    }

    private void OnEnable()
    {
        Player.OnTextBoxChanged += SetValue;
    }
    
    private void OnDisable()
    {
        Player.OnTextBoxChanged -= SetValue;
    }
}
