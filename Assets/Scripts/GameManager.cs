using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Slider enemyHpBar;

    [SerializeField]
    private Slider playerHpBar;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Transform enemys;

    [SerializeField]
    private TMPro.TMP_Text stageText;

    private List<Transform> targets = new List<Transform>();

    private int stageNum = 1;// �X�e�[�W��

    private GameObject[] allEnemyGOs;

    private int enemySumHp = 0;

    public bool isEnemyDead = false;
    public bool isPlayerDead = false;

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
    string[] availableObjectNames = new string[] { "Slime", "RedSlime", "GreenSlime", "IcekingSlime", "YellowSlime"}; // 使用可能なオブジェクトの名前をリストに追加します

    // Start is called before the first frame update
    void Start()
    {
        InitializeGame();
        ResetStage();

    }
    void InitializeGame()
    {
        playerHpBar.value=200;
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

        if (playerHpBar.value <= 0 && !isPlayerDead)
        {
            player.GetComponent<Animator>().SetTrigger("Death");
            //isPlayerDead = true;
            //SceneManager.LoadScene("GameOver");
            StartCoroutine(PlayetDead());
        }
    }

    private IEnumerator PlayetDead()
    {
        isPlayerDead = true;

        yield return new WaitForSeconds(2f);

        Initiate.Fade("GameOver", Color.black, 8f);
    }

    private IEnumerator CloneCard(GameObject gameObject)
    {

        // ���艟���A�����Ȃ�������


        GameObject clone = Instantiate(gameObject, new Vector2(10,10), Quaternion.identity);// ��ʊO�ɃJ�[�h����
        yield return null;// ���̏�����1�t���[���ȏ�҂��Ȃ��ƌ��ʂ�null�Ȃ̂ő҂�
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
        // �G�����Ƃ��J�[�h�̖���
        int dropCardNum = UnityEngine.Random.Range(7, 9);

        

        Debug.Log("========================================");


        // �G�����Ƃ��J�[�h�̎�ނ�����
        for (int i = 0; i < dropCardNum; i++)
        {
            // �G�����Ƃ��J�[�h�̎��
            int dropCardType = UnityEngine.Random.Range(0, _cardNames.Length);
            Debug.Log($"���������J�[�h={_cardNames[dropCardType]}");

            GameObject gameObject = Resources.Load<GameObject>(_cardNames[dropCardType]);
            StartCoroutine(CloneCard(gameObject));
            //GameObject clone = Instantiate(gameObject, new Vector2(0, 0), Quaternion.identity, null);
            //Debug.Log($"name={clone.GetComponent<Card>().cardName}");

            //Instantiate(gameObject, new Vector2(0, 0), Quaternion.identity, GameObject.Find("CardArea " + gameObject.GetComponent<Card>().cardName).transform);
        }

        Debug.Log($"�����J�[�h����={dropCardNum}");
        Debug.Log("========================================");

    }

    /// <summary>
    /// �G�֍U�����鏈��
    /// </summary>
    /// <param name="point"></param>
    public void AttackToEnemy(int point)
    {
        //Debug.Log("Attack");
        Debug.Log($"{((enemyHpBar.value == point) ? "�s�b�^���I" : "")}");
        if (enemyHpBar.value == point)
        {
            playerHpBar.value = playerHpBar.maxValue;
        }
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
        EnemyDropCards();
        NextStage();
    }

    /// <summary>
    /// ���̃X�e�[�W�֐i�ޏ���
    /// </summary>
    private void NextStage()
    {

        stageText.SetText("STAGE:{0}", ++stageNum);

        // �G�𐶐� ��
        
        int enemyNum = UnityEngine.Random.Range(1, 4);
        for(int i = 0; i < enemyNum;i++ )
        {
            int randomIndex = UnityEngine.Random.Range(0, availableObjectNames.Length);
            GameObject gameObject = Resources.Load<GameObject>(availableObjectNames[randomIndex]);
            Instantiate(gameObject, new Vector2(-4+i*3,2), Quaternion.identity, enemys);
        }
        //GameObject gameObject = Resources.Load<GameObject>("Slime");
        //Instantiate(gameObject, new Vector2(2,2), Quaternion.identity, enemys);

        ResetStage();
    }


    /// <summary>
    /// �X�e�[�W����������
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

        //Debug.Log($"���Z�b�g��enemuSumHp={enemySumHp}");
    }
     
}
