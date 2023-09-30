using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueSequence : MonoBehaviour
{
    public static DialogueSequence dialogue;

    [SerializeField]
    private GameObject dialoguePanel;
    [SerializeField]
    private TMP_Text dialogueText;
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private Image dialogueImage;
    [SerializeField]
    private KeyCode advanceDialogueKey;

    private string[] textLines;
    private int textIndex;

    private bool activationFrame = false;

    private void Awake() {
        dialogue = this;
        dialoguePanel.SetActive(false);
        textIndex = 0;
    }

    private void Update() {
        if (!activationFrame) {
            if (dialoguePanel.activeSelf) {
                if (Input.GetKeyDown(advanceDialogueKey)) {
                    if (textIndex < textLines.Length - 1) {
                        textIndex += 1;
                        updateDialogue();
                    } else {
                        endDialogue();
                    }
                }
            }
        } else {
            activationFrame = false;
        }
    }

    // assumes all dialogue is in the form "CharacterName: Line of dialogue."
    private void updateDialogue() {
        int separatorIndex = textLines[textIndex].IndexOf(":");
        nameText.SetText(textLines[textIndex].Substring(0, separatorIndex));
        dialogueText.SetText(textLines[textIndex].Substring(separatorIndex + 2));
    }

    private void endDialogue() {
        dialoguePanel.SetActive(false);
        PlayerStateManager.stateManager.set(PlayerState.FreeRoam);
    }

    public void doSequence(string filename) {
        PlayerStateManager.stateManager.set(PlayerState.Dialogue);
        dialoguePanel.SetActive(true);
        dialogueText.SetText("");
        TextAsset textAsset = (TextAsset) Resources.Load(filename);
        string textContent = textAsset.text;
        textLines = textContent.Split('\n');
        textIndex = 0;
        updateDialogue();
        activationFrame = true;
    }
}
