using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType
{
    Ghost,
    Lobber,
    Grunt,
    Demon,
    Sorcerer,
    Thief,
    Death
}


public class Foe : MonoBehaviour
{
    //VARIABLES
    public float health;
    public int level;
    public EnemyType enemyType;
    public Action triggerAction;
}
