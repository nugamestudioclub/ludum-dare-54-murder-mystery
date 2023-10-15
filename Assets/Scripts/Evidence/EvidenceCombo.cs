using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EvidenceCombo
{
    public string item1;
    public string item2;
    public string comboDesc;
    public Evidence resultingEvidence;
    public bool expended = false;

    public bool hasComponentItems(string item1, string item2) {
        return (item1.Equals(this.item1) && item2.Equals(this.item2))
            || (item1.Equals(this.item2) && item2.Equals(this.item1));
    }
}
