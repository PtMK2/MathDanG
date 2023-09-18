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
    

    private string enemyName = "hogehoge";// ���̕ϐ��͏������Ǝv���Ώ�����
    private string enemyDescription = "hogehoge piyopiyo";// ���̕ϐ��͏������Ǝv���Ώ�����

    

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

                // �N���b�N���ꂽ�G�̏��擾
                enemyName = EnemyGameObj.GetComponent<EnemyController>().enemyName;
                enemyDescription = EnemyGameObj.GetComponent<EnemyController>().enemyDescription;

                // �擾���������p�l���ɔ��f������
                enemyInfoPanel.transform.Find("EnemyName").GetComponent<TextMeshProUGUI>().text = enemyName;
                enemyInfoPanel.transform.Find("EnemyDescription").GetComponent<TextMeshProUGUI>().text = enemyDescription;

                enemyInfoPanel.SetActive(true);
                //Debug.Log(EnemyGameObj.name);
            }
        }
    }

}
