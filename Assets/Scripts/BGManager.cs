using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class BGManager : MonoBehaviour
{
    public GameObject panelBot;
    public GameObject panelPlay, an;

    Vector3 targetBot;
    Vector3 targetPlay;
    public GameObject panelTop;
    bool isOn = true;
    private void Start()
    {
        targetBot = panelBot.GetComponent<RectTransform>().position;
        targetPlay = panelPlay.GetComponent<RectTransform>().position;
              
    }

    private void OnMouseDown()
    {
        if (isOn)
        {
            isOn = false;
            //panelBot.GetComponent<RectTransform>().DOMove(targetBot - new Vector3(0, 1000, 0), 0.4f);
            panelPlay.GetComponent<RectTransform>().DOMove(targetPlay - new Vector3(0, 3000, 0), 0.4f);
            panelTop.SetActive(true);
            an.SetActive(true);
        }
        else
        {
            isOn = true;
            panelBot.GetComponent<RectTransform>().DOMove(targetBot, 0.4f);
        }
    }
}
