using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceButtonController : MonoBehaviour
{
    [SerializeField]
    private DialogueBehavior dialogue;

    private Evidence evidence;

    public void setEvidence(Evidence evidence) {
        this.evidence = evidence;
        Debug.Log(evidence.getExamineFilename());
        this.dialogue.setFilename(evidence.getExamineFilename());
    }

    public void buttonHit() {
        if (PlayerStateManager.stateManager.matches(PlayerState.Inventory)) {
            if (ComboManager.manager.matches(ComboState.Dormant)) {
                dialogue.doDialogue();
            } else {
                EvidenceManager.evidenceManager.selectEvidence(evidence.itemName);
                if (EvidenceManager.evidenceManager.twoItemsSelected()) {
                    ComboManager.manager.setCombineReady();
                } else {
                    ComboManager.manager.cancelCombineReady();
                }
            }
        }
    }

    public bool hasName(string name) {
        return evidence.itemName == name;
    }
}
