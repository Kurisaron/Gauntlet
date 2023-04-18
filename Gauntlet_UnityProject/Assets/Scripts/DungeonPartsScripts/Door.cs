using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnEnable()
    {
        //GameEventBus.Subscribe(GameEvent.DoorOpened, );
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>()) this.gameObject.SetActive(false);
    }

    private void Unlock() => this.gameObject.SetActive(false);
}
