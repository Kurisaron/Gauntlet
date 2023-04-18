using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnEnable()
    {        
        GameEventBus.Subscribe(GameEvent.DoorOpened, Unlock);
    }
    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameEvent.DoorOpened, Unlock);
    }

    private void OnCollisionEnter(Collision collision) //remember to add in a thing for if a player has a key
    {
        if (collision.gameObject.GetComponent<Player>()) this.gameObject.SetActive(false);
    }

    private void Unlock() => this.gameObject.SetActive(false);
}
