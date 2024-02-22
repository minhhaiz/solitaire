using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottoms : MonoBehaviour
{
   public List<GameObject> cards  = new List<GameObject>();
    public Vector3 offSet;
    public BoxCollider2D boxCollider;
    public int value;
    public Cards.Suit suit;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (cards.Count == 0) {
            boxCollider.offset = new Vector2 (0.002121329f, 0.003210127f);
            boxCollider.size = new Vector2(0.04539752f, 1.00642f);
            value = 14;
            suit = Cards.Suit.S;
            return;
        }
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].GetComponent<Cards>().isOnRoll = false;
            if (cards[i] != null && !cards[i].GetComponent<Cards>().isDragging) {
                cards[i].GetComponent<Cards>().targetPos = transform.position + offSet * i;
                if (i == cards.Count - 1)
                {
                    cards[i].GetComponent<Cards>().faceUp = true;
                    value = cards[i].GetComponent<Cards>().value;
                    suit = cards[i].GetComponent<Cards>().suit;
                }
                cards[i].transform.SetParent(transform);
            }
        }

       boxCollider.size = new Vector2(0.04539752f, cards[cards.Count - 1].GetComponent<BoxCollider2D>().size.y);
       boxCollider.offset =new Vector2(0.002121329f, cards[cards.Count - 1].GetComponent<BoxCollider2D>().offset.y) - new Vector2(0, transform.position.y - cards[cards.Count - 1].transform.position.y);
    }
}
