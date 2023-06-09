using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePotion : Potion
{
    [SerializeField]
    private Upgrade upgrade;
    
    private void OnEnable()
    {
        GetRandomUpgrade();
        GameEventBus.Subscribe(GameEvent.ShotPotion, Explode);
    }
    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameEvent.ShotPotion, Explode);
    }

    private void GetRandomUpgrade()
    {
        if (GameManager.Instance == null) Debug.LogError("GAMEMANAGER NOT ACTIVE");
        
        upgrade = Instantiate(GameManager.Instance.upgrades[Random.Range(0, GameManager.Instance.upgrades.Length)]);
        if (upgrade != null)
        {
            if (upgrade.name.Contains("melee")) GetComponent<Renderer>().material.color = Color.red;
            if (upgrade.name.Contains("magic")) GetComponent<Renderer>().material.color = Color.cyan;
            if (upgrade.name.Contains("armor")) GetComponent<Renderer>().material.color = Color.blue;
            if (upgrade.name.Contains("shotPower")) GetComponent<Renderer>().material.color = Color.white;
            if (upgrade.name.Contains("shotSpeed")) GetComponent<Renderer>().material.color = Color.black;
            if (upgrade.name.Contains("move")) GetComponent<Renderer>().material.color = Color.green;
        }

        Debug.Log(upgrade);
    }

    public override void OnPickUp(Player player)
    {
        base.OnPickUp(player);
        
        player.AddUpgrade(upgrade);
        GameEventBus.Publish(GameEvent.Upgraded);
    }


}
