using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // VARIABLES
    public CharacterClass characterClass;
    public List<Upgrade> upgrades;

    // PROPERTIES
    public float MeleePower
    {
        get
        {
            return characterClass.MeleePower + SumUpgradesMeleePower();
        }
    }

    public float Magic
    {
        get
        {
            return characterClass.Magic + SumUpgradesMagic();
        }
    }

    public float Health
    {
        get
        {
            return characterClass.Health;
        }
    }

    public float Armor
    {
        get
        {
            return characterClass.Armor + SumUpgradesArmor();
        }
    }

    public float ShotPower
    {
        get
        {
            return characterClass.ShotPower + SumUpgradesShotSpeed();
        }
    }

    public float ShotSpeed
    {
        get
        {
            return characterClass.ShotSpeed + SumUpgradesShotSpeed();
        }
    }

    public float MoveSpeed
    {
        get
        {
            return characterClass.MoveSpeed + SumUpgradesMoveSpeed();
        }
    }


    // SUM FUNCTIONS
    private float SumUpgradesMeleePower()
    {
        float sum = 0.0f;
        foreach(Upgrade upgrade in upgrades)
        {
            sum += upgrade.MeleePower;
        }
        return sum;
    }

    private float SumUpgradesMagic()
    {
        float sum = 0.0f;
        foreach (Upgrade upgrade in upgrades)
        {
            sum += upgrade.Magic;
        }
        return sum;
    }

    private float SumUpgradesArmor()
    {
        float sum = 0.0f;
        foreach (Upgrade upgrade in upgrades)
        {
            sum += upgrade.Armor;
        }
        return sum;
    }

    private float SumUpgradesShotPower()
    {
        float sum = 0.0f;
        foreach (Upgrade upgrade in upgrades)
        {
            sum += upgrade.ShotPower;
        }
        return sum;
    }

    private float SumUpgradesShotSpeed()
    {
        float sum = 0.0f;
        foreach (Upgrade upgrade in upgrades)
        {
            sum += upgrade.ShotSpeed;
        }
        return sum;
    }

    private float SumUpgradesMoveSpeed()
    {
        float sum = 0.0f;
        foreach (Upgrade upgrade in upgrades)
        {
            sum += upgrade.MoveSpeed;
        }
        return sum;
    }

}
