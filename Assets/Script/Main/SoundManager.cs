using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public static AudioSource audioSource;
    public static AudioClip bgMusic;

    public static AudioClip move;
    public static AudioClip clear;
    public static AudioClip spwan;
    public static AudioClip victory, fail;
    public static AudioClip color, bomb, random;

    public static void SetSoundSwitch()
    {

    }

    public static void PlayMove()
    {
        audioSource.PlayOneShot(move);
    }

}
