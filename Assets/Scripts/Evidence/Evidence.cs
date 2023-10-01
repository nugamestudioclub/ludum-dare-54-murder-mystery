using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Evidence", menuName = "Evidence/Create New Item")]
public class Evidence : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private string itemName;
    public Sprite itemSprite;
    [SerializeField] private string question;

    [SerializeField] private bool hasCombo;
    public bool createdCombo;
    [SerializeField] private Evidence secondEvidence;
    [SerializeField] private Evidence comboEvidence;

    public Evidence GetComboEvidence()
    {
        return comboEvidence;
    }

    public string GetQuestion()
    {
        return question;
    }

    public string GetItemName()
    {
        return itemName;
    }

    public bool GetHasCombo()
    {
        return hasCombo;
    }
    
}
 