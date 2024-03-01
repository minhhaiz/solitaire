using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Calendar : MonoBehaviour
{
    public GameObject calBody;
    public Daily[] slots;
    void Start()
    {
        FlatCalendar flatCalendar;
        flatCalendar = GameObject.Find("FlatCalendar").GetComponent<FlatCalendar>();
        flatCalendar.initFlatCalendar();
        flatCalendar.installDemoData();
        slots = calBody.GetComponentsInChildren<Daily>();


    }


}
