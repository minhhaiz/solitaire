using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cards;

public class CardHint : MonoBehaviour
{
    public Sprite[] faceSprite;
    public int value;
    public Suit suit;
    public GameObject face;
    private void Update()
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
}
