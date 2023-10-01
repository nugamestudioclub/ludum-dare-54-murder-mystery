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

            List<Evidence> collectedEvidence = new List<Evidence>();

            foreach (Evidence ev in collectedEvidence)
            {
                collectedEvidence.Add(ev);
            }

            foreach (Evidence ev in collectedEvidence)
            {
                if (secondEvidence.GetItemName() == ev.GetItemName() && secondEvidence != null)
                {
                    evidenceManage.Add(comboEvidence);
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
