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

    // need to make sure these are initialized in the editor or this will not work
    // also need to keep track of items position in the array
    [SerializeField] private List<Evidence> secondaryEvidence = new List<Evidence>();
    [SerializeField] private List<Evidence> comboEvidence = new List<Evidence>();

    private void Awake()
    {
        evidenceManager = this;
        inventory.SetActive(false);
        isOpen = false;
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
                inventory.SetActive(false);

                PlayerStateManager.stateManager.set(PlayerState.FreeRoam);
                //Time.timeScale = 1;
            }
        }
    }

    public void Add(Evidence evidence)
    {
        evidenceCollected.Add(evidence);
    }

    public void DisplayEvidence()
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

            EvidenceComboBehavior itemEvidence = item.GetComponent<EvidenceComboBehavior>();

            itemEvidence.evidence = evidence;
            itemEvidence.hasCombo = itemEvidence.evidence.GetHasCombo();

            if (itemEvidence.hasCombo)
            {
                itemEvidence.createdCombo = true;
                MakeCombo(itemEvidence);
            }


            var itemImage = item.transform.Find("Icon").GetComponent<Image>();
            if (evidence.itemSprite)
            {
                itemImage.sprite = evidence.itemSprite;
            }
        }
    }

    /* 
     * This is to link the items together to know what they are connected to
     */
    private void MakeCombo(EvidenceComboBehavior comboMaker)
    {
        switch(comboMaker.evidence.GetItemName())
        {
            case "Knife":
                comboMaker.InitializeSecondEvidence(secondaryEvidence[0]); //body
                comboMaker.InitializeComboEvidence(comboEvidence[0]); //autospy report
                break;
            case "Body":
                comboMaker.InitializeSecondEvidence(secondaryEvidence[1]); //knife
                comboMaker.InitializeComboEvidence(comboEvidence[0]); //autopsy report
                break;
            default:
                break;
        }
    }

    public List<Evidence> GetEvidenceList()
    {
        return evidenceCollected;
    }

    public GameObject GetPlaceHolder()
    {
        return evidencePlaceHolder;
    }

    public Transform GetListUI()
    {
        return inventoryContent;
    }
}
