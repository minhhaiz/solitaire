using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Top : MonoBehaviour
{
    public List<GameObject> cards;
    public int value;
    public Cards.Suit suit;
    public bool test = false;
    // Update is called once per frame
    void Update()
    {

        if (cards.Count == 0)
        {
            value = 0;
            suit = Cards.Suit.H;
            return;
        }
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].GetComponent<Cards>().isOnRoll = false;

            cards[i].GetComponent<Cards>().targetPos = transform.position + Vector3.back * 0.01f * i;
            cards[i].GetComponent<Cards>().faceUp = true;

            cards[i].transform.SetParent(transform);
            if(i == cards.Count - 1)
            {
                value = cards[i].GetComponent<Cards>().value;
                suit = cards[i].GetComponent<Cards>().suit;
            }
        }

      
       
    }

}
