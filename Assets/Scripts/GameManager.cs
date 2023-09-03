using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Slider enemyHpBar;

    [SerializeField]
    private Transform enemys;

    private List<Transform> targets = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in enemys)
        {

            targets.Add(child);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHpBar.value <= 0)
        {
            //child.GetComponent<Animator>().SetTrigger("Dead");
        }
    }


    /// <summary>
    /// “G‚ÖUŒ‚‚·‚éˆ—
    /// </summary>
    /// <param name="point"></param>
    public void AttackToEnemy(int point)
    {
        //Debug.Log("Attack");
        enemyHpBar.value -= point;
        foreach (Transform transform in targets)
        {
            transform.GetComponent<Animator>().SetTrigger("Damaged");
        }
    }


    /// <summary>
    /// “G‚ª€‚ñ‚¾‚Æ‚«‚Ìˆ—
    /// </summary>
    public void EnemyDead()
    {
        foreach (Transform child in enemys)
        {
            GameObject.Destroy(child.gameObject);
        }
    }   
}
