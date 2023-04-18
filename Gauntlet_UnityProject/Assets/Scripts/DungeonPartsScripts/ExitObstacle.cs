using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitObstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Player>())
        {
            this.gameObject.SetActive(false); //this line is just here to make sure the code is running

            GameEventBus.Publish(GameEvent.LevelFinished); //the level changing code will be in the level manager, right?
        }
    }
}
