using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceComboBehavior : MonoBehaviour
{
    public Evidence evidence;
    [SerializeField] private bool hasCombo;
    [SerializeField] private Evidence secondEvidence;
    [SerializeField] private Evidence comboEvidence;

    public void DisplayText()
    {
        Debug.Log(evidence.GetQuestion());
    }

    public void Combo()
    {
        foreach(Evidence evidence in EvidenceManager.evidenceManager.GetEvidenceList())
        {
            if (secondEvidence.GetItemName() == evidence.GetItemName())
            {
                EvidenceManager.evidenceManager.Add(comboEvidence);

                //We can add a display a new text here.
            }
        }
    }
}
