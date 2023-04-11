using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // VARIABLES
    public CharacterClass characterClass;
    public List<Upgrade> upgrades;

    [Header("Health")]
    public int currentHealth;
    public int startingHealth = 600;

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

    // UNITY FUNCTIONS
    private void OnEnable()
    {
        currentHealth = startingHealth;
        StartCoroutine(HealthDecay());
    }

    // ATTACKS
    public void DoAttack() => characterClass.DoAttack();

    public void UsePotion()
    {

    }

    // HEALTH DECAY
    private IEnumerator HealthDecay()
    {
        while(gameObject.activeInHierarchy)
        {
            currentHealth -= 1;
            yield return new WaitForSeconds(1);
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
