using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    
    private void Update()
    {
        if (GameManager.Instance == null) return;
        
        if (GameManager.Instance.AnyPlayers())
        {
            Vector3 midPoint = GetMidPoint();
            transform.position = new Vector3(midPoint.x, transform.position.y, midPoint.z);
        }
    }

    private Vector3 GetMidPoint()
    {
        Vector3 point = Vector3.zero;
        Player[] activePlayers = GetActivePlayers();
 
        for (int i = 0; i < activePlayers.Length; i++)
        {
            point += activePlayers[i].transform.position;
        }
        point /= activePlayers.Length;

        // Replace y value (for zooming)
        float maxDistance = 0.0f;
        if (activePlayers.Length > 1)
        {
            for (int i = 0; i < activePlayers.Length; i++)
            {
                for (int j = 0; i < activePlayers.Length; i++)
                {
                    if (i == j) continue;

                    if (Vector3.Distance(activePlayers[i].transform.position, activePlayers[j].transform.position) > maxDistance) maxDistance = Vector3.Distance(activePlayers[i].transform.position, activePlayers[j].transform.position);
                }
            }
        }
        point.y = 10.0f + maxDistance;

        return point;
    }


    private Player[] GetActivePlayers()
    {
        return Array.FindAll(GameManager.Instance.players, player => player != null);
    }
}
