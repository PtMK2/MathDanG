using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Slider enemyHpBar;

    [SerializeField]
    private Transform enemys;

    [SerializeField]
    private TMPro.TMP_Text stageText;

    private List<Transform> targets = new List<Transform>();

    private int stageNum = 1;// �X�e�[�W��

    private GameObject[] allEnemyGOs;

    private int enemySumHp = 0;

    public bool isEnemyDead = false;

    // Start is called before the first frame update
    void Start()
    {

        ResetStage();

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHpBar.value <= 0 && !isEnemyDead)
        {
            foreach (Transform transform in targets)
            {
                transform.GetComponent<Animator>().SetTrigger("Dead");
            }

            isEnemyDead = true;
        }

        if (isEnemyDead && enemys.childCount == 0)
        {
            EnemyDead();
        }
    }


    /// <summary>
    /// �G�֍U�����鏈��
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
    /// �G�����񂾂Ƃ��̏���
    /// </summary>
    public void EnemyDead()
    {

        targets.Clear();

        Debug.Log("dead");
        NextStage();
    }

    /// <summary>
    /// ���̃X�e�[�W�֐i�ޏ���
    /// </summary>
    private void NextStage()
    {

        stageText.SetText("STAGE:{0}", ++stageNum);

        // �G�𐶐� ��
        GameObject gameObject = Resources.Load<GameObject>("Slime");
        Instantiate(gameObject, new Vector2(-1,2), Quaternion.identity, enemys);

        ResetStage();
    }

    private void ResetStage()
    {
        Debug.Log("============================");

        isEnemyDead = false;

        allEnemyGOs = GameObject.FindGameObjectsWithTag("Enemy");
        enemySumHp = 0;
        Debug.Log(allEnemyGOs.Length);

        foreach (GameObject go in allEnemyGOs)
        {
            Debug.Log(go.name);
            targets.Add(go.transform);
            //Debug.Log(go.GetComponent<EnemyController>().enemyHp);
            enemySumHp += go.GetComponent<EnemyController>().enemyHp;
        }

        enemyHpBar.maxValue = enemySumHp;
        enemyHpBar.value = enemyHpBar.maxValue;

        Debug.Log($"���Z�b�g��enemuSumHp={enemySumHp}");
    }
}
