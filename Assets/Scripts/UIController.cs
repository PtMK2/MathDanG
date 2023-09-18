using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyInfoPanel;

    [SerializeField]
    private Slider enemyHpBar;

    [SerializeField]
    private Slider playerHpBar;

    private GameObject EnemyGameObj;
    

    private string enemyName = "hogehoge";// この変数は消そうと思えば消せる
    private string enemyDescription = "hogehoge piyopiyo";// この変数は消そうと思えば消せる

    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
