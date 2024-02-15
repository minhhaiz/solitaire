using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform dealDeck;
    public GameObject prefabs;
    public Deck deck;
    public Bottoms[] bottoms;
    public Top[] tops;
    public float timer = 0;
    public int steps = 0;
    public bool test = false;
    public bool isStart = true;
    public List<GameObject> draggingCards = new List<GameObject>();
    public bool isRandom = false;

    public GameObject an, paneltop, panelbot, panelwin, panelPause;
  
    private void Awake()
    {
        Application.targetFrameRate = 144;
        Instance = this;
        if (isRandom)   Shuffle();
        Deal();

    }
    private void Update()
    {
        timer += Time.deltaTime;
        
        if (CheckWin()||test == true)
        {
            Time.timeScale = 0f;
            an.SetActive(false);
            panelbot.SetActive(false);
            paneltop.SetActive(false);
            panelwin.SetActive(true);
            Debug.Log("win");
        }
        

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
        panelPause.SetActive(false);
        an.SetActive(true);
        panelbot.SetActive(true);
        paneltop.SetActive(true);
        panelwin.SetActive(false);

    }

   
  
    
}
