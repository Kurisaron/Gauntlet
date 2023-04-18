using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private float startingTime = 10; // in seconds
    [SerializeField] private float timeCheck;
    [SerializeField] private GameObject exitObstacle;

    private void OnEnable()
    {
        timeCheck = startingTime;
        StartCoroutine(Countdown());
    }

    public IEnumerator Countdown()
    {
        while (timeCheck >= 0)
        {
            //Debug.Log("Time:" + timeCheck);
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
        this.gameObject.SetActive(false);

        GameObject exitObstacleClone = Instantiate(exitObstacle, transform.position + Vector3.down, transform.rotation);
    }
}
