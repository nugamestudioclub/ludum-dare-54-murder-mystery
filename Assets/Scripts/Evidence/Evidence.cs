using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Evidence", menuName = "Evidence/Create New Item")]
public class Evidence : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private string itemName;
    public Sprite itemSprite;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private string question;

    public string GetQuestion()
    {
        return question;
    }

    
}
 