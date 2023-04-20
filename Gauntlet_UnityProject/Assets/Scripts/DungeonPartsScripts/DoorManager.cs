using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorManager : MonoBehaviour
{
    public float startingTime = 18; //in seconds
    public float timeCheck; //in seconds

    private void OnEnable()
    {
        timeCheck = startingTime;

        /*TO-DO:
         * find a way to check if the player is in combat
         * find a way to check if player has keys in inventory
         * if there are keys in the inventory, set startingTime to 36 seconds
         */

        StartCoroutine(Countdown());
    }

    public IEnumerator Countdown()
    {
        while (timeCheck >= 0)
        {
            Debug.Log("Time:" + timeCheck);
            if (Input.anyKey) //if input is detected, timeCheck = startingTime (resetting the Countdown)
            {
                timeCheck = startingTime;
            }
            else
            {
                timeCheck--;
            }
            yield return new WaitForSeconds(1);
        }
        //this.gameObject.SetActive(false);
        GameEventBus.Publish(GameEvent.DoorOpened);
    }
}
