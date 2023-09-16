using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    [SerializeField]
    private EnemyData _enemyData;// �f�[�^������擾

    [SerializeField]
    private Slider playerHpBar;

    public string enemyName;
    public string enemyDescription;
    public int enemyHp;

    public int enemyNowHp;

    private Animator animator;

    void Awake()
    {
        enemyName = _enemyData.enrmyName;
        enemyDescription = _enemyData.enemyDescription;
        enemyHp = _enemyData.hp;

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
            AttackToPlayer(Random.Range(1, 10));
        }
    }


    /// <summary>
    /// �v���C���[�ւ̍U������
    /// </summary>
    public void AttackToPlayer(int point)
    {
        playerHpBar.value -= point;
    }

    public void Dead()
    {
        Destroy(gameObject);
    }
}
