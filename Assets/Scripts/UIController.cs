using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text stageText;
    [SerializeField]
    private GameObject enemyInfoPanel;
    [SerializeField]
    private Slider enemyHpBar;

    public int stageNum = 0;// ステージ数

    private GameObject EnemyGameObj;
    private GameObject[] allEnemyGOs;

    private string enemyName = "hogehoge";// この変数は消そうと思えば消せる
    private string enemyDescription = "hogehoge piyopiyo";// この変数は消そうと思えば消せる

    private int enemySumHp = 0;

    // Start is called before the first frame update
    void Start()
    {
        stageText.SetText(stageText.text);

        allEnemyGOs = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log(allEnemyGOs.Length);
        foreach (GameObject go in allEnemyGOs)
        {
            Debug.Log(go.GetComponent<EnemyController>().enemyHp);
            enemySumHp += go.GetComponent<EnemyController>().enemyHp;
        }

        enemyHpBar.maxValue = enemySumHp;
        enemyHpBar.value = enemyHpBar.maxValue;

        Debug.Log(enemySumHp);
    }

    // Update is called once per frame
    void Update()
    {
        stageText.SetText("STAGE:{0}", stageNum);

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("clicked");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2D = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            if (hit2D && hit2D.transform.gameObject.tag == "Enemy")
            {
                //Debug.Log(EnemyGameObj == hit2D.transform.gameObject);
                if (EnemyGameObj == hit2D.transform.gameObject && enemyInfoPanel.activeInHierarchy)
                {
                    enemyInfoPanel.SetActive(false);
                    return;
                }

                EnemyGameObj = hit2D.transform.gameObject;

                // クリックされた敵の情報取得
                enemyName = EnemyGameObj.GetComponent<EnemyController>().enemyName;
                enemyDescription = EnemyGameObj.GetComponent<EnemyController>().enemyDescription;

                // 取得した情報をパネルに反映させる
                enemyInfoPanel.transform.Find("EnemyName").GetComponent<TextMeshProUGUI>().text = enemyName;
                enemyInfoPanel.transform.Find("EnemyDescription").GetComponent<TextMeshProUGUI>().text = enemyDescription;

                enemyInfoPanel.SetActive(true);
                //Debug.Log(EnemyGameObj.name);
            }
        }
    }

}
