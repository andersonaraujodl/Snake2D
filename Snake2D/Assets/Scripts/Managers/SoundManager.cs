using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource sfxSource;
    public AudioSource musicSource;

    public List<AudioClipDictionary> sfxClips;
    public List<AudioClipDictionary> musicClips;

    private bool isMute;
    private UIController uiController;

    public bool IsMute
    {
        get { return isMute; }
    }

    public void Init(UIController _uiController)
    {
        uiController = _uiController;

        if(PlayerPrefs.HasKey("IsMute"))
        {
            isMute = (PlayerPrefs.GetInt("IsMute") == 1) ? true : false;
            OnMuteChange();
        }
    }

    public AudioClip GetSFXClip(string name)
    {
        return sfxClips.First(x => x.name == name).clip;
    }

    public AudioClip GetMusicClip(string name)
    {
        return musicClips.First(x => x.name == name).clip;
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.clip = clip;
        
        if (!isMute)
        {
            sfxSource.Play();
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;

        if(!isMute)
        {
            musicSource.Play();
        }
    }

    public void ToggleMute()
    {
        isMute = !isMute;
        int muteInt = isMute ? 1 : 0;
        PlayerPrefs.SetInt("IsMute", muteInt);

        OnMuteChange();
    }

    private void OnMuteChange()
    {
        uiController.OnSoundSettingClick();

        if (isMute)
        {
            musicSource.Stop();
            sfxSource.Stop();
        }
        else
        {
            musicSource.Play();
        }
    }
}