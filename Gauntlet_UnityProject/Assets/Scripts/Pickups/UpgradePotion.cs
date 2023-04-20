using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePotion : Potion
{
    private void OnEnable()
    {
        GameEventBus.Subscribe(GameEvent.ShotPotion, Explode);
    }
    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameEvent.ShotPotion, Explode);
    }

    public override void OnPickUp(Player player)
    {
        /* TO-DO:
         * upgrade the stat that this potion has
         * if the player who picked it up already has that specific upgrade, add potion to player inventory 
         * update the UI for the player who picked it up to show that change
         *
         */
    }


}
