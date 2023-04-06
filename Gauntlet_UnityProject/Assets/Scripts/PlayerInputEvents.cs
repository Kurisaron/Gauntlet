using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputEvents : MonoBehaviour
{
    private Vector2 moveDirection;

    private void Update()
    {
        transform.position += new Vector3(moveDirection.x, 0, moveDirection.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();

    }
}
