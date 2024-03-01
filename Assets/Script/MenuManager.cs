using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static ToggleSwitch;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    [Header("Panels")]
    public GameObject selectChalengePanel;
    public GameObject selectChalengePanel_Child;
    public GameObject homePanel, shopPanel, dailyPanel;
    public GameObject settingsPanel;
    public GameObject settingsPanel_Child;

    [Header("GameObjects")]
    public GameObject canvas;
    public ToggleSwitch musicSwitch;
    public ToggleSwitch soundSwitch;
    public ToggleSwitch vibrateSwitch;


    [Header("Buttons")]
    public Button homeButton;
    public Button shopButton;
    public Button dailyButton;

    [Header("Others")]
    public int maxLevel = 2004;
    public int selectedDaily = DateTime.Today.Day;               
    public bool isStart;
    private string savePath;
    string encryptKey = "iamnupermane4133bbce2ea2315a1916";
    

    private void Awake()
    {
        instance = this;
        isStart = false;
        Application.targetFrameRate = 144;

        
        shopPanel.GetComponent<RectTransform>().localPosition = Vector3.left * 10000;
        dailyPanel.GetComponent<RectTransform>().localPosition = Vector3.right * 10000;
        homeButton.GetComponent<Image>().color = Color.red;
    }



    public void PlayChalenge()
    {
        selectChalengePanel.SetActive(true);
        selectChalengePanel_Child.GetComponent<RectTransform>().DOMove(Vector3.zero + canvas.GetComponent<RectTransform>().position, 0.5f);
    }
    public void CloseChalenge()
    {
        selectChalengePanel_Child.GetComponent<RectTransform>().DOMove(Vector3.right * 10000 + canvas.GetComponent<RectTransform>().position, 0.4f).OnComplete(() => selectChalengePanel.SetActive(false));
    }


    static bool CheckLevelExistence(string name)
    {
        string prefabPath = name;

        if (File.Exists(prefabPath))
        {
            Debug.Log("Prefab exists at path: " + prefabPath);
            return true;
        }
        else
        {
            Debug.LogWarning("Prefab does not exist at path: " + prefabPath);
            return false;
        }
    }

   
    
    public void Home()
    {
        homePanel.GetComponent<RectTransform>().DOMove(canvas.GetComponent<RectTransform>().position, 0.25f);
        shopPanel.GetComponent<RectTransform>().DOMove(Vector3.left * 10000 + new Vector3(0, canvas.GetComponent<RectTransform>().position.y, 0), 0.25f);
        dailyPanel.GetComponent<RectTransform>().DOMove(Vector3.right * 10000 + new Vector3(0, canvas.GetComponent<RectTransform>().position.y, 0), 0.25f);
     
        homeButton.GetComponent<Image>().color = Color.red;
        shopButton.GetComponent<Image>().color = Color.white;
        dailyButton.GetComponent<Image>().color = Color.white;
    }
    public void Shop()
    {
        shopPanel.GetComponent<RectTransform>().DOMove(canvas.GetComponent<RectTransform>().position, 0.25f);
        homePanel.GetComponent<RectTransform>().DOMove(Vector3.right * 10000 + new Vector3(0, canvas.GetComponent<RectTransform>().position.y, 0), 0.25f);
        dailyPanel.GetComponent<RectTransform>().DOMove(Vector3.right * 20000 + new Vector3(0, canvas.GetComponent<RectTransform>().position.y, 0), 0.25f);
        homeButton.GetComponent<Image>().color = Color.white;
        shopButton.GetComponent<Image>().color = Color.red;
        dailyButton.GetComponent<Image>().color = Color.white;
    }
    public void Daily()
    {
        Calendar cal = GameObject.Find("Calendar").GetComponent<Calendar>();
        dailyPanel.GetComponent<RectTransform>().DOMove(canvas.GetComponent<RectTransform>().position, 0.25f);
        homePanel.GetComponent<RectTransform>().DOMove(Vector3.left * 10000 + new Vector3(0, canvas.GetComponent<RectTransform>().position.y, 0), 0.25f);
        shopPanel.GetComponent<RectTransform>().DOMove(Vector3.left * 20000 + new Vector3(0, canvas.GetComponent<RectTransform>().position.y, 0), 0.25f);
        homeButton.GetComponent<Image>().color = Color.white;
        shopButton.GetComponent<Image>().color = Color.white;
        dailyButton.GetComponent<Image>().color = Color.red;
    }

    public void SettingClick()
    {
        settingsPanel.SetActive(true);
        settingsPanel_Child.GetComponent<RectTransform>().DOMove(canvas.GetComponent<RectTransform>().position, 0.25f);
    }
    public void CloseSetting()
    {
        settingsPanel_Child.GetComponent<RectTransform>().DOMove(Vector3.up * 10000 + canvas.GetComponent<RectTransform>().position, 0.25f).OnComplete(() => settingsPanel.SetActive(false));
    }
    public void playRanDom()
    {
        SceneManager.LoadScene("Random");
        
    }
}
