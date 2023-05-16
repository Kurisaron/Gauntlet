using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Foe
{
    private List<EnemyType> enemyObjectList = new List<EnemyType>();


    private void Awake()
    {
        //triggerAction = GeneratorTrigger;
        shotAction = GeneratorShot;
    }

    private void OnEnable()
    {
        health = level;
        enemyObjectList.Add(enemyType);

        GameEventBus.Subscribe(GameEvent.PlayerAdded, StartSpawnEnemy);
    }


    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameEvent.PlayerAdded, StartSpawnEnemy);
    }

    private void StartSpawnEnemy()
    {
        StartCoroutine(SpawnEnemy(enemyType));
    }

    private IEnumerator SpawnEnemy(EnemyType enemyType)
    {
        while (true)
        {
            foreach (EnemyType enemy in enemyObjectList)
            {
                GameObject enemyToSpawn = ObjectPooler.Instance.GetPooledObject(enemyType.ToString());
                
                if (enemyToSpawn != null)
                {
                    enemyToSpawn.transform.SetPositionAndRotation(transform.position, transform.rotation);
                    
                    enemyToSpawn.GetComponent<Enemy>().matRed = 255.0f * enemyToSpawn.GetComponent<Renderer>().material.color.r;
                    enemyToSpawn.GetComponent<Enemy>().matGreen = 255.0f * enemyToSpawn.GetComponent<Renderer>().material.color.g;
                    enemyToSpawn.GetComponent<Enemy>().matBlue = 255.0f * enemyToSpawn.GetComponent<Renderer>().material.color.b;
                    
                    enemyToSpawn.GetComponent<Enemy>().level = level;
                    enemyToSpawn.GetComponent<Enemy>().AssignStats();

                    if(enemyType == EnemyType.Ghost)
                    {
                        enemyToSpawn.GetComponent<Ghost>().AssignStats();
                    }

                    enemyToSpawn.SetActive(true);
                }
            }

            yield return new WaitForSeconds(1);
        }
    }

    private void GeneratorShot(PlayerShot shot)
    {
        ReduceHealth(shot.shooter.ShotPower, shot.shooter);
        level--;
    }

}
