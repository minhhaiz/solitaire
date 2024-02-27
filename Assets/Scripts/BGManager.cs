using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class BGManager : MonoBehaviour
{
    public GameObject panelBot;
    Vector3 targetBot;
    bool isOn = true;
    private void Start()
    {
        targetBot = panelBot.GetComponent<RectTransform>().position;

              
    }

    private void OnMouseDown()
    {
        if (isOn)
        {
            isOn = false;
            panelBot.GetComponent<RectTransform>().DOMove(targetBot - new Vector3(0, 1000, 0), 0.6f);
        }
        else
        {
            isOn = true;
            panelBot.GetComponent<RectTransform>().DOMove(targetBot, 0.6f);
        }
    }
}