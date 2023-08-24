using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "敵データベース")]
public class EnemyData : ScriptableObject
{
    public string enrmyName;

    public string enemyDescription;

    public int hp;
}
