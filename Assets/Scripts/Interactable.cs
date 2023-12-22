using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Interactable : MonoBehaviour
{
    [SerializeField] private bool isInRange;
    [SerializeField] private KeyCode interactKey;
    [SerializeField] private UnityEvent interactAction;
    [SerializeField] private GameObject textUI;

    [SerializeField] private bool repeatable;

    private bool interacted;

    private void Start()
    {
        textUI.SetActive(false);
        interacted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && PlayerStateManager.stateManager.matches(PlayerState.FreeRoam) && this.isInteractable())
        {
            if (Input.GetKeyDown(interactKey)) {
                interactAction.Invoke();
                textUI.SetActive(false);
                interacted = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isInteractable()) {
                textUI.SetActive(true);
            }
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            if (textUI.activeSelf) {
                textUI.SetActive(false);
            }
        }
    }

    private bool isInteractable() {
        return (repeatable || !interacted);
    }

    public void resetInteraction() {
        interacted = false;
    }
}
