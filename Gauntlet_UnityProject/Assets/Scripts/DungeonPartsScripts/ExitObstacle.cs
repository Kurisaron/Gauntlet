using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitObstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Player>())
        {
            this.gameObject.SetActive(false);
            //TO-DO:
            //add in the level change code here (we could probably use the level finished event bus enum here)
        }
    }
}
