using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RouterBehavior : MonoBehaviour
{
    [SerializeField]
    private UnityEvent defaultAction;

    [SerializeField]
    private List<string> evidenceRequired;
    
    [SerializeField]
    private List<UnityEvent> resultAction;

    [SerializeField]
    private bool repeatable;

    [SerializeField]
    private string nonRepeatFilename;

    private bool interacted;

    void Start() {
        if (evidenceRequired.Count != resultAction.Count) {
            Debug.LogError("Evidence conditions and resulting actions are not 1 to 1.", this);
        }
        interacted = false;
    }

    public void doRoute() {
        for (int i = 0; i < evidenceRequired.Count; i++) {
            if (EvidenceManager.evidenceManager.hasEvidenceNamed(evidenceRequired[i])) {
                resultAction[i].Invoke();
                interacted = true;
                return;
            }
        }

        defaultAction.Invoke();
        interacted = true;
    }

    private bool isInteractable() {
        return (repeatable || !interacted);
    }
}
