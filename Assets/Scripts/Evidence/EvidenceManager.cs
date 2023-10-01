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

            item.GetComponent<EvidenceComboBehavior>().evidence = evidence;

            var itemImage = item.transform.Find("Icon").GetComponent<Image>();
            if (evidence.itemSprite)
            {
                itemImage.sprite = evidence.itemSprite;
            }
        }
    }

    public List<Evidence> GetEvidenceList()
    {
        return evidenceCollected;
    }

    
}
