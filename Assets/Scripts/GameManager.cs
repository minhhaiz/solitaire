using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static Cards;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform dealDeck;
    public GameObject prefabs;
    public GameObject cardhint;
    public Deck deck;
    public Bottoms[] bottoms;
    public Top[] tops;
    public RollCard roll;
    public float timer = 0;
    public bool isStart = true;
    public List<GameObject> draggingCards = new List<GameObject>();
    public bool isRandom = false;
    
    public List<Transform> cardSlots;
    public GameObject an, paneltop, panelbot, panelwin, panelPause, panelLose;
    Vector3 panelLosePos;
    public int steps;
    

    public class ObjectState
    {
        public Cards card;
        public Transform parent;
        public GameObject preCard;

    }
    [SerializeField] private List<ObjectState> Steps = new List<ObjectState>();
    private void Awake()
    {
        Time.timeScale = 1;
        Application.targetFrameRate = 144;
        Instance = this;
        panelLosePos = panelLose.GetComponent<RectTransform>().position;
        panelLose.GetComponent<RectTransform>().position += Vector3.up * 10000;
        panelLose.SetActive(true);
        if (isRandom) Shuffle();
        Deal();

        

    }
    public void AddStep(ObjectState state)
    {
        Steps.Add(state);

    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (CheckWin())
        {
            Win();
        }


    }

    public void Win()
    {
        Time.timeScale = 0f;
        an.SetActive(false);
        panelbot.SetActive(false);
        paneltop.SetActive(false);
        panelwin.SetActive(true);
        Debug.Log("win");
    }
    

   

    /*    private void SolitaireCreate()
   {
       if (isRandom)
       {
           for (int i = 0; i < 52; i++)
           {
               GameObject newCard = Instantiate(prefabs, dealDeck);
               newCard.GetComponent<Cards>().value = i % 13 + 1;
               switch (i / 13)
               {
                   case 0:
                       newCard.GetComponent<Cards>().suit = Cards.Suit.S;
                       newCard.gameObject.name = (i % 13 + 1) + "S";
                       break;
                   case 1:
                       newCard.GetComponent<Cards>().suit = Cards.Suit.C;
                       newCard.gameObject.name = (i % 13 + 1) + "C"; break;
                   case 2:
                       newCard.GetComponent<Cards>().suit = Cards.Suit.D;
                       newCard.gameObject.name = (i % 13 + 1) + "D"; break;
                   case 3:
                       newCard.GetComponent<Cards>().suit = Cards.Suit.H;
                       newCard.gameObject.name = (i % 13 + 1) + "H"; break;
               }

               newCard.GetComponent<Cards>().faceUp = false;
               newCard.transform.position = dealDeck.transform.position + new Vector3(0, 0, -0.001f) * i;
               deck.cards.Add(newCard);
           }
       }

   }*/

    public void Shuffle()
    {
        for(int i = 0;i<deck.cards.Count;i++)
        {
            int random = Random.Range(0,deck.cards.Count);
            GameObject temp = deck.cards[random];
            deck.cards[random] = deck.cards[i];
            deck.cards[i] = temp;

        }
    }

    public void  Deal()
    {
        for (int i = 0; i<bottoms.Length;i++)
        {
            for( int j = 1;j <= i+1; j++)
            {
                bottoms[i].cards.Add(deck.cards[deck.cards.Count - 1 ]);
                deck.cards.RemoveAt(deck.cards.Count - 1);
                bottoms[i].cards[bottoms[i].cards.Count - 1].GetComponent<Cards>().faceUp = false;
                bottoms[i].cards[bottoms[i].cards.Count - 1].transform.SetParent(bottoms[i].transform);
                
            }
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
   

    public bool CheckWin()
    {
        foreach(Top top in tops)
        {
            if (top.value < 13)
            {
                return false;
            }
        }
        return true;
    }
    public void Pause()
    {
        an.SetActive(false);
        panelbot.SetActive(false);
        paneltop.SetActive(false);
        panelwin.SetActive(false);
        panelPause.SetActive(true);
    }
    public void Continue()
    {
       
        an.SetActive(true);
        panelbot.SetActive(true);
        paneltop.SetActive(true);
        panelwin.SetActive(false);
        panelPause.SetActive(false);

    }

    public void RestoreState()
    {
        if (Steps.Count == 0) return;
        var last = Steps[Steps.Count - 1];

        Steps.Remove(last);

        int totalChild = last.parent.childCount;



        List<GameObject> temp = new List<GameObject>();
        switch (last.card.transform.parent.tag)
        {

            case "Bot":
                int index = 0;
                index = last.card.transform.parent.GetComponent<Bottoms>().cards.IndexOf(last.card.gameObject);
                temp.AddRange(last.card.transform.parent.GetComponent<Bottoms>().cards.GetRange(index, last.card.transform.parent.GetComponent<Bottoms>().cards.Count - index));
                last.card.transform.parent.GetComponent<Bottoms>().cards.RemoveRange(index, last.card.transform.parent.GetComponent<Bottoms>().cards.Count - index);
                break;
            case "Top":
                temp.Add(last.card.gameObject);
                last.card.transform.parent.GetComponent<Top>().cards.Remove(last.card.gameObject);
                break;
            case "Deck":
                temp.Add(last.card.gameObject);
                last.card.transform.parent.GetComponent<Deck>().cards.Remove(last.card.gameObject);
                break;
            case "Roll":
                temp.Add(last.card.gameObject);
                last.card.transform.parent.GetComponent<RollCard>().cards.Remove(last.card.gameObject);
                break;
        }

        last.card.transform.SetParent(last.parent);
        switch (last.card.transform.parent.tag)
        {
            case "Bot":
                if (last.preCard != null)
                {
                    last.card.transform.parent.GetComponent<Bottoms>().cards.Last().GetComponent<Cards>().faceUp = last.preCard.GetComponent<Cards>().faceUp;
                }
                else
                last.card.transform.parent.GetComponent<Bottoms>().cards.AddRange(temp);
                break;
            case "Top":
                last.card.transform.parent.GetComponent<Top>().cards.AddRange(temp);
                break;
            case "Deck":
                last.card.transform.parent.GetComponent<Deck>().cards.AddRange(temp);
                break;
            case "Roll":
                last.card.transform.parent.GetComponent<RollCard>().cards.AddRange(temp);
                break;
        }

        steps--;


        if (totalChild > 0)
        {
            if (last.parent.GetChild(totalChild - 1).TryGetComponent(out Cards card))
            {
                card.faceUp = false;
            }
            else
                card.faceUp = true;
        }

        }

    public void RestoreAll()
    {
        do
        {
            SupportAll();
            steps = 0;
            timer = 0;
           
        } while (Steps.Count > 0);
       
        Continue();

    }

    private void SupportAll()
    {
        if (Steps.Count == 0) return;
        var last = Steps[Steps.Count - 1];

        Steps.Remove(last);

        int totalChild = last.parent.childCount;




        switch (last.card.transform.parent.tag)
        {

            case "Bot":
                last.card.transform.parent.GetComponent<Bottoms>().cards.Remove(last.card.gameObject);
                break;
            case "Top":
                last.card.transform.parent.GetComponent<Top>().cards.Remove(last.card.gameObject);
                break;
            case "Deck":
                last.card.transform.parent.GetComponent<Deck>().cards.Remove(last.card.gameObject);
                break;
            case "Roll":
                last.card.transform.parent.GetComponent<RollCard>().cards.Remove(last.card.gameObject);
                break;
        }

        last.card.transform.SetParent(last.parent);
        switch (last.card.transform.parent.tag)
        {
            case "Bot":
                last.card.transform.parent.GetComponent<Bottoms>().cards.Add(last.card.gameObject);
                break;
            case "Top":
                last.card.transform.parent.GetComponent<Top>().cards.Add(last.card.gameObject);
                break;
            case "Deck":
                last.card.transform.parent.GetComponent<Deck>().cards.Add(last.card.gameObject);
                break;
            case "Roll":
                last.card.transform.parent.GetComponent<RollCard>().cards.Add(last.card.gameObject);
                break;
        }

        steps--;
        roll.cards.Reverse();
        deck.cards.AddRange(roll.cards);
        roll.cards = new List<GameObject>();
        if (totalChild > 0)
        {
            if (last.parent.GetChild(totalChild - 1).TryGetComponent(out Cards card))
            {
                card.faceUp = false;
            }
            else
                card.faceUp = true;

        }
    }

    public void Hint()
    {
        GameObject card = FindCardHint();
        if (card == null)
        {
            panelLose.GetComponent<RectTransform>().DOMove(panelLosePos, 0.7f).OnComplete(MoveLosePanelUp);
            return;
        }
        if (card == deck.gameObject)
        {
            Debug.Log("CCHINT");
            deck.outline.SetActive(true);
        } else
        {
            GameObject hintTarget = card.GetComponent<Cards>().TargetMove();
            cardhint.GetComponent<CardHint>().value = card.GetComponent<Cards>().value;
            cardhint.GetComponent<CardHint>().suit = card.GetComponent<Cards>().suit;

            cardhint.transform.position = card.transform.position + Vector3.back * 4;
            cardhint.SetActive(true);
            cardhint.transform.DOMove(hintTarget.transform.position, 1.3f).OnComplete(EndHint);

        }
    }
    private void MoveLosePanelUp()
    {
        panelLose.GetComponent<RectTransform>().DOMove(panelLosePos + Vector3.up * 10000, 1f).SetDelay(1f);
    }

    private void EndHint()
    {
        cardhint.SetActive(false);
    }



    public GameObject FindCardHint()
    {
        foreach (Bottoms bot in bottoms)
        {
            foreach (GameObject cards in bot.cards)
            {
                if (cards.GetComponent<Cards>().CanMoveHint() && cards.GetComponent<Cards>().faceUp)
                {
                    return cards;

                }
            }
        }

        if (roll.cards.Count > 0 && roll.cards.Last().GetComponent<Cards>().CanMoveHint())
        {
             return roll.cards.Last();

        }
        else if (roll.cards.Count > 0)
        {
            for (int i = 0; i < roll.cards.Count - 1; i++)
            {
                if (roll.cards[i].GetComponent<Cards>().CanMoveHint())
                {
                    return deck.gameObject;
                }
            }
        }
        for (int i = 0; i < deck.cards.Count; i++)
        {
            if (deck.cards[i].GetComponent<Cards>().CanMoveHint())
            {
                return deck.gameObject;
            }
        }
        //foreach (Top topcon in tops)
        //{
        //        if (topcon.cards.Count > 0 && topcon.cards.Last().GetComponent<Cards>().CanMoveHint())
        //        {
        //            return topcon.cards.Last();

        //        }
        //}

        return null;
    }

    public bool CheckLose()
    {
        if (FindCardHint() == null && deck.cards.Count == 0) return true;
        return false;
    }
}
