using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorcerer : Enemy
{
    private void Awake()
    {
        //triggerAction = SorcererTrigger;
        shotAction = EnemyShot_Default;
    }

    private void OnEnable()
    {
        //AssignStats();

        StartCoroutine(BecomeInvisible());
    }

    private IEnumerator BecomeInvisible()
    {
        Color sorcererColor = GetComponent<Renderer>().material.color;
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            
            GetComponent<Collider>().enabled = false;
            
            sorcererColor.a = 100f/255f;
            GetComponent<Renderer>().material.color = sorcererColor;

            yield return new WaitForSeconds(1.0f);

            GetComponent<Collider>().enabled = true;
            
            sorcererColor.a = 1;
            GetComponent<Renderer>().material.color = sorcererColor;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>()) StartCoroutine(DrainHealth(collision.gameObject.GetComponent<Player>()));
    }

    private void OnCollisionExit(Collision collision)
    {
        StopAllCoroutines();
    }

}
