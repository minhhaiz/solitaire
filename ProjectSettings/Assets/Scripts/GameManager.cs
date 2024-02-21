using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform dealDeck;
    public GameObject prefabs;
    public Deck deck;
    public Bottoms[] bottoms;
    public Top[] tops;
    public float timer = 0;
    public bool isStart = true;
    public List<GameObject> draggingCards = new List<GameObject>();
    public bool isRandom = false;
    public List<Cards> listCards = new List<Cards>();
    public List<Transform> cardSlots;
    public GameObject an, paneltop, panelbot, panelwin, panelPause;
    public int steps;

    public class ObjectState
    {
        public Cards card;
        public Transform parent;

    }
    [SerializeField] private List <ObjectState> Steps = new List<ObjectState>();
    private void Awake()
    {
        Time.timeScale = 1;
        Application.targetFrameRate = 144;
        Instance = this;
        if (isRandom)   Shuffle();
        Deal();
        
      foreach(Transform slot in cardSlots)
        {
            Cards card = slot.GetComponentInChildren<Cards>();  
            if (card != null)
            {
                listCards.Add(card);
            }
        }

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

    public Transform AutoMove(Cards movingCard)
    {

        Transform validSlot = null;

        // Ki?m tra t?ng v? trí trên bàn ch?i
        foreach (Transform slot in cardSlots)
        {
            // Ki?m tra n?u v? trí ?ang tr?ng và lá bài có th? di chuy?n ??n ?ó
            if (slot.childCount == 0 && CanMoveToPosition(slot, movingCard))
            {
                validSlot = slot;
                break;
            }
        }

        return validSlot;

    }

    bool CanMoveToPosition(Transform slot, Cards movingCard)
    {

        if(slot == null && movingCard.faceUp == true && movingCard.value == 1)
        {
            return true;
        }
        return true;
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
                bottoms[i].cards.Add(deck.cards[deck.cards.Count -1 ]);
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


        if (totalChild > 0)
        {
            if (last.parent.GetChild(totalChild - 1).TryGetComponent(out Cards card))
            {
                card.faceUp = false;
            }
          
        }
    }

    public void RestoreAll()
    {

        if (Steps.Count == 0) return;



        for (int i = 0; i < steps; i++)
        {
             var last = Steps[Steps.Count - i];

            int totalChild = last.parent.childCount;
            Steps.Remove(last);
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

            if (totalChild > 0)
            {
                if (last.parent.GetChild(totalChild - 1).TryGetComponent(out Cards card))
                {
                    card.faceUp = false;
                }

            }
        }
        steps = 0;


       
    }
}
