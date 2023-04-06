using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterClass", menuName = "Class/Character Class", order = 1)]
public class CharacterClass : ScriptableObject, ICharacter
{
    public ClassEnum _class;
    public GameObject characterPrefab;

    [SerializeField, Range(0,4)]
    private float meleePower;
    [SerializeField, Range(0, 4)]
    private float magic;
    [SerializeField, Range(0, 4)]
    private float health;
    [SerializeField, Range(0, 4)]
    private float armor;
    [SerializeField, Range(0, 4)]
    private float shotPower;
    [SerializeField, Range(0, 4)]
    private float shotSpeed;
    [SerializeField, Range(0, 4)]
    private float moveSpeed;

    public float MeleePower
    {
        get => meleePower;
        set
        {
            meleePower = value;
        }
    }

    public float Magic
    {
        get => magic;
        set
        {
            magic = value;
        }
    }

    public float Health
    {
        get => health;
        set
        {
            health = value;
        }
    }

    public float Armor
    {
        get => armor;
        set
        {
            armor = value;
        }
    }

    public float ShotPower
    {
        get => shotPower;
        set
        {
            shotPower = value;
        }
    }

    public float ShotSpeed
    {
        get => shotSpeed;
        set
        {
            shotSpeed = value;
        }
    }

    public float MoveSpeed
    {
        get => moveSpeed;
        set
        {
            moveSpeed = value;
        }
    }


    public void DoAttack()
    {
        switch (_class)
        {
            case ClassEnum.Warrior:
                WarriorAttack();
                break;
            case ClassEnum.Valkyrie:
                ValkyrieAttack();
                break;
            case ClassEnum.Wizard:
                WizardAttack();
                break;
            case ClassEnum.Elf:
                ElfAttack();
                break;
            default:
                Debug.LogError("Class enum was invalid. You should not be here.");
                _class = ClassEnum.Warrior;
                DoAttack();
                break;
        }
    }

    private void WarriorAttack()
    {
        Debug.Log("Warrior attack");
    }

    private void ValkyrieAttack()
    {
        Debug.Log("Valkyrie attack");
    }

    private void WizardAttack()
    {
        Debug.Log("Wizard attack");
    }

    private void ElfAttack()
    {
        Debug.Log("Elf attack");
    }

}
