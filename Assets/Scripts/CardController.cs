using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    [SerializeField]
    private Camera _Camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDrag()
    {
        transform.position = (Vector2)_Camera.ScreenToWorldPoint(Input.mousePosition);// �ʒu���}�E�X�̈ʒu�Ɠ���������
    }
}
