using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorcerer : Enemy
{
    private void Awake()
    {
        triggerAction = SorcererTrigger;
    }

    private void OnEnable()
    {
        AssignStats();

        StartCoroutine(BecomeInvisible());
    }

    public override void Move()
    {
        
    }

    private IEnumerator BecomeInvisible()
    {
        Color sorcererColor = GetComponent<Renderer>().material.color;
        while (true)
        {
            
            GetComponent<Collider>().enabled = false;
            sorcererColor.a = 100f/255f;
            GetComponent<Renderer>().material.color = sorcererColor;
            yield return new WaitForSeconds(1);
            GetComponent<Collider>().enabled = true;
            sorcererColor.a = 255f/255f;
            GetComponent<Renderer>().material.color = sorcererColor;
        }
    }

    private void SorcererTrigger(Collider other)
    {
        if (other.gameObject.transform.parent.name.Contains("Shot"))
        {
            //health - shot power?
            //also change this so that it accounts for players who have the same class
            //tracks who shot that projectile so that the correct player is awarded a point
            if (other.gameObject.transform.parent.name.Contains("Elf") && FindObjectOfType<Player>().name.Contains("Elf"))
            {
                AddScore(GameObject.Find("Elf").GetComponent<Player>(), scoreIncrease);
                other.gameObject.SetActive(false);
            }
            if (other.gameObject.transform.parent.name.Contains("Warrior") && FindObjectOfType<Player>().name.Contains("Warrior"))
            {
                AddScore(GameObject.Find("Warrior").GetComponent<Player>(), scoreIncrease);
                other.gameObject.SetActive(false);
            }
            if (other.gameObject.transform.parent.name.Contains("Valkyrie") && FindObjectOfType<Player>().name.Contains("Valkyrie"))
            {
                AddScore(GameObject.Find("Valkyrie").GetComponent<Player>(), scoreIncrease);
                other.gameObject.SetActive(false);
            }
            if (other.gameObject.transform.parent.name.Contains("Wizard") && FindObjectOfType<Player>().name.Contains("Wizard"))
            {
                AddScore(GameObject.Find("Wizard").GetComponent<Player>(), scoreIncrease);
                other.gameObject.SetActive(false);
            }
        }
    }
}
