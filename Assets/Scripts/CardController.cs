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

    private void OnTriggerStay2D(Collider2D collision)
    {
            
            //Debug.Log($"OnCollisionEnter2D : {collision.tag}:{collision.name}");
    
            if (collision.tag == "NumCrad")
            {
                //Debug.Log($"OnCollisionEnter2D : {collision.tag}:{collision.name}");
    
                //collision.transform.position = new Vector2(transform.position.x, transform.position.y);
    
                SortCards();
            }
    }

    private void SortCards()
    {
        List<Transform> cards = new List<Transform>();

        foreach (Transform child in transform)
        {
            cards.Add(child);
        }

        cards.Sort((a, b) => a.position.x.CompareTo(b.position.x));

        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].SetSiblingIndex(i);
            //cards[i].transform.position = new Vector3(-5f + (i * 1.4f), transform.position.y, 0);
            cards[i].transform.position = new Vector3(-5f + i, transform.position.y, 0);
        }
    }

}
