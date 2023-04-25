using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputEvents : MonoBehaviour
{
    private Vector2 moveDirection;
    private float speed = 10f;
    public Player player;

    private bool classSelected = false;

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

        SelectClass(ClassEnum.Warrior);
    }

    public void SelectWizard(InputAction.CallbackContext context)
    {
        if (classSelected || !context.performed) return;

        SelectClass(ClassEnum.Wizard);
    }

    public void SelectValkyrie(InputAction.CallbackContext context)
    {
        if (classSelected || !context.performed) return;

        SelectClass(ClassEnum.Valkyrie);
    }

    public void SelectElf(InputAction.CallbackContext context)
    {
        if (classSelected || !context.performed) return;

        SelectClass(ClassEnum.Elf);
    }

    private void SelectClass(ClassEnum classEnum)
    {
        player.characterClass = Array.Find(GameManager.Instance.classes, characterClass => characterClass._class == classEnum);

        //GameObject classBody = Instantiate(player.characterClass.CharacterPrefab, transform);
        Color classColor;
        switch (classEnum)
        {
            case ClassEnum.Warrior:
                classColor = Color.red;
                break;
            case ClassEnum.Valkyrie:
                classColor = Color.cyan;
                break;
            case ClassEnum.Wizard:
                classColor = Color.blue;
                break;
            case ClassEnum.Elf:
                classColor = Color.green;
                break;
            default:
                classColor = Color.black;
                Debug.LogWarning("Somehow you broke the game, be rewarded with playing the necromancer.");
                break;
        }
        GetComponent<Renderer>().material.color = classColor;

        classSelected = true;
    }


    public void Attack(InputAction.CallbackContext context)
    {
        if (!classSelected || !context.performed) return;

        GetComponent<Player>().DoAttack();
    }
}
