using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorManager : MonoBehaviour
{
    public float startingTime = 180; //in seconds
    public float timeCheck; //in seconds

    private void OnEnable()
    {
        timeCheck = startingTime;
        StartCoroutine(Countdown());
    }

    public IEnumerator Countdown()
    {
             
        while (true)
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

            /*if(timeCheck <= 0)
            {
                GameEventBus.Publish(GameEvent.DoorOpened);
                GameEventBus.Unsubscribe(GameEvent.DoorOpened, );
            }*/
        }
    }
}
