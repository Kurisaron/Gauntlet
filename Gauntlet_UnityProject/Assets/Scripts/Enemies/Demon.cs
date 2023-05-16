using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : Enemy
{
    private List<GameObject> demonProjectiles = new List<GameObject>();
    [SerializeField] private GameObject demonProjectile;
    private Vector3 shootDirection;

    private void Awake()
    {
        //triggerAction = DemonTrigger;
        shotAction = EnemyShot_Default;
        moveAction = Move;
    }

    private void OnEnable()
    {
        //AssignStats();

        
    }

    private void Start()
    {
        demonProjectiles.Add(demonProjectile);
        StartCoroutine(ShootProjectile());
    }

    protected override void Update()
    {
        base.Update();

        UpdateShootDirection();
    }

    private void UpdateShootDirection()
    {
        Player target = null;
        float minDistance = 5000.0f;

        foreach (Player player in GameManager.Instance.players)
        {
            if (player == null) continue;

            if (Vector3.Distance(transform.position, player.transform.position) < minDistance)
            {
                minDistance = Vector3.Distance(transform.position, player.transform.position);
                target = player;
            }
        }

        if (target != null)
        {
            shootDirection = (target.transform.position - transform.position).normalized;
        }
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
                shot.GetComponent<DemonShot>().moveDirection = shootDirection;
                
                while(timeAlive <= 2.0f)
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
