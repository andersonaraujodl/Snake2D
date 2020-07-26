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

    private bool mute;

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
        sfxSource.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void Mute(bool status)
    {
        mute = status;
    }
}