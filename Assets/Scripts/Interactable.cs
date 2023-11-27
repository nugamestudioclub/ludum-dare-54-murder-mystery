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
        if (isInRange && PlayerStateManager.stateManager.matches(PlayerState.FreeRoam))
        {
            if (Input.GetKeyDown(interactKey)) {
                interactAction.Invoke();
                //textUI.SetActive(false);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            textUI.SetActive(true);
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            textUI.SetActive(false);
        }
    }

    private bool isInteractable() {
        return (repeatable || !interacted);
    }
}
