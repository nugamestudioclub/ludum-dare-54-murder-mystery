using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;

    private Vector2 moveDirection;

    private void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        if (PlayerStateManager.stateManager.matches(PlayerState.FreeRoam)) {
            Move();
        } else {
            rb.velocity = Vector2.zero;
        }
    }

    private void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

}
