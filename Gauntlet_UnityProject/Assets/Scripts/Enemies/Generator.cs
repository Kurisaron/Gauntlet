using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Foe
{

    private void OnEnable()
    {
        StartCoroutine(SpawnEnemy(enemyType));
    }


    private IEnumerator SpawnEnemy(EnemyType enemyType)
    {
        GameObject enemyObject = ObjectPooler.Instance.GetPooledObject(enemyType.ToString());
        //List<GameObject> enemyObjectList = 

        while (true)
        {
            /*foreach (var item in collection)
            {

            }*/
            
            
            
            if (enemyObject != null)
            {
                enemyObject.transform.position = transform.position;
                enemyObject.transform.rotation = transform.rotation;
                enemyObject.SetActive(true);
            }

            yield return new WaitForSeconds(1);
        }
    }
}
