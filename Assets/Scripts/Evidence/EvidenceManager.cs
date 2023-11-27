using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EvidenceManager : MonoBehaviour
{
    public static EvidenceManager evidenceManager;
    public static bool isOpen;

    [SerializeField] private GameObject inventory;
    [SerializeField] private KeyCode inventoryKey;
    [SerializeField] private Transform inventoryContent;
    [SerializeField] private GameObject evidencePlaceHolder;

    [SerializeField] private List<Evidence> evidenceCollected = new List<Evidence>();

    //private Evidence currentEvidence;
    private List<string> itemNamesSelected;
    [SerializeField] private ComboSet comboSet;
    [SerializeField] private string incorrectComboFilename;
    [SerializeField] private string expendedComboFilename;

    private void Awake()
    {
        evidenceManager = this;
        inventory.SetActive(false);
        isOpen = false;
        itemNamesSelected = new List<string>();
    }

    private void Update()
    {
        if (!isOpen)
        {
            if (PlayerStateManager.stateManager.matches(PlayerState.FreeRoam) && Input.GetKeyDown(inventoryKey))
            {
                isOpen = true;
                DisplayEvidence();
                inventory.SetActive(true);

                PlayerStateManager.stateManager.set(PlayerState.Inventory);
                //Time.timeScale = 0;
            }
        }
        else
        {
            if (PlayerStateManager.stateManager.matches(PlayerState.Inventory) && Input.GetKeyDown(inventoryKey))
            {
                isOpen = false;

                ComboManager.manager.resetCombine();
                inventory.SetActive(false);

                itemNamesSelected = new List<string>();

                PlayerStateManager.stateManager.set(PlayerState.FreeRoam);

                //Time.timeScale = 1;
            }
        }
    }

    public void Add(Evidence evidence)
    {
        evidenceCollected.Add(evidence);
    }

    private void DisplayEvidence()
    {
        foreach(Transform item in inventoryContent)
        {
            Destroy(item.gameObject);
        }

        bool selectFirst = false;

        foreach (Evidence evidence in evidenceCollected)
        {
            GameObject item = Instantiate(evidencePlaceHolder, inventoryContent);

            if (!selectFirst)
            {
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(item);
                selectFirst = true;
            }

            //item.GetComponent<PickupBehavior>().evidence = evidence;

            //item.GetComponent<DialogueBehavior>().setFilename(evidence.getExamineFilename());
            item.GetComponent<EvidenceButtonController>().setEvidence(evidence);

            var itemImage = item.transform.Find("Icon").GetComponent<Image>();
            if (evidence.itemSprite)
            {
                itemImage.sprite = evidence.itemSprite;
            }
        }
    }

    public void selectEvidence(string targetName) {
        if (itemNamesSelected.Contains(targetName)) {
            this.deselectIcon(targetName);
        } else if (itemNamesSelected.Count > 1) {
            this.deselectIcon(itemNamesSelected[0]);
            this.selectIcon(targetName);
        } else {
            this.selectIcon(targetName);
        }
    }

    public void deselectAllEvidence() {
        string[] currentNames = new string[2];
        itemNamesSelected.CopyTo(currentNames);
        foreach (string name in currentNames) {
            this.deselectIcon(name);
        }
    }

    private void deselectIcon(string targetName) {
        foreach (Transform evidenceObject in inventoryContent) {
            if (evidenceObject.gameObject.GetComponent<EvidenceButtonController>().hasName(targetName)) {
                evidenceObject.gameObject.GetComponent<Image>().color = Color.white;
                itemNamesSelected.Remove(targetName);
                break;
            }
        }
    }

    private void selectIcon(string targetName) {
        foreach (Transform evidenceObject in inventoryContent) {
            if (evidenceObject.gameObject.GetComponent<EvidenceButtonController>().hasName(targetName)) {
                evidenceObject.gameObject.GetComponent<Image>().color = Color.blue;
                itemNamesSelected.Add(targetName);
                break;
            }
        }
    }

    public bool twoItemsSelected() {
        return itemNamesSelected.Count == 2;
    }

    public void combineSelectedEvidence() {

        if (comboSet.hasCombo(itemNamesSelected[0], itemNamesSelected[1])) {
            if (!comboSet.expendedCombo(itemNamesSelected[0], itemNamesSelected[1])) {
                DialogueSequence.dialogue.doSequence(comboSet.getComboDesc(itemNamesSelected[0], itemNamesSelected[1]));
                comboSet.expendCombo(itemNamesSelected[0], itemNamesSelected[1]);
                this.Add(comboSet.getComboEvidence(itemNamesSelected[0], itemNamesSelected[1]));
                this.DisplayEvidence();
            } else {
                DialogueSequence.dialogue.doSequence(expendedComboFilename);
            }
        } else {
            DialogueSequence.dialogue.doSequence(incorrectComboFilename);
        }

        this.deselectAllEvidence();
    }

    private void GetQuestion()
    {
       // Debug.Log(currentEvidence.GetQuestion());
    }

    public bool hasEvidenceNamed(string targetName) {
        foreach (Evidence evidence in evidenceCollected)
        {
            if (evidence.itemName == targetName) {
                return true;
            }
        }
        return false;
    }
}
