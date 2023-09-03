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

    [SerializeField]
    private TMPro.TMP_Text stageText;

    private List<Transform> targets = new List<Transform>();

    private int stageNum = 1;// ステージ数

    // Start is called before the first frame update
    void Start()
    {
        stageText.SetText(stageText.text);

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
            EnemyDead();//仮
        }
    }


    /// <summary>
    /// 敵へ攻撃する処理
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
    /// 敵が死んだときの処理
    /// </summary>
    public void EnemyDead()
    {
        foreach (Transform child in enemys)
        {
            GameObject.Destroy(child.gameObject);
        }
        NextStage();
    }

    private void NextStage()
    {
        stageText.SetText("STAGE:{0}", ++stageNum);
        enemyHpBar.value = enemyHpBar.maxValue;//仮
    }
}
