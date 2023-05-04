using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Foe
{
    private List<EnemyType> enemyObjectList = new List<EnemyType>();

    private void Start()
    {
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
                    enemyToSpawn.transform.position = transform.position;
                    enemyToSpawn.transform.rotation = transform.rotation;
                    enemyToSpawn.SetActive(true);
                }
            }

            yield return new WaitForSeconds(1);
        }
    }
}
