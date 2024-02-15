using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public enum HUDType { timer, step}
    public HUDType type;
    private void Update()
    {
        switch(type)
        {
            case HUDType.timer:
                float time = GameManager.Instance.timer;
                GetComponent<TextMeshProUGUI>().text = string.Format("Time\n{0:0}:{1:00}",(int)time/60, (int)time % 60);
                break;
            case HUDType.step:
                GetComponent<TextMeshProUGUI>().text = string.Format("Steps\n {0}", GameManager.Instance.steps);
                break;
        }
    }
}
