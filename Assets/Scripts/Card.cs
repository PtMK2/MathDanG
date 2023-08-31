using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField]
    private CardData _cardData;// �f�[�^������擾

    private Camera _Camera;

    public string cardName;

    // Start is called before the first frame update
    void Start()
    {
        cardName = _cardData.cardName;

        _Camera = Camera.main;

        //Debug.Log($"start");
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    void OnMouseDrag()
    {

        //�I�u�W�F�N�g�̍��W��ύX����
        transform.position = (Vector2)_Camera.ScreenToWorldPoint(Input.mousePosition); ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log($"OnTriggerStay2D : {collision.tag}:{collision.name}");


        switch (collision.name)
        {
            case "CardArea":
                transform.SetParent(collision.transform);
                break;
            case "CardPutArea":
                transform.SetParent(collision.transform);
                break;
            default:
                break;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.SetParent(null);
    }
}
