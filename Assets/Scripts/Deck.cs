using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<GameObject> cards = new List<GameObject>();
    public GameObject outline;
    public Transform roll;


    private void OnMouseDown()
    {
     
        
        GameManager.Instance.StopBlink();
        if (cards.Count > 0)
        {
            cards[cards.Count - 1].transform.SetParent(roll);
            roll.GetComponent<RollCard>().cards.Add(cards[cards.Count - 1]);
            cards.Remove(cards[cards.Count - 1]);
            GameManager.Instance.stepsCount++;
            GameManager.Instance.AddStep(new GameManager.ObjectState() { card = roll.GetComponent<RollCard>().cards.Last().GetComponent<Cards>(), parent = transform });
        }
        else
        {
            GameManager.Instance.AddStep(new GameManager.ObjectState() { card = roll.GetComponent<RollCard>().cards.Last().GetComponent<Cards>(), parent = transform , endroll = true});
            roll.GetComponent<RollCard>().cards.Reverse();
            cards.AddRange(roll.GetComponent<RollCard>().cards);
            roll.GetComponent<RollCard>().cards.Clear();
            GameManager.Instance.stepsCount++;
        }
    }
    private void Update()
    {
        
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].GetComponent<Cards>().isOnRoll = false;

            cards[i].GetComponent<Cards>().targetPos = transform.position;
            cards[i].GetComponent<Cards>().faceUp = false;
            cards[i].transform.SetParent(transform);
        }
    }
}
