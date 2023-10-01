using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Evidence", menuName = "Evidence/Create New Item")]
public class Evidence : ScriptableObject
{
    [SerializeField] private string itemName;
    public Sprite itemSprite;
    [SerializeField] private string itemExamineFilename;

    public string getExamineFilename() {
        return itemExamineFilename;
    }
}
 