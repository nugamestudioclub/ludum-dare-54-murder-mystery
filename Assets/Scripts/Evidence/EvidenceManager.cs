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
    [SerializeField] private List<Evidence> secondaryEvidence = new List<Evidence>();
    [SerializeField] private List<Evidence> comboEvidence = new List<Evidence>();

    [SerializeField] private bool comboMode = false;

    public bool comboCheckOne = false;
    public bool comboCheckTwo = false;

    private Evidence currentEvidence;

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
            if (comboMode)
            {
                comboCheckOne = true;
            }

            if (PlayerStateManager.stateManager.matches(PlayerState.Inventory) && Input.GetKeyDown(inventoryKey))
            {
                isOpen = false;
                inventory.SetActive(false);

                PlayerStateManager.stateManager.set(PlayerState.FreeRoam);
                //Time.timeScale = 1;
            }
        }
    }

    // if click on item -> click on wrong -> display message -> reset flag

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

            if (itemEvidence.hasCombo && !itemEvidence.createdCombo)
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
     * 
     */
    private void MakeCombo(EvidenceComboBehavior combomaker)
    {
        switch(combomaker.evidence.GetItemName())
        {
            case "Knife":
                combomaker.InitializeSecondEvidence(secondaryEvidence[0]); //body
                combomaker.InitializeComboEvidence(comboEvidence[0]); //autospy report
                break;
            case "Body":
                combomaker.InitializeSecondEvidence(secondaryEvidence[1]); //knife
                combomaker.InitializeComboEvidence(comboEvidence[0]); //autopsy report
                break;

        }
    }

    public void changeComboOne()
    {
        comboCheckOne = !comboCheckOne;
    }

    public void changeComboTwo()
    {
        comboCheckTwo = !comboCheckTwo;
    }

    public void changeComboMode()
    {
        comboMode = !comboMode;
    }

    public bool GetComboMode()
    {
        return comboMode;
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
