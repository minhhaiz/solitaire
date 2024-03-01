using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Daily : MonoBehaviour
{
    public bool isPassed = false;

    public bool IsPassed { get => isPassed; set => isPassed = value; }

    public Daily(bool isPassed)
    {
        this.isPassed = isPassed;
    }
    private void Awake()
    {
        GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;
        var button = GetComponent<Button>();

    }



    private void Update()
    {
        if (isPassed)
        {
            GetComponent<Button>().interactable = false;
            GetComponentInChildren<Text>().color = Color.gray;
        }
        if (!isPassed && int.Parse(GetComponentInChildren<Text>().text) < DateTime.Today.Day)
        {
            GetComponent<Button>().interactable = true;
            GetComponentInChildren<Text>().color = Color.red;
        }
        if (int.Parse(GetComponentInChildren<Text>().text) > DateTime.Today.Day)
        {
            GetComponent<Button>().interactable = false;
            GetComponentInChildren<Text>().color = Color.gray;
        }
    }

}
