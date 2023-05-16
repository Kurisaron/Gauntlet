using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobber : Enemy
{
    [SerializeField] private GameObject lobberProjectile;
    private List<GameObject> lobberProjectiles = new List<GameObject>();
    private bool runningAway = false;
    
    private void Awake()
    {
        //triggerAction = LobberTrigger;
        shotAction = EnemyShot_Default;
        moveAction = Move;
    }

    private void Start()
    {
        lobberProjectiles.Add(lobberProjectile);
        StartCoroutine(ShootProjectile());
    }

    public override void Move()
    {
        //Debug.Log("Lobber Moving");
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
            Debug.Log(Vector3.Distance(target.transform.position, transform.position));
            
            runningAway = Vector3.Distance(target.transform.position, transform.position) < 3.0f;
            if (runningAway) RunAway(target);
            else RunTowards(target);
            
        }
    }

    private void RunAway(Player target)
    {
        //Debug.Log("Lobber Running Away");
        if (Vector3.Distance(transform.position, target.transform.position) - 3.0f <= 0.25f) return;

        transform.position += speed * Time.deltaTime * (transform.position - target.transform.position);
    }

    private void RunTowards(Player target)
    {
        //Debug.Log("Lobber Running Towards");
        if (Vector3.Distance(transform.position, target.transform.position) - 3.0f <= 0.25f) return;
        transform.position += speed * Time.deltaTime * (target.transform.position - transform.position);
    }

    private IEnumerator ShootProjectile()
    {
        while (true)
        {
            if (runningAway) continue;

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
