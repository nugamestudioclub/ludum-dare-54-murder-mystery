using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehavior : MonoBehaviour
{
    [SerializeField] private Evidence evidence;
    [SerializeField] private bool interact = true;

    public void pickUp()
    {
        if (interact && !EvidenceManager.isOpen)
        {
            interact = false;
            string question = evidence.GetQuestion();
            Debug.Log(question);

            EvidenceManager.evidenceManager.Add(evidence);
        }
    }
}
