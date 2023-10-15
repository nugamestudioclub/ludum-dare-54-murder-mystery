using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ComboState {
    Dormant,
    Select,
    Combine
}

public class ComboManager : MonoBehaviour
{
    public static ComboManager manager;

    [SerializeField]
    private GameObject selectButton;

    private ComboState comboState;

    void Start()
    {
        manager = this;
        comboState = ComboState.Dormant;
    }

    public void selectButtonHit() {
        if (PlayerStateManager.stateManager.matches(PlayerState.Inventory)) {
            if (comboState == ComboState.Dormant) {
                selectButton.GetComponent<Image>().color = Color.red;
                comboState = ComboState.Select;
            } else if (comboState == ComboState.Select) {
                selectButton.GetComponent<Image>().color = Color.white;
                comboState = ComboState.Dormant;
                EvidenceManager.evidenceManager.deselectAllEvidence();
            } else {
                EvidenceManager.evidenceManager.combineSelectedEvidence();
                this.resetCombine();
            }
        }
    }

    public void setCombineReady() {
        selectButton.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().SetText("Combine");
        comboState = ComboState.Combine;
    }

    public void cancelCombineReady() {
        selectButton.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().SetText("Select");
        comboState = ComboState.Select;
    }

    public void resetCombine() {
        selectButton.GetComponent<Image>().color = Color.white;
        selectButton.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().SetText("Select");
        comboState = ComboState.Dormant;
    }

    public bool matches(ComboState state) {
        return this.comboState == state;
    }
}
