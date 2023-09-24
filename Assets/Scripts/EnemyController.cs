using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    [SerializeField]
    private EnemyData _enemyData;// データから情報取得

    [SerializeField]
    private Slider playerHpBar;

    public string enemyName;
    public string enemyDescription;
    public int enemyHp;
    public int enemyAttack;

    public int enemyNowHp;

    private Animator animator;

    void Awake()
    {
        enemyName = _enemyData.enrmyName;
        enemyDescription = _enemyData.enemyDescription;
        enemyHp = _enemyData.hp;
        enemyAttack = _enemyData.attack;

        enemyNowHp = enemyHp;

        animator = GetComponent<Animator>();

        playerHpBar = GameObject.Find("PlayerHP").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        int random = Random.Range(0, 5000);

        // random timing to animation
        if (random < 1)
        {
            animator.SetTrigger("Jump");
        }

        if (random == 10)
        {
            AttackToPlayer(enemyAttack);
        }
    }


    /// <summary>
    /// プレイヤーへの攻撃処理
    /// </summary>
    public void AttackToPlayer(int point)
    {
        playerHpBar.value -= point;
        GameObject.FindWithTag("Player").GetComponent<Animator>().SetTrigger("Hurt");
    }

    public void Dead()
    {
        Destroy(gameObject);
    }
}
