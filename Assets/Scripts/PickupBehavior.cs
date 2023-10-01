using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehavior : MonoBehaviour
{
    public Evidence evidence;
    [SerializeField] private bool interact = true;

    public void PickUp()
    {
        if (interact && !EvidenceManager.isOpen)
        {
            interact = false;
            string question = evidence.GetQuestion();
            Debug.Log(question);

            EvidenceManager.evidenceManager.Add(evidence);
            GetComponent<EvidenceComboBehavior>().hasCombo = evidence.GetHasCombo();
            GetComponent<EvidenceComboBehavior>().Combo();
        }
    }
}
