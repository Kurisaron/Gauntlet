using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitObstacle : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Player>())
        {           
            GameEventBus.Publish(GameEvent.LevelFinished); //the level changing code will be in the level manager, right?
        }
    }
}
