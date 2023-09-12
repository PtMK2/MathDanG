using System;
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

    private int stageNum = 1;// ステージ数

    private GameObject[] allEnemyGOs;

    private int enemySumHp = 0;

    public bool isEnemyDead = false;

    private readonly string[] _cardNames = {
        "Card zero",
        "Card one",
        "Card two",
        "Card three",
        "Card four",
        "Card five",
        "Card six",
        "Card seven",
        "Card eight",
        "Card nine",
        "Card plus",
        "Card minus",
        "Card multiplication",
        "Card divide"
    };

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

    private IEnumerator CloneCard(GameObject gameObject)
    {

        // ごり押し、いつかなおしたい


        GameObject clone = Instantiate(gameObject, new Vector2(10,10), Quaternion.identity);// 画面外にカード生成
        yield return null;// 次の処理で1フレーム以上待たないと結果がnullなので待つ
        //Debug.Log($"name={clone.GetComponent<Card>().cardName}");
        String cName = clone.GetComponent<Card>().cardName;
        Transform cTf = GameObject.Find("CardArea " + cName).transform;
        //clone.transform.position = new Vector2(cTf.position.x, cTf.position.y);
        clone.transform.SetParent(cTf);
        //clone.transform.position = new Vector2(0, 0);
        Debug.Log($"name={clone.transform.parent.name}");
        clone.GetComponent<Card>().tmpPos = new Vector2(cTf.transform.position.x, cTf.transform.position.y);

    }
    private void EnemyDropCards()
    {
        // 敵が落とすカードの枚数
        int dropCardNum = UnityEngine.Random.Range(1, 4);

        

        Debug.Log("========================================");


        // 敵が落とすカードの種類を決定
        for (int i = 0; i < dropCardNum; i++)
        {
            // 敵が落とすカードの種類
            int dropCardType = UnityEngine.Random.Range(0, _cardNames.Length);
            Debug.Log($"生成したカード={_cardNames[dropCardType]}");

            GameObject gameObject = Resources.Load<GameObject>(_cardNames[dropCardType]);
            StartCoroutine(CloneCard(gameObject));
            //GameObject clone = Instantiate(gameObject, new Vector2(0, 0), Quaternion.identity, null);
            //Debug.Log($"name={clone.GetComponent<Card>().cardName}");

            //Instantiate(gameObject, new Vector2(0, 0), Quaternion.identity, GameObject.Find("CardArea " + gameObject.GetComponent<Card>().cardName).transform);
        }

        Debug.Log($"生成カード総数={dropCardNum}");
        Debug.Log("========================================");

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

        targets.Clear();

        Debug.Log("dead");
        EnemyDropCards();
        NextStage();
    }

    /// <summary>
    /// 次のステージへ進む処理
    /// </summary>
    private void NextStage()
    {

        stageText.SetText("STAGE:{0}", ++stageNum);

        // 敵を生成 仮
        GameObject gameObject = Resources.Load<GameObject>("Slime");
        Instantiate(gameObject, new Vector2(2,2), Quaternion.identity, enemys);

        ResetStage();
    }


    /// <summary>
    /// ステージ初期化処理
    /// </summary>
    private void ResetStage()
    {
        //Debug.Log("============================");

        isEnemyDead = false;

        allEnemyGOs = GameObject.FindGameObjectsWithTag("Enemy");
        enemySumHp = 0;
        //Debug.Log(allEnemyGOs.Length);

        foreach (GameObject go in allEnemyGOs)
        {
            //Debug.Log(go.name);
            targets.Add(go.transform);
            //Debug.Log(go.GetComponent<EnemyController>().enemyHp);
            enemySumHp += go.GetComponent<EnemyController>().enemyHp;
        }

        enemyHpBar.maxValue = enemySumHp;
        enemyHpBar.value = enemyHpBar.maxValue;

        //Debug.Log($"リセット後enemuSumHp={enemySumHp}");
    }
}
