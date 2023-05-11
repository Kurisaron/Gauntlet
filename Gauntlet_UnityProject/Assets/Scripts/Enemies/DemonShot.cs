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
            gameObject.SetActive(false);
        }
        else
        {
            other.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        transform.position += 10.0f * Time.deltaTime * transform.forward;
    }
}
