using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class RollCard : MonoBehaviour
{
    public List<GameObject> cards = new List<GameObject>();
    private int oldChild;
 

 
    private void Update()
    {
        int total = cards.Count;
        if (oldChild == total) return;
        oldChild = total;
        for (int i = 0; i < total; i++)
        {
            cards[i].GetComponent<Cards>().isOnRoll = true;
            cards[i].transform.DORotate(new Vector3(0, 360, 0), 0.6f);
            cards[i].transform.SetParent(transform);

            if (cards[i] != null && !cards[i].GetComponent<Cards>().isDragging)
            {
                if (i == 0)
                {
                    cards[i].GetComponent<Cards>().targetPos = transform.position + Vector3.back * 0.001f * i;
                    cards[i].GetComponent<Cards>().faceUp = true;
                    cards[i].GetComponent<Cards>().boxCollider.enabled = true;

                }

                if (i > 0)
                {
                    cards[i - 1].GetComponent<Cards>().targetPos = transform.position + Vector3.left * 0.2f +  Vector3.back * 0.001f * i;
                    cards[i - 1].GetComponent<Cards>().boxCollider.enabled = false;
                    cards[i].GetComponent<Cards>().targetPos = transform.position + Vector3.right * 0.03f + Vector3.back * 0.002f * i;
                    cards[i].GetComponent<Cards>().faceUp = true;
                    cards[i].GetComponent<Cards>().boxCollider.enabled = true;
                }
                if (i > 1)
                {
                    cards[i - 2].GetComponent<Cards>().targetPos = transform.position + Vector3.left * 0.4f + Vector3.back * 0.001f * i;
                    cards[i - 2].GetComponent<Cards>().boxCollider.enabled = false;
                    cards[i].GetComponent<Cards>().targetPos = transform.position + Vector3.right * 0.03f + Vector3.back * 0.002f * i;
                    cards[i].GetComponent<Cards>().faceUp = true;
                    cards[i].GetComponent<Cards>().boxCollider.enabled = true;
                }

            }
        }
    }
}
