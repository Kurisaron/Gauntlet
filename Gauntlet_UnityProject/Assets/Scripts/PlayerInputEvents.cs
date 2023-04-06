using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputEvents : MonoBehaviour
{
    private Vector2 moveDirection;
    private float speed = 10f;
    public Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        transform.position += new Vector3(moveDirection.x, 0, moveDirection.y) * speed * player.MoveSpeed * Time.deltaTime;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();

    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        GetComponent<Player>().characterClass.DoAttack();
    }
}
