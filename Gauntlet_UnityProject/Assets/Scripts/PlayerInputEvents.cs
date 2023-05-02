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

    public bool ClassSelected
    {
        get { return classSelected; }
    }

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
        gameObject.name = "Warrior";
    }

    public void SelectWizard(InputAction.CallbackContext context)
    {
        if (classSelected || !context.performed) return;

        SelectClass(ClassEnum.Wizard);
        gameObject.name = "Wizard";
    }

    public void SelectValkyrie(InputAction.CallbackContext context)
    {
        if (classSelected || !context.performed) return;

        SelectClass(ClassEnum.Valkyrie);
        gameObject.name = "Valkyrie";
    }

    public void SelectElf(InputAction.CallbackContext context)
    {
        if (classSelected || !context.performed) return;

        SelectClass(ClassEnum.Elf);
        gameObject.name = "Elf";
    }

    private void SelectClass(ClassEnum classEnum)
    {
        player.characterClass = Array.Find(GameManager.Instance.classes, characterClass => characterClass._class == classEnum);

        //GameObject classBody = Instantiate(player.characterClass.CharacterPrefab, transform);
        GetComponent<Renderer>().material.color = classEnum switch
        {
            ClassEnum.Warrior => Color.red,
            ClassEnum.Valkyrie => Color.cyan,
            ClassEnum.Wizard => Color.blue,
            ClassEnum.Elf => Color.green,
            _ => Color.black
        };

        if (GetComponent<Renderer>().material.color == Color.black) Debug.Log("Your class choice was somehow invalid. For your intrepid behaviour, enjoy playing as the necromancer.");

        //Debug.Log(Array.FindIndex(GameManager.Instance.players, player => player == gameObject.GetComponent<Player>()).ToString());
        GameUIManager.Instance.SetClass(Array.FindIndex(GameManager.Instance.players, player => player == gameObject.GetComponent<Player>()), classEnum);

        classSelected = true;
    }


    public void Attack(InputAction.CallbackContext context)
    {
        if (!classSelected || !context.performed) return;

        GetComponent<Player>().DoAttack();
    }
}
