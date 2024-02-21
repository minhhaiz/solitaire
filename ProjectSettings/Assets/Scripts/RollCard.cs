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

            if (cards[i] != null && !cards[i].GetComponent<Cards>().isDragging)
            {
                cards[i].GetComponent<Cards>().targetPos = transform.position + Vector3.back * 0.001f * i;              
                cards[i].GetComponent<Cards>().faceUp = true;

            }
        }
    }
}
