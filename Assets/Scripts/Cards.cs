using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Cards : MonoBehaviour
{
    public GameObject back;
    public GameObject face;
    public GameObject outline;
    public Sprite[] faceSprite;
    public BoxCollider2D boxCollider;
    public bool faceUp = false;
 
    public bool isOntop = false;
    public bool isOnbottom = false;
    public bool isOnRoll = false;
    public Collider2D lastCollision;
    
   
    public Vector3 targetPos;
    public int value;
    public float clickTime = 0;
    public enum Suit
    {
        S,
        C,
        D,
        H
    }
    public Suit suit;

    public bool isDragging = false;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        targetPos = transform.position;
        UpdateSprite();
    }
 

    private void UpdateSprite()
    {
        switch (suit)
        {
            case Suit.S:
                face.GetComponent<SpriteRenderer>().sprite = faceSprite[value - 1];
                break;
            case Suit.C:
                face.GetComponent<SpriteRenderer>().sprite = faceSprite[value - 1 + 13];
                break;
            case Suit.D:
                face.GetComponent<SpriteRenderer>().sprite = faceSprite[value - 1 + 26];
                break;
            case Suit.H:
                face.GetComponent<SpriteRenderer>().sprite = faceSprite[value - 1 + 39];
                break;
        }
    }
    private void FixedUpdate()
    {
        clickTime += Time.fixedDeltaTime;
    }
    private void Update()
    {
       
        if (!isOnRoll)
        {
            if (!faceUp)
            {
                transform.DORotate(new Vector3(0, 180, 0), 0.3f);
                boxCollider.enabled = false;
            }      
             else
            {
                transform.DORotate(new Vector3(0, 0, 0), 0.3f);
                boxCollider.enabled = true;
            }
        }
        outline.SetActive(isDragging);
        if (isDragging)
        { 
            if (transform.GetComponentInParent<Bottoms>() != null)
            {
                if (GameManager.Instance.draggingCards.Count > 0)
                {
                    foreach (GameObject card in GameManager.Instance.draggingCards)
                    {
                        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        
                        mousepos.z = -5 - (0.01f * GameManager.Instance.draggingCards.IndexOf(card));
                        mousepos += GetComponentInParent<Bottoms>().offSet * GameManager.Instance.draggingCards.IndexOf(card);
                        if (card.GetComponent<Cards>().isDragging)
                        {
                            card.transform.position = mousepos;
                            

                        }
                        else
                        {
                            card.GetComponent<Cards>().isDragging = true;
                            card.transform.position = mousepos;
                            
                        }
                    }
                }
                
            }
            else { 
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePosition.z = -5;
                    transform.position = mousePosition;
            }


        }


    }
    public void OnMouseDown()
    {
        isDragging = true;
        if (transform.GetComponentInParent<Bottoms>() != null)
        {
            int indexInBot = transform.GetComponentInParent<Bottoms>().cards.IndexOf(gameObject);
            for (int i = indexInBot; i < transform.GetComponentInParent<Bottoms>().cards.Count; i++)
            {
                GameManager.Instance.draggingCards.Add(transform.GetComponentInParent<Bottoms>().cards[i]);
            }
        }
        clickTime = 0;

    }
    
    public void OnMouseUp()
    {
        if (clickTime <=0.2f)
        {
            AutoMove();
            foreach (GameObject card in GameManager.Instance.draggingCards)
            {
                card.GetComponent<Cards>().isDragging = false;
            }
        }
        else
        {
            if (isOntop && GameManager.Instance.draggingCards.Count < 2)
            {
                if (value == 1 && lastCollision.GetComponent<Top>().value == 0)
                {
                    lastCollision.GetComponent<Top>().cards.Add(gameObject);
                    if (transform.GetComponentInParent<Bottoms>() != null)
                    {
                        transform.GetComponentInParent<Bottoms>().cards.Remove(gameObject);
                    }
                    if (transform.GetComponentInParent<Top>() != null)
                    {
                        transform.GetComponentInParent<Top>().cards.Remove(gameObject);
                    }
                    if (transform.GetComponentInParent<RollCard>() != null)
                    {
                        transform.GetComponentInParent<RollCard>().cards.Remove(gameObject);
                    }
                    GameManager.Instance.steps++;
                    GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });

                    transform.SetParent(lastCollision.transform);
                    lastCollision.GetComponent<Top>().value = value;
                    lastCollision.GetComponent<Top>().suit = suit;
                }
                if (value != 1 && (lastCollision.GetComponent<Top>().value == value - 1) && (suit == lastCollision.GetComponent<Top>().suit))
                {
                    lastCollision.GetComponent<Top>().cards.Add(gameObject);
                    if (transform.GetComponentInParent<Bottoms>() != null)
                    {
                        transform.GetComponentInParent<Bottoms>().cards.Remove(gameObject);
                    }
                    if (transform.GetComponentInParent<Top>() != null)
                    {
                        transform.GetComponentInParent<Top>().cards.Remove(gameObject);
                    }
                    if (transform.GetComponentInParent<RollCard>() != null)
                    {
                        transform.GetComponentInParent<RollCard>().cards.Remove(gameObject);
                    }
                    GameManager.Instance.steps++;
                    GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });
                    transform.SetParent(lastCollision.transform);
                    lastCollision.GetComponent<Top>().value = value;
                    lastCollision.GetComponent<Top>().suit = suit;
                }
            }
            if (isOnbottom && lastCollision != null)
            {
                if (value == 13 && lastCollision.GetComponent<Bottoms>().value == 14)
                {
                    GameManager.Instance.steps++;
                    GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });
                    if (transform.GetComponentInParent<RollCard>() != null)
                    {
                        lastCollision.GetComponent<Bottoms>().cards.Add(gameObject);
                        transform.GetComponentInParent<RollCard>().cards.Remove(gameObject);
                    }
                    else if (transform.GetComponentInParent<Top>() != null)
                    {
                        lastCollision.GetComponent<Bottoms>().cards.Add(gameObject);

                        transform.GetComponentInParent<Top>().cards.Remove(gameObject);
                    }
                    else
                    {
                        SwitchDraggingList(lastCollision.GetComponent<Bottoms>());
                    }

                    transform.SetParent(lastCollision.transform);
                }
                else if (value < 13)
                {
                    switch (lastCollision.GetComponent<Bottoms>().suit)
                    {
                        case Suit.S:
                            if (suit == Suit.D || suit == Suit.H)
                            {
                                if (value != 13 && lastCollision.GetComponent<Bottoms>().value == value + 1)
                                {
                                    if (transform.GetComponentInParent<RollCard>() != null)
                                    {
                                        lastCollision.GetComponent<Bottoms>().cards.Add(gameObject);

                                        transform.GetComponentInParent<RollCard>().cards.Remove(gameObject);
                                        GameManager.Instance.steps++;
                                        GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });
                                    }
                                    else if (transform.GetComponentInParent<Top>() != null)
                                    {
                                        lastCollision.GetComponent<Bottoms>().cards.Add(gameObject);

                                        transform.GetComponentInParent<Top>().cards.Remove(gameObject);
                                        GameManager.Instance.steps++;
                                        GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });
                                    }
                                    else
                                    {
                                        SwitchDraggingList(lastCollision.GetComponent<Bottoms>());
                                        GameManager.Instance.steps++;
                                        GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });
                                    }
                                }
                            }
                            break;
                        case Suit.C:
                            if (suit == Suit.D || suit == Suit.H)
                            {
                                if (value != 13 && lastCollision.GetComponent<Bottoms>().value == value + 1)
                                {
                                    if (transform.GetComponentInParent<RollCard>() != null)
                                    {
                                        lastCollision.GetComponent<Bottoms>().cards.Add(gameObject);

                                        transform.GetComponentInParent<RollCard>().cards.Remove(gameObject);
                                        GameManager.Instance.steps++;
                                        GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });
                                    }
                                    else if (transform.GetComponentInParent<Top>() != null)
                                    {
                                        lastCollision.GetComponent<Bottoms>().cards.Add(gameObject);

                                        transform.GetComponentInParent<Top>().cards.Remove(gameObject);
                                        GameManager.Instance.steps++;
                                        GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });
                                    }
                                    else
                                    {
                                        SwitchDraggingList(lastCollision.GetComponent<Bottoms>());
                                        GameManager.Instance.steps++;
                                        GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });

                                    }
                                }
                            }
                            break;
                        case Suit.D:
                            if (suit == Suit.S || suit == Suit.C)
                            {
                                if (value != 13 && lastCollision.GetComponent<Bottoms>().value == value + 1)
                                {
                                    if (transform.GetComponentInParent<RollCard>() != null)
                                    {
                                        lastCollision.GetComponent<Bottoms>().cards.Add(gameObject);

                                        transform.GetComponentInParent<RollCard>().cards.Remove(gameObject);
                                        GameManager.Instance.steps++;
                                        GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });
                                    }
                                    else if (transform.GetComponentInParent<Top>() != null)
                                    {
                                        lastCollision.GetComponent<Bottoms>().cards.Add(gameObject);

                                        transform.GetComponentInParent<Top>().cards.Remove(gameObject);
                                        GameManager.Instance.steps++;
                                        GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });
                                    }
                                    else
                                    {
                                        SwitchDraggingList(lastCollision.GetComponent<Bottoms>());
                                        GameManager.Instance.steps++;
                                        GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });
                                    }
                                }
                            }
                            break;
                        case Suit.H:
                            if (suit == Suit.S || suit == Suit.C)
                            {
                                if (value != 13 && lastCollision.GetComponent<Bottoms>().value == value + 1)
                                {
                                    if (transform.GetComponentInParent<RollCard>() != null)
                                    {
                                        lastCollision.GetComponent<Bottoms>().cards.Add(gameObject);

                                        transform.GetComponentInParent<RollCard>().cards.Remove(gameObject);
                                        GameManager.Instance.steps++;
                                        GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });
                                    }
                                    else if (transform.GetComponentInParent<Top>() != null)
                                    {
                                        lastCollision.GetComponent<Bottoms>().cards.Add(gameObject);

                                        transform.GetComponentInParent<Top>().cards.Remove(gameObject);
                                        GameManager.Instance.steps++;
                                        GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });
                                    }
                                    else
                                    {
                                        SwitchDraggingList(lastCollision.GetComponent<Bottoms>());
                                        GameManager.Instance.steps++;
                                        GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });
                                    }
                                }
                            }
                            break;
                    }
                }
                foreach (GameObject card in GameManager.Instance.draggingCards)
                {
                    card.GetComponent<Cards>().isDragging = false;
                }
                GameManager.Instance.draggingCards = new List<GameObject>();
            }
            foreach (GameObject card in GameManager.Instance.draggingCards)
            {
                card.GetComponent<Cards>().isDragging = false;
            }
            GameManager.Instance.draggingCards = new List<GameObject>();
        }
        GameManager.Instance.draggingCards = new List<GameObject>();


        isDragging = false;

    }
    
    private void AutoMove()
    {
        isDragging = false;
        if (CanMove())
        {
            foreach (Top top in GameManager.Instance.tops)
            {
                if (value == 1 && top.value == 0 && transform.parent.tag != "Top" && GameManager.Instance.draggingCards.Count <= 1)
                {
                    top.cards.Add(gameObject);
                    if (transform.GetComponentInParent<Bottoms>() != null)
                    {
                        transform.GetComponentInParent<Bottoms>().cards.Remove(gameObject);
                    }

                    if (transform.GetComponentInParent<RollCard>() != null)
                    {
                        transform.GetComponentInParent<RollCard>().cards.Remove(gameObject);
                    }
                    GameManager.Instance.steps++;
                    GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });

                    transform.SetParent(top.transform);
                    top.value = value;
                    top.suit = suit;
                    GameManager.Instance.draggingCards.Clear();
                    return;
                }
                if (value != 1 && (top.value == value - 1) && (suit == top.suit) && GameManager.Instance.draggingCards.Count <= 1)
                {
                    top.cards.Add(gameObject);
                    if (transform.GetComponentInParent<Bottoms>() != null)
                    {
                        transform.GetComponentInParent<Bottoms>().cards.Remove(gameObject);
                    }
                    if (transform.GetComponentInParent<RollCard>() != null)
                    {
                        transform.GetComponentInParent<RollCard>().cards.Remove(gameObject);
                    }
                    GameManager.Instance.steps++;
                    GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });
                    transform.SetParent(top.transform);
                    top.value = value;
                    top.suit = suit;
                    GameManager.Instance.draggingCards.Clear();
                    return;
                }
            }
            foreach (Bottoms bot in GameManager.Instance.bottoms)
            {
                Debug.Log(bot.value);
                if (value == 13 && bot.value == 14)
                {
                    GameManager.Instance.steps++;
                    GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });
                    if (transform.GetComponentInParent<RollCard>() != null)
                    {
                        bot.cards.Add(gameObject);
                        transform.GetComponentInParent<RollCard>().cards.Remove(gameObject);
                    }
                    else if (transform.GetComponentInParent<Top>() != null)
                    {
                        bot.cards.Add(gameObject);

                        transform.GetComponentInParent<Top>().cards.Remove(gameObject);
                    }
                    else
                    {
                        SwitchDraggingList(bot);
                    }

                    transform.SetParent(bot.transform);
                    break;
                }
                else if (value < 13)
                {
                    switch (bot.suit)
                    {
                        case Suit.S:
                            if (suit == Suit.D || suit == Suit.H)
                            {
                                if (value != 13 && bot.value == value + 1)
                                {
                                    if (transform.GetComponentInParent<RollCard>() != null)
                                    {
                                        bot.cards.Add(gameObject);

                                        transform.GetComponentInParent<RollCard>().cards.Remove(gameObject);

                                        GameManager.Instance.steps++;
                                        GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });
                                        return;
                                    }
                                    else if (transform.GetComponentInParent<Top>() != null)
                                    {
                                        bot.cards.Add(gameObject);

                                        transform.GetComponentInParent<Top>().cards.Remove(gameObject);

                                        GameManager.Instance.steps++;
                                        GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });

                                        return;

                                    }
                                    else if (transform.GetComponentInParent<Bottoms>() != null)
                                    {
                                      

                                        SwitchDraggingList(bot);
                                        foreach (GameObject card in GameManager.Instance.draggingCards)
                                        {
                                            card.GetComponent<Cards>().isDragging = false;
                                        }
                                        GameManager.Instance.draggingCards = new List<GameObject>();
                                        GameManager.Instance.steps++;
                                        GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });
                                        return;
                                    }
                                }
                            }
                            break;
                        case Suit.C:
                            if (suit == Suit.D || suit == Suit.H)
                            {
                                if (value != 13 && bot.value == value + 1)
                                {
                                    if (transform.GetComponentInParent<RollCard>() != null)
                                    {
                                        bot.cards.Add(gameObject);

                                        transform.GetComponentInParent<RollCard>().cards.Remove(gameObject);

                                        GameManager.Instance.steps++;
                                        GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });
                                        return;
                                    }
                                    else if (transform.GetComponentInParent<Top>() != null)
                                    {
                                        bot.cards.Add(gameObject);

                                        transform.GetComponentInParent<Top>().cards.Remove(gameObject);
                                        GameManager.Instance.steps++;
                                        GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });
                                        return;

                                    }
                                    else if (transform.GetComponentInParent<Bottoms>() != null)
                                    {
                                        Debug.Log("asdfgasdf");

                                        SwitchDraggingList(bot);
                                        foreach (GameObject card in GameManager.Instance.draggingCards)
                                        {
                                            card.GetComponent<Cards>().isDragging = false;
                                        }
                                        GameManager.Instance.draggingCards = new List<GameObject>();
                                        GameManager.Instance.steps++;
                                        GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });
                                        return;


                                    }
                                }
                            }
                            break;
                        case Suit.D:
                            if (suit == Suit.S || suit == Suit.C)
                            {
                                if (value != 13 && bot.value == value + 1)
                                {
                                    if (transform.GetComponentInParent<RollCard>() != null)
                                    {
                                        bot.cards.Add(gameObject);

                                        transform.GetComponentInParent<RollCard>().cards.Remove(gameObject);

                                        GameManager.Instance.steps++;
                                        GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });
                                        return;

                                    }
                                    else if (transform.GetComponentInParent<Top>() != null)
                                    {
                                        bot.cards.Add(gameObject);

                                        transform.GetComponentInParent<Top>().cards.Remove(gameObject);

                                        GameManager.Instance.steps++;
                                        GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });
                                        return;

                                    }
                                    else if (transform.GetComponentInParent<Bottoms>() != null)
                                    {
                         
            
                                        SwitchDraggingList(bot);
                                        foreach (GameObject card in GameManager.Instance.draggingCards)
                                        {
                                            card.GetComponent<Cards>().isDragging = false;
                                        }
                                        GameManager.Instance.draggingCards = new List<GameObject>();
                                        GameManager.Instance.steps++;
                                        GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });
                                        return;

                                    }
                                }
                            }
                            break;
                        case Suit.H:
                            if (suit == Suit.S || suit == Suit.C)
                            {
                                if (value != 13 && bot.value == value + 1)
                                {
                                    if (transform.GetComponentInParent<RollCard>() != null)
                                    {
                                        bot.cards.Add(gameObject);

                                        transform.GetComponentInParent<RollCard>().cards.Remove(gameObject);

                                        GameManager.Instance.steps++;
                                        GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });
                                        return;

                                    }
                                    else if (transform.GetComponentInParent<Top>() != null)
                                    {
                                        bot.cards.Add(gameObject);

                                        transform.GetComponentInParent<Top>().cards.Remove(gameObject);

                                        GameManager.Instance.steps++;
                                        GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });
                                        return;

                                    }
                                    else if (transform.GetComponentInParent<Bottoms>() != null)
                                    {
                                        
                                        SwitchDraggingList(bot);
                                        foreach (GameObject card in GameManager.Instance.draggingCards)
                                        {
                                            card.GetComponent<Cards>().isDragging = false;
                                        }
                                        GameManager.Instance.draggingCards = new List<GameObject>();
                                        GameManager.Instance.steps++;
                                        GameManager.Instance.AddStep(new GameManager.ObjectState() { card = this, parent = transform.parent });
                                        return;

                                    }
                                }
                            }
                            break;
                    }
                }
            }
        }
        else
        {
            Rung();
        }


    }

    public void Rung()
    {
        foreach (GameObject card in GameManager.Instance.draggingCards)
        {
            card.transform.DOShakePosition(0.3f, new Vector3(0.05f, 0.05f, 0), 5, 90f, false)
                .OnComplete(() => {
                    transform.DOMove(targetPos, 0.1f);
                });

        }
    }

    public bool CanMove()
    {
        foreach (Top top in GameManager.Instance.tops)
        {
            if (value == 1 && top.value == 0 && transform.parent.tag != "Top")
            {
                return true;
            }
            if (value != 1 && (top.value == value - 1) && (suit == top.suit))
            {
                return true;
            }
        }
        foreach (Bottoms bot in GameManager.Instance.bottoms)
        {
            if (value == 13 && bot.value == 14)
            {
                return true;
            }
            else if (value < 13)
            {
                switch (bot.suit)
                {
                    case Suit.S:
                        if (suit == Suit.D || suit == Suit.H)
                        {
                            if (value != 13 && bot.value == value + 1)
                            {
                                return true;
                            }
                        }
                        break;
                    case Suit.C:
                        if (suit == Suit.D || suit == Suit.H)
                        {
                            if (value != 13 && bot.value == value + 1)
                            {
                                return true;
                            }
                        }
                        break;
                    case Suit.D:
                        if (suit == Suit.S || suit == Suit.C)
                        {
                            if (value != 13 && bot.value == value + 1)
                            {
                                return true;
                            }
                        }
                        break;
                    case Suit.H:
                        if (suit == Suit.S || suit == Suit.C)
                        {
                            if (value != 13 && bot.value == value + 1)
                            {
                                return true;
                            }
                        }
                        break;
                }
            }


        }
        return false;
    }

    private void SwitchDraggingList(Bottoms nextmove)
    {

        for(int i = GameManager.Instance.draggingCards.Count - 1; i >= 0; i--)
        {
            GameManager.Instance.draggingCards[i].GetComponentInParent<Bottoms>().cards.RemoveAt(GameManager.Instance.draggingCards[i].GetComponentInParent<Bottoms>().cards.Count - 1);
        }

        nextmove.cards.AddRange(GameManager.Instance.draggingCards);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Top"))
        {
            isOntop = true;
            isOnbottom = false;
            lastCollision = collision;

        }
        if(collision.CompareTag("Bot"))
        {
            isOnbottom = true;
            isOntop = false;
            lastCollision = collision;

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

            if (collision.CompareTag("Top"))
            {
                isOntop = true;
                isOnbottom = false;
                lastCollision = collision;

            }
            if (collision.CompareTag("Bot"))
            {
                isOnbottom = true;
                isOntop = false;
                lastCollision = collision;

            }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Top"))
        {
            isOntop = false;
            lastCollision = null;

        }
        if (collision.CompareTag("Bot"))
        {
            isOnbottom = false;
            lastCollision = null;

        }
    }
    private void LateUpdate()
    {
        if (!isDragging)
        {
            transform.position =  Vector3.Lerp(transform.position, targetPos, 0.5f);
        }
    }

    

}
