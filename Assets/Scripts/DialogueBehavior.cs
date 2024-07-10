using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueBehavior : MonoBehaviour
{
    [SerializeField]
    private string filename;
    [SerializeField]
    private UnityEvent onDialogueAction;
    [SerializeField]
    private UnityEvent postDialogueAction;

    public static void doGivenDialogue(string givenName) {
        if (!PlayerStateManager.stateManager.matches(PlayerState.Dialogue)) {
            DialogueSequence.dialogue.doSequence(givenName);
        }
    }

    public void doDialogue() {
        if (!PlayerStateManager.stateManager.matches(PlayerState.Dialogue)) {

            if (postDialogueAction == null)
            {
                DialogueSequence.dialogue.doSequence(filename);
            }
            else
            {
                DialogueSequence.dialogue.doSequence(filename, postDialogueAction);
            }

            if (onDialogueAction != null) {
                Debug.Log("post dialogue action done");
                onDialogueAction.Invoke();
            }
        }
    }

    public void setFilename(string name) {
        this.filename = name;
    }
}
