using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ComboSet", menuName = "Evidence/Create New ComboSet")]
public class ComboSet : ScriptableObject
{
    [SerializeField] public List<EvidenceCombo> allCombos;

    public bool expendedCombo(string item1, string item2) {
        foreach (EvidenceCombo combo in allCombos) {
            if (combo.hasComponentItems(item1, item2)) {
                return combo.expended;
            }
        }
        return true;
    }

    public void expendCombo(string item1, string item2) {
        foreach (EvidenceCombo combo in allCombos) {
            if (combo.hasComponentItems(item1, item2)) {
                combo.expended = true;
            }
        }
    }
    
    public bool hasCombo(string item1, string item2) {
        foreach (EvidenceCombo combo in allCombos) {
            if (combo.hasComponentItems(item1, item2)) {
                return true;
            }
        }
        return false;
    }

    public string getComboDesc(string item1, string item2) {
        foreach (EvidenceCombo combo in allCombos) {
            if (combo.hasComponentItems(item1, item2)) {
                return combo.comboDesc;
            }
        }
        return null;
    }

    public Evidence getComboEvidence(string item1, string item2) {
        foreach (EvidenceCombo combo in allCombos) {
            if (combo.hasComponentItems(item1, item2)) {
                return combo.resultingEvidence;
            }
        }
        return null;
    }

    public void replenishAllCombos() {
        foreach (EvidenceCombo combo in allCombos) {
            combo.expended = false;
        }
    }
}
 