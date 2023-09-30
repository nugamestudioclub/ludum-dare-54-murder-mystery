using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EvidenceManager : MonoBehaviour
{
    public static EvidenceManager evidenceManager;

    [SerializeField] private GameObject inventory;
    [SerializeField] private bool isOpen;
    [SerializeField] private KeyCode inventoryKey;
    [SerializeField] private Transform inventoryContent;
    [SerializeField] private GameObject evidencePlaceHolder;

    [SerializeField] private List<Evidence> evidenceCollected = new List<Evidence>();

    private void Awake()
    {
        evidenceManager = this;
        inventory.SetActive(false);
    }

    private void Update()
    {
        if (!isOpen)
        {
            if (Input.GetKeyDown(inventoryKey))
            {
                isOpen = true;
                DisplayEvidence();
                inventory.SetActive(true);
            }
        }
        else
        {
            if (Input.GetKeyDown(inventoryKey))
            {
                isOpen = false;
                inventory.SetActive(false);
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

            var itemImage = item.transform.Find("Icon").GetComponent<Image>();
            if (evidence.itemSprite)
            {
                itemImage.sprite = evidence.itemSprite;
            }
        }
    }
}
