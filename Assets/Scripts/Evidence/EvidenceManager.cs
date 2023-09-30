using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceManager : MonoBehaviour
{
    public static EvidenceManager evidenceManager;
    
    [SerializeField] private List<Evidence> evidenceCollected = new List<Evidence>();

    private void Awake()
    {
        evidenceManager = this;
    }

    public void Add(Evidence evidence)
    {
        evidenceCollected.Add(evidence);
    }
}
