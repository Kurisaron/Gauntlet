using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    MeleePower = 0,
    Magic = 1,
    Armor = 2,
    ShotPower = 3,
    ShotSpeed = 4,
    MoveSpeed = 5
}

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Potion/Upgrade", order = 1)]
public class Upgrade : ScriptableObject, ICharacter
{
    public UpgradeType upgradeType;
    [SerializeField, Range(0,2)]
    private float meleePower;
    [SerializeField, Range(0, 2)]
    private float magic;
    [SerializeField, Range(0, 2)]
    private float armor;
    [SerializeField, Range(0, 2)]
    private float shotPower;
    [SerializeField, Range(0,2)]
    private float shotSpeed;
    [SerializeField, Range(0, 2)]
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

}
