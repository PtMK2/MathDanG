using UnityEngine;

public class Card : MonoBehaviour
{

    [SerializeField]
    private CardData _cardData;// データから情報取得

    private Camera _Camera;

    public string cardName;

    // Start is called before the first frame update
    void Start()
    {
        cardName = _cardData.cardName;

        //Debug.Log($"start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDrag()
    {

        //オブジェクトの座標を変更する
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition); ;
    }
}
