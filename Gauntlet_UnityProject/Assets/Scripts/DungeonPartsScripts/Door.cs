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
        if (collision.gameObject.GetComponent<Player>())
        {
            if (collision.gameObject.GetComponent<Player>().keysHeld > 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent.name.Contains("Shot")) other.gameObject.SetActive(false);
    }

    private void Unlock() => gameObject.SetActive(false);
}
