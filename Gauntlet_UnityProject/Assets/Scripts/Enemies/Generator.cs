using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Foe
{
    private List<EnemyType> enemyObjectList = new List<EnemyType>();


    private void Awake()
    {
        triggerAction = GeneratorTrigger;
    }
    private void Start()
    {
        health = level;


        enemyObjectList.Add(enemyType);
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
                    enemyToSpawn.GetComponent<Enemy>().level = level;
                    enemyToSpawn.SetActive(true);
                }
            }

            yield return new WaitForSeconds(1);
        }
    }

    private void GeneratorTrigger(Collider other)
    {
        if (other.gameObject.transform.parent.name.Contains("Shot"))
        {
            health--;
            level--;
        }
    }
}
