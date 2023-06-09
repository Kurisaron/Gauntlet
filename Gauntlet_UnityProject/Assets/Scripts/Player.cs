using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // VARIABLES
    public CharacterClass characterClass;

    public List<Upgrade> upgrades = new List<Upgrade>();
    public int keysHeld;
    public int potionsHeld;

    [Header("Health")]
    public int currentHealth;
    public int startingHealth = 600;

    public int score;

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
        //TestInventory();
        
        currentHealth = startingHealth;
        StartCoroutine(HealthDecay());
    }

    // ATTACKS
    public void DoAttack()
    {
        StartCoroutine(TestAttackRoutine(Instantiate(characterClass.ProjectilePrefab, transform)));
    }

    private IEnumerator TestAttackRoutine(GameObject shot)
    {
        shot.transform.parent = null;
        shot.AddComponent<PlayerShot>().shooter = this;
        float timeAlive = 0.0f;

        while (timeAlive < 3.0f)
        {
            shot.transform.position += shot.transform.forward * Time.deltaTime * 10.0f;
            shot.SetActive(true);
            timeAlive += Time.deltaTime;
            yield return null;
        }

        Destroy(shot);
    }

    public void UsePotion()
    {
        if (potionsHeld > 0) PotionAttackManager.Instance.UsePotion(this);
        else Debug.Log("No more potions for Player " + Array.FindIndex(GameManager.Instance.players, player => player == this).ToString());
    }

    // INVENTORY

    public void AddUpgrade(Upgrade upgrade)
    {
        upgrades.Add(upgrade);
        GameUIManager.Instance.AddUpgrade(Array.FindIndex(GameManager.Instance.players, player => player == this), upgrade);
    }

    public void AddItem(string potionORkey)
    {
        if (InventoryFull()) return;

        if (potionORkey == "potion") potionsHeld += 1;
        else keysHeld += 1;

        UpdateInventory();
    }

    private bool InventoryFull()
    {
        return (keysHeld + potionsHeld) >= 12;
    }

    private void UpdateInventory() => GameUIManager.Instance.UpdateInventory(Array.FindIndex(GameManager.Instance.players, player => player == this));

    private void TestInventory()
    {
        for (int i = 0; i < 8; i++)
        {
            if (UnityEngine.Random.Range(0, 2) == 0) AddItem("potion");
            else AddItem("key");
        }
    }

    // HEALTH
    private IEnumerator HealthDecay()
    {
        while(gameObject.activeInHierarchy)
        {
            currentHealth -= 1;

            if (currentHealth <= 0) PlayerDead();

            yield return new WaitForSeconds(1);
        }
    }

    private void PlayerDead()
    {
        int index = Array.FindIndex(GameManager.Instance.players, player => player == this);
        GameUIManager.Instance.DisableContainer(index);
        GameManager.Instance.players[index] = null;
        Destroy(gameObject);
    }

    // SUM FUNCTIONS
    private float SumUpgradesMeleePower()
    {
        float sum = 0.0f;

        if (upgrades == null) return sum;

        foreach(Upgrade upgrade in upgrades)
        {
            sum += upgrade.MeleePower;
        }
        return sum;
    }

    private float SumUpgradesMagic()
    {
        float sum = 0.0f;

        if (upgrades == null) return sum;


        foreach (Upgrade upgrade in upgrades)
        {
            sum += upgrade.Magic;
        }
        return sum;
    }

    private float SumUpgradesArmor()
    {
        float sum = 0.0f;

        if (upgrades == null) return sum;


        foreach (Upgrade upgrade in upgrades)
        {
            sum += upgrade.Armor;
        }
        return sum;
    }

    private float SumUpgradesShotPower()
    {
        float sum = 0.0f;

        if (upgrades == null) return sum;


        foreach (Upgrade upgrade in upgrades)
        {
            sum += upgrade.ShotPower;
        }
        return sum;
    }

    private float SumUpgradesShotSpeed()
    {
        float sum = 0.0f;

        if (upgrades == null) return sum;


        foreach (Upgrade upgrade in upgrades)
        {
            sum += upgrade.ShotSpeed;
        }
        return sum;
    }

    private float SumUpgradesMoveSpeed()
    {
        float sum = 0.0f;

        if (upgrades == null) return sum;


        foreach (Upgrade upgrade in upgrades)
        {
            sum += upgrade.MoveSpeed;
        }
        return sum;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent.name.Contains("Shot")) other.transform.parent.gameObject.SetActive(false);
    }

}
