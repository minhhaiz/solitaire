using DG.Tweening;
using DG.Tweening.Core.Easing;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Cards;
using Random = UnityEngine.Random;
using Sequence = DG.Tweening.Sequence;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
    public bool isPlaying = false;
    Vector3 targetPlay;
    public List<Transform> cardSlots;
    public GameObject an, paneltop, panelbot, panelwin, panelPause, panelLose, panelPlay;
    Vector3 panelLosePos;
    public int stepsCount;
    public List<GameObject> firstDeck;
    public Process process = new Process();

    public Tween myTween = null;
    public class ObjectState
    {
        public Cards card;
        public Transform parent;
        public GameObject preCard;
        public bool prefaceup;
        public bool endroll = false;
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
        if (PlayerPrefs.GetString("Level") == "Easy")
        {
            LoadLevel();

        }
        else if (PlayerPrefs.GetString("Level") == "Random")
        {
            isRandom = true;
            if (isRandom) Shuffle();
            firstDeck = new List<GameObject>();
            firstDeck.AddRange(deck.cards);
            Debug.Log("First: " + firstDeck.Count);
            Deal();
        }
        else if (PlayerPrefs.HasKey("Process"))
        {
            LoadProcess();
            SaveProcess();
        }
        else
        {
            if (isRandom) Shuffle();
            firstDeck = new List<GameObject>();
            firstDeck.AddRange(deck.cards);
            Debug.Log("First: " + firstDeck.Count);
            Deal();
        }


        targetPlay = panelPlay.GetComponent<RectTransform>().position;
        panelPlay.transform.position = targetPlay - new Vector3(0, 3000, 0);
        SaveProcess();

    }
    public void AddStep(ObjectState state)
    {
        Steps.Add(state);
        SaveProcess();
    }

    private void Update()
    {
        if (isWon) return;
        timer += Time.deltaTime;

        if (CheckWin())
        {
            Win();
        }

    }
    public bool isWon = false;
    public void Win()
    {
        if (isWon) return;
        isWon = true;
        Time.timeScale = 0f;
        panelbot.SetActive(false);
        paneltop.SetActive(false);
        panelwin.SetActive(true);
        PlayerPrefs.DeleteKey("Process");
        Debug.Log("win");
        Debug.Log("time scale" + Time.timeScale);
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
        for (int i = 0; i < deck.cards.Count; i++)
        {
            int random = UnityEngine.Random.Range(0, deck.cards.Count);
            GameObject temp = deck.cards[random];
            deck.cards[random] = deck.cards[i];
            deck.cards[i] = temp;

        }
    }

    public void Deal()
    {
        if (deck.cards.Count == 52)
        {
            for (int i = 0; i < bottoms.Length; i++)
            {
                for (int j = 1; j <= i + 1; j++)
                {
                    bottoms[i].cards.Add(deck.cards[deck.cards.Count - 1]);
                    deck.cards.RemoveAt(deck.cards.Count - 1);
                    bottoms[i].cards[bottoms[i].cards.Count - 1].GetComponent<Cards>().faceUp = false;
                    bottoms[i].cards[bottoms[i].cards.Count - 1].transform.SetParent(bottoms[i].transform);

                }
            }
        }
        SaveProcess();
    }


