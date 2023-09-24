using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyInfoPanel;

    [SerializeField]
    private Slider enemyHpBar;
    private Image enemyHpBarImage;

    [SerializeField]
    private Slider playerHpBar;
    private Image playerHpBarImage;

    [SerializeField]
    private TMPro.TMP_Text enemyHpBarText;

    [SerializeField]
    private TMPro.TMP_Text playerHpBarText;

    private GameObject EnemyGameObj;
    

    private string enemyName = "hogehoge";// ���̕ϐ��͏������Ǝv���Ώ�����
    private string enemyDescription = "hogehoge piyopiyo";// ���̕ϐ��͏������Ǝv���Ώ�����

    

    // Start is called before the first frame update
    void Start()
    {

        enemyHpBarImage = enemyHpBar.fillRect.GetComponent<Image>();
        playerHpBarImage = playerHpBar.fillRect.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyHpBarText.text = enemyHpBar.value.ToString();
        playerHpBarText.text = playerHpBar.value.ToString();

        if (playerHpBar.value <= playerHpBar.maxValue * 0.2)
        {
            playerHpBarImage.color = Color.red;
        } else if (playerHpBar.value <= playerHpBar.maxValue * 0.4)
        {
            playerHpBarImage.color = Color.yellow;
        } else
        {
            //playerHpBarImage.color = Color.green;
            playerHpBarImage.color = new Color(0f, 0.816f, 0.090f);
        }

        if (enemyHpBar.value <= enemyHpBar.maxValue * 0.2)
        {
            enemyHpBarImage.color = Color.red;
        }
        else if (enemyHpBar.value <= enemyHpBar.maxValue * 0.4)
        {
            enemyHpBarImage.color = Color.yellow;
        }
        else
        {
            enemyHpBarImage.color = new Color(0f, 0.816f, 0.090f);
            //enemyHpBarImage.color = Color.green;
        }

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
