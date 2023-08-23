using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text stageText;

    public int stageNum = 0;// ステージ数

    // Start is called before the first frame update
    void Start()
    {
        stageText.SetText(stageText.text);
    }

    // Update is called once per frame
    void Update()
    {
        stageText.SetText("STAGE:{0}", stageNum);
    }
}
