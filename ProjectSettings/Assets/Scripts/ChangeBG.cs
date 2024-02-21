using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBG : MonoBehaviour
{
    public  Sprite[] backGroundList;
   int backGroundIndex;


    private void Awake()
    {
        backGroundIndex = 0;
    }
    public void ChangedBC()
    {
        backGroundIndex++;
        if (backGroundIndex >= backGroundList.Length)
        {
            backGroundIndex = 0;
        }

        GetComponent<SpriteRenderer>().sprite = backGroundList[backGroundIndex];

    }
}
