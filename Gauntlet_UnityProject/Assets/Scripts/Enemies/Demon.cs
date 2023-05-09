using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : Enemy
{
    private List<GameObject> demonProjectiles = new List<GameObject>();
    [SerializeField] private GameObject demonProjectile;

    private void Awake()
    {
        triggerAction = DemonTrigger;
    }

    private void OnEnable()
    {
        AssignStats();

        
    }

    private void Start()
    {
        demonProjectiles.Add(demonProjectile);
        StartCoroutine(ShootProjectile());
    }

    public override void Move()
    {
        
    }

    private IEnumerator ShootProjectile()
    {
        while (true)
        {
            foreach (GameObject projectile in demonProjectiles)
            {
                
                GameObject shot = ObjectPooler.Instance.GetPooledObject(demonProjectile.name);
                shot.transform.parent = null;
                float timeAlive = 0.0f;

                shot.transform.SetPositionAndRotation(transform.position, transform.rotation);
                
                shot.SetActive(true);
                
                shot.transform.position += 10.0f * Time.deltaTime * shot.transform.forward;
                timeAlive += Time.deltaTime;

                if (timeAlive >= 2.0f) gameObject.SetActive(false);
            }
           
            yield return new WaitForSeconds(1);

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

    private void DemonTrigger(Collider other)
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
