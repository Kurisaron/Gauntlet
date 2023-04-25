using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputEvents : MonoBehaviour
{
    private Vector2 moveDirection;
    private float speed = 10f;
    public Player player;

    [SerializeField] private bool classSelected = false;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        transform.position += new Vector3(moveDirection.x, 0, moveDirection.y) * speed * player.MoveSpeed * Time.deltaTime;
        transform.LookAt(transform.position + new Vector3(moveDirection.x, 0, moveDirection.y), Vector3.up);
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!classSelected) return;
        
        moveDirection = context.ReadValue<Vector2>();

    }

    public void SelectWarrior(InputAction.CallbackContext context)
    {
        if (classSelected || !context.performed) return;

    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        GetComponent<Player>().DoAttack();
    }
}
