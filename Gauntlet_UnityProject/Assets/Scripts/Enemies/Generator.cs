using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Foe
{
    
    
    private IEnumerator SpawnEnemy(EnemyType enemyType)
    {
        yield return new WaitForSeconds(1);
    }
}
