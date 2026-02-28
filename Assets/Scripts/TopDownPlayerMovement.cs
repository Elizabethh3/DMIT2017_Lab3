using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownPlayerMovement : MonoBehaviour
{
    public InputAction moveInput;
    private Vector2 movementDirection = Vector2.zero;
    public float moveSpeed;
    public event Action<Vector2> OnMove;
    private void Awake()
    {
        moveInput.Enable();
        moveInput.performed += GetMoveVector;
        moveInput.canceled += GetMoveVector;

    }

    public void GetMoveVector(InputAction.CallbackContext c)
    {
        movementDirection = c.ReadValue<Vector2>();
        OnMove?.Invoke(movementDirection);
        
    }

    private void Update()
    {
        transform.position += new Vector3(movementDirection.x, movementDirection.y, 0) * moveSpeed * Time.deltaTime;
    }
}
