using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{

    [SerializeField]
    private GameObject _card;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        string tmpFormula = "";

        foreach (Transform child in _card.transform)
        {
            tmpFormula += child.GetComponent<Card>().cardName;
        }

        Debug.Log($"tmpFormula : {tmpFormula}");
    }
}
