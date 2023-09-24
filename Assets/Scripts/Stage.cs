using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stage : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text stageText;
    // Start is called before the first frame update
    int stage = GameManager.stageNum;
    void Start()
    {
        stageText.SetText("最高スコアは"+stage+"です！");
    }

}
