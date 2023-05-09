using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonShot : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Wall")
        {
            gameObject.SetActive(false);
        }
        if (other.gameObject.GetComponent<Player>())
        {
            other.gameObject.GetComponent<Player>().currentHealth -= damage;
        }
    }
}
