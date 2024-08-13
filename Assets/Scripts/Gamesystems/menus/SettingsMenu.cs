using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    private bool muted = false;
    public void SetVolume(float volume)
    {
        if (!muted)
        {
            audioMixer.SetFloat("volume", volume);
        }
    }
    public void Mute(float volume)
    {
        if (!muted)
        {
            muted = true;
            audioMixer.SetFloat("volume", -80);
        }
        else
        {
            audioMixer.SetFloat("volume", -40);
            muted = false;
        }
    }
}
