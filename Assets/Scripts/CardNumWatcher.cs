using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardNumWatcher : MonoBehaviour
{

    [SerializeField]
    private GameObject cardArea;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int cardNum = cardArea.transform.childCount;
        //Debug.Log(cardNum);

        this.GetComponent<TMPro.TMP_Text>().text = cardNum.ToString();
    }
}
