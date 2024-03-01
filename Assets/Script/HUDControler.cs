using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameManager;

public class HUDControler : MonoBehaviour
{
    public enum HUDState
    {
        Win,
        Time,
        HighscoreEasy,
        HighscoreMedium,
        HighscoreHard,
        LevelText,
        NewGameTxt,
        Heart,
        HintText,
        MoneyText,
        ReceiveMoney

    }
    public HUDState state;

    private void Update()
    {
        switch (state)
        {
            case HUDState.Win:

                break;
            case HUDState.Time:

                break;
            case HUDState.HighscoreEasy:
                break;
            case HUDState.HighscoreMedium:
                break;
            case HUDState.HighscoreHard:
                break;
            case HUDState.LevelText:
     
                break;
            case HUDState.NewGameTxt:
                break;
            case HUDState.Heart:
                break;
            case HUDState.HintText:

                break;
            case HUDState.MoneyText:

                break;
            case HUDState.ReceiveMoney:

                break;
        }
    }
}
