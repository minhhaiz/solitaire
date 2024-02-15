using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<GameObject> cards = new List<GameObject>();

    public Transform roll;

    private void OnMouseDown()
    {
        if(cards.Count > 0)
        {
            cards[cards.Count - 1].transform.SetParent(roll);
            roll.GetComponent<RollCard>().cards.Add(cards[cards.Count - 1]);
            cards.Remove(cards[cards.Count - 1]);
            GameManager.Instance.steps++;
        }
        else
        {
            roll.GetComponent<RollCard>().cards.Reverse();
            cards.AddRange(roll.GetComponent<RollCard>().cards);
            roll.GetComponent<RollCard>().cards.Clear();
            GameManager.Instance.steps++;
        }
    }
    private void Update()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].GetComponent<Cards>().targetPos = transform.position + Vector3.back * 0.001f * i;
            cards[i].GetComponent<Cards>().faceUp = false;
            cards[i].transform.SetParent(transform);
        }
    }
}
