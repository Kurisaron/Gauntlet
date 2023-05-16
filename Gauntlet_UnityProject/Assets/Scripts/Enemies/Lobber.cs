using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobber : Enemy
{
    [SerializeField] private GameObject lobberProjectile;
    private List<GameObject> lobberProjectiles = new List<GameObject>();
    
    private void Awake()
    {
        //triggerAction = LobberTrigger;
        shotAction = EnemyShot_Default;
    }

    private void Start()
    {
        lobberProjectiles.Add(lobberProjectile);
        StartCoroutine(ShootProjectile());
    }

    public override void Move()
    {
        
    }

    private IEnumerator ShootProjectile()
    {
        while (true)
        {
            foreach (GameObject projectile in lobberProjectiles)
            {

                GameObject shot = ObjectPooler.Instance.GetPooledObject(lobberProjectile.name);
                shot.transform.parent = null;
                float timeAlive = 0.0f;

                shot.transform.SetPositionAndRotation(transform.position, transform.rotation);

                while (timeAlive <= 2.0f)
                {
                    shot.SetActive(true);
                    timeAlive += Time.deltaTime;
                    yield return null;
                }
                shot.SetActive(false);
            }

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
