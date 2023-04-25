using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterClass", menuName = "Class/Character Class", order = 1)]
public class CharacterClass : ScriptableObject, ICharacter
{
    public ClassEnum _class;
    [SerializeField]
    private GameObject characterPrefab;
    [SerializeField]
    private GameObject shotPrefab;

    [SerializeField, Range(0,4)]
    private float meleePower;
    [SerializeField, Range(0, 4)]
    private float magic;
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

    public GameObject CharacterPrefab
    {
        get => characterPrefab;
    }

    public GameObject ProjectilePrefab
    {
        get => shotPrefab;
    }

}
