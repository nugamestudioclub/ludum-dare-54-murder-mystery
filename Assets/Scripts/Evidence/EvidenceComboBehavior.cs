using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceComboBehavior : MonoBehaviour
{
    public Evidence evidence;
    public bool hasCombo;
    public bool createdCombo = false;
    [SerializeField] private Evidence secondEvidence;
    [SerializeField] private Evidence comboEvidence;

    public void DisplayText()
    {
        Debug.Log(evidence.GetQuestion());
    }

    public void Combo()
    {
        if (hasCombo)
        {
            EvidenceManager evidenceManage = EvidenceManager.evidenceManager;

            foreach (Evidence evidence in evidenceManage.GetEvidenceList())
            {
                if (secondEvidence.GetItemName() == evidence.GetItemName() && secondEvidence != null)
                {
                    evidenceManage.changeComboMode();
                    
                }
            }
        }
    }

    public void InitializeComboEvidence(Evidence evidence)
    {
        comboEvidence = evidence;
    }

    public void InitializeSecondEvidence(Evidence evidence)
    {
        secondEvidence = evidence;
    }
}
