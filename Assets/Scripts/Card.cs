using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Card : MonoBehaviour
{
    [SerializeField]
    private CardData _cardData;// �f�[�^������擾

    private Camera _Camera;

    public string cardName;

    public bool _isTrigger = false;// �J�[�h���̓G���A�ƐڐG���Ă��邩
    public bool _isDragging = false;// �J�[�h���h���b�O���Ă��邩

    public Vector2 tmpPos;

    // Start is called before the first frame update
    void Start()
    {
        cardName = _cardData.cardName;

        _Camera = Camera.main;

        tmpPos = new Vector2(transform.position.x, transform.position.y);
        //Debug.Log($"tmpPos : {tmpPos}");

        //Debug.Log($"start");
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isTrigger && !_isDragging)
        {
            transform.position = tmpPos;
            transform.SetParent(GameObject.Find("CardArea " + cardName).transform);
        }
    }

    void OnMouseDrag()
    {
        _isDragging = true;

        //�I�u�W�F�N�g�̍��W��ύX����
        transform.position = (Vector2)_Camera.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseUp()
    {
        _isDragging = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log($"OnTriggerStay2D : {collision.tag}:{collision.name}");

        _isTrigger = true;

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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "CardArea" && !_isDragging)
        {
            tmpPos = (Vector2)_Camera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (collision.name == "CardPutArea" && !_isDragging)
        {
            transform.position = new Vector2(transform.position.x, collision.transform.position.y);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isTrigger = false;

        //transform.SetParent(null);
    }
}
