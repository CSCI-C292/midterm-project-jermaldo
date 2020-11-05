using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class Settings : MonoBehaviour
{
    public AudioMixer _musicMixer;
    public AudioMixer _effectsMixer;
    public void SetMusicVolume(float volume)
    {
        _musicMixer.SetFloat("Volume", volume);
    }
    public void SetFullScreen(bool isFull)
    {
        Screen.fullScreen = isFull;
    }
    public void SetEffectsVolume(float volume)
    {
        _effectsMixer.SetFloat("SoundEffectsVolume", volume);
    }
}
 