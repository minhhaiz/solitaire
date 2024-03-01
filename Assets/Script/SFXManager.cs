using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    public AudioSource bgMusic;
    public AudioSource pick;
    public AudioSource click;
    public AudioSource win;
    public AudioSource lose;
    public AudioSource paper;
    public AudioSource wrong;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else Destroy(gameObject);
    }
    public void PlayBgMusic()
    {
        bgMusic.Play();
    }
    public void MuteMusic()
    {
        bgMusic.mute = true;

    }
    public void UnmuteMusic()
    {
        bgMusic.mute = false;
    }
    public void MuteSfx()
    {
        pick.mute = true;
        click.mute = true;
        win.mute = true;
        lose.mute = true;
        paper.mute = true;
        wrong.mute = true;
    }
    public void UnmuteSfx()
    {
        pick.mute = false;
        click.mute = false;
        win.mute = false;
        lose.mute = false;
        paper.mute = false;
        wrong.mute = false;
    }
    public void PlayPick()
    {
        pick.Play();
    }
    public void PlayClick()
    {
        click.Play();
    }
    public void PlayWin()
    {
        win.Play();
        Debug.Log("Win");

    }
    public void PlayLose()
    {
        lose.Play();
        Debug.Log("lose");
    }
    public void PlayPaper()
    {
        paper.Play();
    }   
    public void PlayWrong()
    {
        wrong.Play();
    }

}
