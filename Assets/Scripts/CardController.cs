using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CardController : MonoBehaviour
{
    [SerializeField]
    private Camera _Camera;
    public float _transformX;
    public float _transformY;
    public float _transformZ;

    private bool _isDragging = false;// �J�[�h���h���b�O���Ă��邩
    private bool _isCollider = false;// �J�[�h���̓G���A�ƐڐG���Ă��邩

    // Start is called before the first frame update
    void Start()
    {
        _transformX = transform.position.x;
        _transformY = transform.position.y;
        _transformZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isCollider && !_isDragging)// ���̓G���A�ƏՓ˂��Ă��Ȃ��@���@�h���b�O���Ă��Ȃ��Ƃ��A�J�[�h�������ʒu�ɖ߂�
        {
            transform.position = new Vector3(_transformX, _transformY, _transformZ);
            transform.parent = null;
        }
    }

    private void OnMouseDrag()
    {
        transform.position = (Vector2)_Camera.ScreenToWorldPoint(Input.mousePosition);// �ʒu���}�E�X�̈ʒu�Ɠ���������
        _isDragging = true;
    }

    private void OnMouseUp()
    {
        _isDragging = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log($"OnTriggerStay2D : {collision.tag}:{collision.name}");

        if (!_isDragging && collision.tag == "CardPutArea")// �h���b�O���I������Ƃ��ɓ��͗��ƏՓ˂��Ă�����ʒu�𓯊�����
        {
            transform.position = collision.transform.position;
            transform.parent = collision.transform;
        }

        if (collision.name == "CardArea")
        {
            transform.parent = collision.transform;
        }

        _isCollider = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isCollider = false;
        transform.parent.SetParent(null);
    }

}
