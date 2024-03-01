using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ToggleSwitch : MonoBehaviour
{
    public enum SwitchType
    {
        Sound,
        Music,
        Vibration
    }

    public bool isOn = false;
    public GameObject switchObj;
    public RectTransform pointOn;
    public RectTransform pointOff;
    public SwitchType switchType;

    private void Update()
    {

        if (isOn)
        {
            switchObj.GetComponent<RectTransform>().position = Vector3.Lerp(switchObj.GetComponent<RectTransform>().position, pointOn.position, 0.1f);
            GetComponent<Image>().color = Color.green;
            if (switchType == SwitchType.Sound)
            {
                SFXManager.instance.UnmuteSfx();
            }
            else if (switchType == SwitchType.Music)
            {
                SFXManager.instance.UnmuteMusic();
            }
        }
        else
        {
            GetComponent<Image>().color = Color.red;
            switchObj.GetComponent<RectTransform>().position = Vector3.Lerp(switchObj.GetComponent<RectTransform>().position, pointOff.position, 0.1f);
            if (switchType == SwitchType.Sound)
            {
                SFXManager.instance.MuteSfx();
            }
            else if (switchType == SwitchType.Music)
            {
                SFXManager.instance.MuteMusic();
            }
        }
    }
    public void Click()
    {
        SFXManager.instance.PlayClick();
        if (isOn)
        {
            isOn = false;
        }
        else
        {
            isOn = true;
        }
    }
}
