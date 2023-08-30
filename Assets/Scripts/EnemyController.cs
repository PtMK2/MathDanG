using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField]
    private EnemyData _enemyData;// ƒf[ƒ^‚©‚çî•ñæ“¾

    public string enemyName;
    public string enemyDescription;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        enemyName = _enemyData.enrmyName;
        enemyDescription = _enemyData.enemyDescription;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // random timing to animation
        if (Random.Range(0, 5000) < 1)
        {
            animator.SetTrigger("Jump");
        }
    }
}
