using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill", order = 51)]
public class Skill : ScriptableObject
{
    [SerializeField]
    private string skillName;
    [SerializeField]
    private int cost;
    
}
