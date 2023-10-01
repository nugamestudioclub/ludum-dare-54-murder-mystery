using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueBehavior : MonoBehaviour
{
    [SerializeField]
    private string filename;
    [SerializeField]
    private UnityEvent postDialogueAction;

    public void doDialogue() {
        if (!PlayerStateManager.stateManager.matches(PlayerState.Dialogue)) {
            DialogueSequence.dialogue.doSequence(filename);
            if (postDialogueAction != null) {
                postDialogueAction.Invoke();
            }
        }
    }

    public void setFilename(string name) {
        this.filename = name;
    }
}
