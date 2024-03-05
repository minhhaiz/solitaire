using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using System.Linq;

public class RollCard : MonoBehaviour
{
    public List<GameObject> cards = new List<GameObject>();
    private int oldChild;
    public Vector3 offset;

 
    private void Update()
    {

        int total = cards.Count;
        if (oldChild == total) return;
        oldChild = total;
        if (cards.Count >= 3)
        {
            cards[cards.Count - 3].GetComponent<Cards>().targetPos = transform.position;
            cards[cards.Count - 1].GetComponent<Cards>().faceUp = true;
            cards[cards.Count - 2].GetComponent<Cards>().faceUp = true;            
            cards[cards.Count - 3].GetComponent<Cards>().faceUp = true;
            cards[cards.Count - 3].transform.SetParent(transform);
            cards[cards.Count - 2].transform.SetParent(transform);
            cards[cards.Count - 1].transform.SetParent(transform);
            cards[cards.Count - 3].GetComponent<Rigidbody2D>().simulated = false;
            cards[cards.Count - 2].GetComponent<Rigidbody2D>().simulated = false;
            cards[cards.Count - 1].GetComponent<Rigidbody2D>().simulated = true;
            cards[cards.Count - 2].GetComponent<Cards>().targetPos = transform.position + offset;
            cards[cards.Count - 1].GetComponent<Cards>().targetPos = transform.position + offset * 2;
            if (cards.Count > 3)
            {
                for (int i = 0; i < cards.Count - 3; i++) {
                    cards[i].GetComponent<Cards>().targetPos = transform.position - Vector3.back;
                    cards[i].GetComponent<Cards>().faceUp = true; 
                    cards[i].GetComponent<Rigidbody2D>().simulated = false;
                    cards[i].transform.SetParent(transform);

                }
            }
            
        } else if (cards.Count == 2) {
            cards[cards.Count - 2].GetComponent<Cards>().targetPos = transform.position;
            cards[cards.Count - 1].GetComponent<Cards>().targetPos = transform.position + offset * 1;
            cards[cards.Count - 2].GetComponent<Cards>().faceUp = true;
            cards[cards.Count - 1].GetComponent<Cards>().faceUp = true;
            cards[cards.Count - 2].GetComponent<Rigidbody2D>().simulated = false;
            cards[cards.Count - 1].GetComponent<Rigidbody2D>().simulated = true;
            cards[cards.Count - 2].transform.SetParent(transform);
            cards[cards.Count - 1].transform.SetParent(transform);
        }
        else if (cards.Count == 1) {
            cards[cards.Count - 1].GetComponent<Cards>().targetPos = transform.position;
            cards[cards.Count - 1].GetComponent<Cards>().faceUp = true;
            cards[cards.Count - 1].GetComponent<Rigidbody2D>().simulated = true;
            cards[cards.Count - 1].transform.SetParent(transform);

        }
    }
}