public void Loadnext()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetString("Level", "Random");
        SceneManager.LoadScene("Random");
        
    }
    public void NextGame()
    {
     


        Time.timeScale = 1f;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        roll.cards = new List<GameObject>();
        tops[0].cards = new List<GameObject>();
        tops[1].cards = new List<GameObject>();
        tops[2].cards = new List<GameObject>();
        tops[3].cards = new List<GameObject>();
        bottoms[0].cards = new List<GameObject>();
        bottoms[1].cards = new List<GameObject>();
        bottoms[2].cards = new List<GameObject>();
        bottoms[3].cards = new List<GameObject>();
        bottoms[4].cards = new List<GameObject>();
        bottoms[5].cards = new List<GameObject>();
        bottoms[6].cards = new List<GameObject>();
        deck.cards = new List<GameObject>();
        deck.outline.SetActive(false);

        Steps = new List<ObjectState>();
        isWon = false;
        deck.cards.AddRange(firstDeck);
        Shuffle();

        firstDeck = new List<GameObject>();
        firstDeck.AddRange(deck.cards);
        Debug.Log("next");
       

        StartCoroutine(DelayContinue());
        panelPlay.GetComponent<RectTransform>().DOMove(targetPlay - new Vector3(0, 3000, 0), 0.4f);
    }


    public bool CheckWin()
    {
        foreach (Top top in tops)
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
        panelPlay.GetComponent<RectTransform>().DOMove(targetPlay - new Vector3(0, 3000, 0), 0.4f);

    }


    public void RestoreState()
    {
        if (Steps.Count == 0) return;

        var last = Steps[Steps.Count - 1];

        Steps.Remove(last);


        int totalChild = last.parent.childCount;



        if (last.endroll)
        {
            deck.cards.Reverse();
            roll.cards.AddRange(deck.cards);
            deck.cards = new List<GameObject>();
            stepsCount++;
            return;
        }

        List<GameObject> temp = new List<GameObject>();

        switch (last.card.transform.parent.tag)
        {

            case "Bot":
                int index = 0;
                index = last.card.transform.parent.GetComponent<Bottoms>().cards.IndexOf(last.card.gameObject);
                if (index == -1)
                {

                    index = 0;
                }

                temp.AddRange(last.card.transform.parent.GetComponent<Bottoms>().cards.GetRange(index, last.card.transform.parent.GetComponent<Bottoms>().cards.Count - index));
                last.card.transform.parent.GetComponent<Bottoms>().cards.RemoveRange(index, last.card.transform.parent.GetComponent<Bottoms>().cards.Count - index);
                break;
            case "Top":
                temp.Add(last.card.gameObject);
                last.card.transform.parent.GetComponent<Top>().cards.RemoveAt(last.card.transform.parent.GetComponent<Top>().cards.Count - 1);
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
                    Debug.Log(last.prefaceup);
                    last.parent.GetComponent<Bottoms>().cards[last.parent.GetComponent<Bottoms>().cards.Count - 1].GetComponent<Cards>().faceUp = last.prefaceup;
                }
                last.card.transform.parent.GetComponent<Bottoms>().cards.AddRange(temp);
                Debug.Log(last.preCard);
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
        stepsCount++;


        Debug.Log(totalChild + " test");



    }
    public void RestoreAll()
    {
        isWon = false;

        if (myTween != null)
        {
            myTween.Kill();
        }
        roll.cards = new List<GameObject>();
        tops[0].cards = new List<GameObject>();
        tops[1].cards = new List<GameObject>();
        tops[2].cards = new List<GameObject>();
        tops[3].cards = new List<GameObject>();
        tops[0].value = 0;
        tops[1].value = 0;
        tops[2].value = 0;
        tops[3].value = 0;
        Time.timeScale = 1;
        bottoms[0].cards = new List<GameObject>();
        bottoms[1].cards = new List<GameObject>();
        bottoms[2].cards = new List<GameObject>();
        bottoms[3].cards = new List<GameObject>();
        bottoms[4].cards = new List<GameObject>();
        bottoms[5].cards = new List<GameObject>();
        bottoms[6].cards = new List<GameObject>();
        deck.cards = new List<GameObject>();
        deck.outline.SetActive(false);
        Steps = new List<ObjectState>();
        isWon = false;
        deck.cards.AddRange(firstDeck);
        Debug.Log("restart");


        StartCoroutine(DelayContinue());
        panelPlay.GetComponent<RectTransform>().DOMove(targetPlay - new Vector3(0, 3000, 0), 0.4f);

    }

    private IEnumerator DelayContinue()
    {
        yield return new WaitForSeconds(0.2f);
        Debug.Log("Cut");
        Continue();
        timer = 0;
        stepsCount = 0;
        Deal();
    }

    public void SaveProcess()
    {
        process.bot0 = new List<string>();
        process.bot1 = new List<string>();
        process.bot2 = new List<string>();
        process.bot3 = new List<string>();
        process.bot4 = new List<string>();
        process.bot5 = new List<string>();
        process.bot6 = new List<string>();
        process.top0 = new List<string>();
        process.top1 = new List<string>();
        process.top2 = new List<string>();
        process.top3 = new List<string>();
        process.deck = new List<string>();
        process.roll = new List<string>();
        process.bot0f = new List<bool>();
        process.bot1f = new List<bool>();
        process.bot2f = new List<bool>();
        process.bot3f = new List<bool>();
        process.bot4f = new List<bool>();
        process.bot5f = new List<bool>();
        process.bot6f = new List<bool>();
        process.firstdeck = new List<string>();

        foreach(GameObject card in firstDeck)
        {
            process.firstdeck.Add(card.name);
        }
        foreach(GameObject card in bottoms[0].GetComponent<Bottoms>().cards)
        {
            process.bot0.Add(card.name);
            if (card.GetComponent<Cards>().faceUp)
            {
                process.bot0f.Add(true);
            }
            else
            {
                process.bot0f.Add(false);
            }
        }
        foreach (GameObject card in bottoms[1].GetComponent<Bottoms>().cards)
        {
            process.bot1.Add(card.name);
            if (card.GetComponent<Cards>().faceUp)
            {
                process.bot1f.Add(true);
            }
            else
            {
                process.bot1f.Add(false);
            }
        }
        foreach (GameObject card in bottoms[2].GetComponent<Bottoms>().cards)
        {
            process.bot2.Add(card.name);
            if (card.GetComponent<Cards>().faceUp)
            {
                process.bot2f.Add(true);
            }
            else
            {
                process.bot2f.Add(false);
            }
        }
        foreach (GameObject card in bottoms[3].GetComponent<Bottoms>().cards)
        {
            process.bot3.Add(card.name);
            if (card.GetComponent<Cards>().faceUp)
            {
                process.bot3f.Add(true);
            }
            else
            {
                process.bot3f.Add(false);
            }
        }
        foreach (GameObject card in bottoms[4].GetComponent<Bottoms>().cards)
        {
            process.bot4.Add(card.name);
            if (card.GetComponent<Cards>().faceUp)
            {
                process.bot4f.Add(true);
            }
            else
            {
                process.bot4f.Add(false);
            }
        }
        foreach (GameObject card in bottoms[5].GetComponent<Bottoms>().cards)
        {
            process.bot5.Add(card.name);
            if (card.GetComponent<Cards>().faceUp)
            {
                process.bot5f.Add(true);
            }
            else
            {
                process.bot5f.Add(false);
            }
        }
        foreach (GameObject card in bottoms[6].GetComponent<Bottoms>().cards)
        {
            process.bot6.Add(card.name);
            if (card.GetComponent<Cards>().faceUp)
            {
                process.bot6f.Add(true);
            }
            else
            {
                process.bot6f.Add(false);
            }
        }
        foreach ( GameObject card in tops[0].GetComponent<Top>().cards)
        {
            process.top0.Add(card.name);
        }
        foreach (GameObject card in tops[1].GetComponent<Top>().cards)
        {
            process.top1.Add(card.name);
        }
        foreach (GameObject card in tops[2].GetComponent<Top>().cards)
        {
            process.top2.Add(card.name);
        }
        foreach (GameObject card in tops[3].GetComponent<Top>().cards)
        {
            process.top3.Add(card.name);
        }
       
        foreach (GameObject deck in deck.GetComponent<Deck>().cards)
        {
            process.deck.Add(deck.name);
        }
        foreach (GameObject card in roll.GetComponent<RollCard>().cards)
        {
            process.roll.Add(card.name);
        }

        string json = JsonConvert.SerializeObject(process);
        Debug.Log(json);
        PlayerPrefs.SetString("Process", json);
    }
    public void SaveLevel()
    {
        process.bot0 = new List<string>();
        process.bot1 = new List<string>();
        process.bot2 = new List<string>();
        process.bot3 = new List<string>();
        process.bot4 = new List<string>();
        process.bot5 = new List<string>();
        process.bot6 = new List<string>();
        process.top0 = new List<string>();
        process.top1 = new List<string>();
        process.top2 = new List<string>();
        process.top3 = new List<string>();
        process.deck = new List<string>();
        process.roll = new List<string>();
        process.bot0f = new List<bool>();
        process.bot1f = new List<bool>();
        process.bot2f = new List<bool>();
        process.bot3f = new List<bool>();
        process.bot4f = new List<bool>();
        process.bot5f = new List<bool>();
        process.bot6f = new List<bool>();
        process.firstdeck = new List<string>();

        foreach (GameObject card in bottoms[0].GetComponent<Bottoms>().cards)
        {
            process.bot0.Add(card.name);
            if (card.GetComponent<Cards>().faceUp)
            {
                process.bot0f.Add(true);
            }
            else
            {
                process.bot0f.Add(false);
            }
        }
        foreach (GameObject card in bottoms[1].GetComponent<Bottoms>().cards)
        {
            process.bot1.Add(card.name);
            if (card.GetComponent<Cards>().faceUp)
            {
                process.bot1f.Add(true);
            }
            else
            {
                process.bot1f.Add(false);
            }
        }
        foreach (GameObject card in bottoms[2].GetComponent<Bottoms>().cards)
        {
            process.bot2.Add(card.name);
            if (card.GetComponent<Cards>().faceUp)
            {
                process.bot2f.Add(true);
            }
            else
            {
                process.bot2f.Add(false);
            }
        }
        foreach (GameObject card in bottoms[3].GetComponent<Bottoms>().cards)
        {
            process.bot3.Add(card.name);
            if (card.GetComponent<Cards>().faceUp)
            {
                process.bot3f.Add(true);
            }
            else
            {
                process.bot3f.Add(false);
            }
        }
        foreach (GameObject card in bottoms[4].GetComponent<Bottoms>().cards)
        {
            process.bot4.Add(card.name);
            if (card.GetComponent<Cards>().faceUp)
            {
                process.bot4f.Add(true);
            }
            else
            {
                process.bot4f.Add(false);
            }
        }
        foreach (GameObject card in bottoms[5].GetComponent<Bottoms>().cards)
        {
            process.bot5.Add(card.name);
            if (card.GetComponent<Cards>().faceUp)
            {
                process.bot5f.Add(true);
            }
            else
            {
                process.bot5f.Add(false);
            }
        }
        foreach (GameObject card in bottoms[6].GetComponent<Bottoms>().cards)
        {
            process.bot6.Add(card.name);
            if (card.GetComponent<Cards>().faceUp)
            {
                process.bot6f.Add(true);
            }
            else
            {
                process.bot6f.Add(false);
            }
        }
        foreach (GameObject card in tops[0].GetComponent<Top>().cards)
        {
            process.top0.Add(card.name);
        }
        foreach (GameObject card in tops[1].GetComponent<Top>().cards)
        {
            process.top1.Add(card.name);
        }
        foreach (GameObject card in tops[2].GetComponent<Top>().cards)
        {
            process.top2.Add(card.name);
        }
        foreach (GameObject card in tops[3].GetComponent<Top>().cards)
        {
            process.top3.Add(card.name);
        }

        foreach (GameObject deck in deck.GetComponent<Deck>().cards)
        {
            process.deck.Add(deck.name);
        }
        foreach (GameObject card in roll.GetComponent<RollCard>().cards)
        {
            process.roll.Add(card.name);
        }
        foreach (GameObject card in firstDeck)
        {
            process.firstdeck.Add(card.name);
        }
        string filename;
        int count = 1;
        while (File.Exists("Assets/Resources/Level" + count + ".json"))
        {
            count++;
        }
        filename = "Level" + count + ".json";
        string json = JsonConvert.SerializeObject(process);
        string path = Path.Combine("Assets", "Resources", filename);
        File.WriteAllText(path, json);        
    }
    public void LoadLevel()
    {
        int randomNumber = Random.Range(1,10);

        string json = Resources.Load<TextAsset>("Level" + randomNumber.ToString()).text;

        process = JsonConvert.DeserializeObject<Process>(json);
        roll.cards = new List<GameObject>();
        tops[0].cards = new List<GameObject>();
        tops[1].cards = new List<GameObject>();
        tops[2].cards = new List<GameObject>();
        tops[3].cards = new List<GameObject>();
        bottoms[0].cards = new List<GameObject>();
        bottoms[1].cards = new List<GameObject>();
        bottoms[2].cards = new List<GameObject>();
        bottoms[3].cards = new List<GameObject>();
        bottoms[4].cards = new List<GameObject>();
        bottoms[5].cards = new List<GameObject>();
        bottoms[6].cards = new List<GameObject>();
        deck.cards = new List<GameObject>();
        for (int i = 0; i < process.bot0.Count; i++)
        {
            bottoms[0].cards.Add(GameObject.Find(process.bot0[i]));
            bottoms[0].cards.Last().GetComponent<Cards>().faceUp = process.bot0f[i];
            Debug.Log("cardgace" + process.bot0f[i]);
        }
        for (int i = 0; i < process.bot1.Count; i++)
        {
            bottoms[1].cards.Add(GameObject.Find(process.bot1[i]));
            bottoms[1].cards.Last().GetComponent<Cards>().faceUp = process.bot1f[i];
        }
        for (int i = 0; i < process.bot2.Count; i++)
        {
            bottoms[2].cards.Add(GameObject.Find(process.bot2[i]));
            bottoms[2].cards.Last().GetComponent<Cards>().faceUp = process.bot2f[i];
        }
        for (int i = 0; i < process.bot3.Count; i++)
        {
            bottoms[3].cards.Add(GameObject.Find(process.bot3[i]));
            bottoms[3].cards.Last().GetComponent<Cards>().faceUp = process.bot3f[i];
        }
        for (int i = 0; i < process.bot4.Count; i++)
        {
            bottoms[4].cards.Add(GameObject.Find(process.bot4[i]));
            bottoms[4].cards.Last().GetComponent<Cards>().faceUp = process.bot4f[i];
        }
        for (int i = 0; i < process.bot5.Count; i++)
        {
            bottoms[5].cards.Add(GameObject.Find(process.bot5[i]));
            bottoms[5].cards.Last().GetComponent<Cards>().faceUp = process.bot5f[i];
        }
        for (int i = 0; i < process.bot6.Count; i++)
        {
            bottoms[6].cards.Add(GameObject.Find(process.bot6[i]));
            bottoms[6].cards.Last().GetComponent<Cards>().faceUp = process.bot6f[i];
        }

        foreach(string card in process.firstdeck)
        {
            firstDeck.Add(GameObject.Find(card));
        }
        foreach (string card in process.top0)
        {
            tops[0].cards.Add(GameObject.Find(card));
        }
        foreach (string card in process.top1)
        {
            tops[1].cards.Add(GameObject.Find(card));
        }
        foreach (string card in process.top2)
        {
            tops[2].cards.Add(GameObject.Find(card));
        }
        foreach (string card in process.top3)
        {
            tops[3].cards.Add(GameObject.Find(card));
        }
        foreach (string card in process.deck)
        {
            deck.cards.Add(GameObject.Find(card));
        }
        foreach (string card in process.roll)
        {
            roll.cards.Add(GameObject.Find(card));
        }
    }
    public void LoadProcess()
    {
        string json = PlayerPrefs.GetString("Process");
        process = JsonConvert.DeserializeObject<Process>(json);
        roll.cards = new List<GameObject>();
        tops[0].cards = new List<GameObject>();
        tops[1].cards = new List<GameObject>();
        tops[2].cards = new List<GameObject>();
        tops[3].cards = new List<GameObject>();
        bottoms[0].cards = new List<GameObject>();
        bottoms[1].cards = new List<GameObject>();
        bottoms[2].cards = new List<GameObject>();
        bottoms[3].cards = new List<GameObject>();
        bottoms[4].cards = new List<GameObject>();
        bottoms[5].cards = new List<GameObject>();
        bottoms[6].cards = new List<GameObject>();
        deck.cards = new List<GameObject>();
        firstDeck = new List<GameObject>();
        for (int i = 0; i < process.bot0.Count; i++ ) 
        {
            bottoms[0].cards.Add(GameObject.Find(process.bot0[i]));
            bottoms[0].cards.Last().GetComponent<Cards>().faceUp = process.bot0f[i];
            Debug.Log("cardgace" + process.bot0f[i]);
        }
        for (int i = 0; i < process.bot1.Count; i++)
        {
            bottoms[1].cards.Add(GameObject.Find(process.bot1[i]));
            bottoms[1].cards.Last().GetComponent<Cards>().faceUp = process.bot1f[i];
        }
        for (int i = 0; i < process.bot2.Count; i++)
        {
            bottoms[2].cards.Add(GameObject.Find(process.bot2[i]));
            bottoms[2].cards.Last().GetComponent<Cards>().faceUp = process.bot2f[i];
        }
        for (int i = 0; i < process.bot3.Count; i++)
        {
            bottoms[3].cards.Add(GameObject.Find(process.bot3[i]));
            bottoms[3].cards.Last().GetComponent<Cards>().faceUp = process.bot3f[i];
        }
        for (int i = 0; i < process.bot4.Count; i++)
        {
            bottoms[4].cards.Add(GameObject.Find(process.bot4[i]));
            bottoms[4].cards.Last().GetComponent<Cards>().faceUp = process.bot4f[i];
        }
        for (int i = 0; i < process.bot5.Count; i++)
        {
            bottoms[5].cards.Add(GameObject.Find(process.bot5[i]));
            bottoms[5].cards.Last().GetComponent<Cards>().faceUp = process.bot5f[i];
        }
        for (int i = 0; i < process.bot6.Count; i++)
        {
            bottoms[6].cards.Add(GameObject.Find(process.bot6[i]));
            bottoms[6].cards.Last().GetComponent<Cards>().faceUp = process.bot6f[i];
        }

        foreach (string card in process.firstdeck)
        {
            firstDeck.Add(GameObject.Find(card));
        }
        foreach (string card in process.top0)
        {
            tops[0].cards.Add(GameObject.Find(card));
        }
        foreach (string card in process.top1)
        {
            tops[1].cards.Add(GameObject.Find(card));
        }
        foreach (string card in process.top2)
        {
            tops[2].cards.Add(GameObject.Find(card));
        }
        foreach (string card in process.top3)
        {
            tops[3].cards.Add(GameObject.Find(card));
        }
        foreach (string card in process.deck)
        {
           deck.cards.Add(GameObject.Find(card));
        }
        foreach (string card in process.roll)
        {
            roll.cards.Add(GameObject.Find(card));
        }


    }

 
    Sequence blinkSq;
    public void Blink()
    {
        if (blinkSq == null)
        {
            deck.outline.SetActive(true);
            blinkSq = DOTween.Sequence();
            blinkSq.Append(deck.outline.GetComponent<SpriteRenderer>().DOFade(0.4f, 0.7f));
            //blinkSq.AppendInterval(0.4f);
            blinkSq.Append(deck.outline.GetComponent<SpriteRenderer>().DOFade(1, 0.7f));
            //blinkSq.AppendInterval(0.4f);
            blinkSq.SetLoops(-1);
        }
    }
    public void StopBlink()
    {
        if (blinkSq != null)
        {
            blinkSq.Append(deck.outline.GetComponent<SpriteRenderer>().DOFade(0, 0));
            blinkSq.Kill();
            blinkSq = null;
            deck.outline.SetActive(false);
        }
    }

    public void Hint()
    {
        myTween.Kill(this);
        GameObject card = FindCardHint();
        if (card == null)
        {
            panelLose.GetComponent<RectTransform>().DOMove(panelLosePos, 0.4f).OnComplete(MoveLosePanelUp);
            return;
        }
        if (card == deck.gameObject)
        {
            deck.outline.SetActive(true);

            Debug.Log("CCHINT");
            Blink();

        }
        else
        {
            GameObject hintTarget = card.GetComponent<Cards>().TargetMove();
            cardhint.GetComponent<CardHint>().value = card.GetComponent<Cards>().value;
            cardhint.GetComponent<CardHint>().suit = card.GetComponent<Cards>().suit;

            cardhint.transform.position = card.transform.position + Vector3.back * 4;
            cardhint.SetActive(true);
            myTween = cardhint.transform.DOMove(hintTarget.transform.position, 1.1f).OnComplete(EndHint);

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

    public bool check = false;
    public bool CardFly()
    {
        Debug.Log("RUN");
        StartCoroutine(DelayFly());
        if (check) {
            Debug.Log(check.ToString());
        }
        return check;
    }

    private IEnumerator DelayFly()
    {
        foreach (Bottoms bot in bottoms)
        {
            if (bot.cards.Count > 0)
            {
                Cards lastCard = bot.cards[bot.cards.Count - 1].GetComponent<Cards>();

                foreach (Top top in tops)
                {
                    if (lastCard.value == 1 && top.value == 0)
                    {
                        yield return new WaitForSeconds(0.2f);
                        lastCard.AddPrecard();
                        top.cards.Add(lastCard.gameObject);

                        bot.cards.Remove(lastCard.gameObject);
                        top.value = lastCard.value;
                        top.suit = lastCard.suit;
                        //GameManager.Instance.steps++;
                        GameManager.Instance.AddStep(new GameManager.ObjectState() { card = lastCard, parent = lastCard.transform.parent, preCard = lastCard.GetComponent<Cards>().precard, prefaceup = lastCard.GetComponent<Cards>().preFaceUp });
                        check = true;

                        break;
                    }
                    else if (lastCard.value == top.value + 1 && lastCard.suit == top.suit)
                    {
                        yield return new WaitForSeconds(0.2f);
                        lastCard.AddPrecard();
                        top.cards.Add(lastCard.gameObject);

                        bot.cards.Remove(lastCard.gameObject);
                        top.value = lastCard.value;
                        top.suit = lastCard.suit;
                        //GameManager.Instance.steps++;
                        GameManager.Instance.AddStep(new GameManager.ObjectState() { card = lastCard, parent = lastCard.transform.parent, preCard = lastCard.GetComponent<Cards>().precard, prefaceup = lastCard.GetComponent<Cards>().preFaceUp });
                        check = true;


                        break;
                    }
                }
            }
            if (roll.cards.Count > 0)
            {
                Cards lastCard = roll.cards[roll.cards.Count - 1].GetComponent<Cards>();
                foreach (Top top in tops)
                {
                    if (lastCard.value == 1 && top.value == 0)
                    {
                        yield return new WaitForSeconds(0.2f);
                        lastCard.AddPrecard();
                        top.cards.Add(lastCard.gameObject);


                        roll.cards.Remove(lastCard.gameObject);
                        top.value = lastCard.value;
                        top.suit = lastCard.suit;
                        //GameManager.Instance.steps++;
                        GameManager.Instance.AddStep(new GameManager.ObjectState() { card = lastCard, parent = lastCard.transform.parent, preCard = lastCard.GetComponent<Cards>().precard, prefaceup = lastCard.GetComponent<Cards>().preFaceUp });
                        check = true;


                        break;
                    }
                    else if (lastCard.value == top.value + 1 && lastCard.suit == top.suit)
                    {
                        yield return new WaitForSeconds(0.2f);
                        lastCard.AddPrecard();
                        top.cards.Add(lastCard.gameObject);

                        roll.cards.Remove(lastCard.gameObject);
                        top.value = lastCard.value;
                        top.suit = lastCard.suit;
                        //GameManager.Instance.steps++;

                        GameManager.Instance.AddStep(new GameManager.ObjectState() { card = lastCard, parent = lastCard.transform.parent, preCard = lastCard.GetComponent<Cards>().precard, prefaceup = lastCard.GetComponent<Cards>().preFaceUp });

                        check = true;


                        break;
                    }
                }
            }
            else yield return null;

        }
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("menu");
        panelPlay.GetComponent<RectTransform>().DOMove(targetPlay - new Vector3(0, 3000, 0), 0.4f);
    }

    public void PlayOP()
    {
       
        
        if (isPlaying)
        {
            isPlaying = true;
            panelPlay.GetComponent<RectTransform>().DOMove(targetPlay - new Vector3(0,  3000, 0), 0.4f);
        }
        else
        {
            isPlaying = false;
            panelPlay.GetComponent<RectTransform>().DOMove(targetPlay, 0.4f);
            
            paneltop.SetActive(false);
           an.SetActive(false);
        }
    }
}
