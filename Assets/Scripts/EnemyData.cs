using UnityEngine;

[CreateAssetMenu(menuName = "敵データベース")]
public class EnemyData : ScriptableObject
{
    public string enrmyName;

    public string enemyDescription;

    public int hp;

    public int attack;

    public int attackRate;
}
