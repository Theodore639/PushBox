using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainForm : MonoBehaviour
{
    private static MainForm instance;
    public static MainForm Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(MainForm)) as MainForm;
            }
            return instance;
        }
    }

    public bool musicSwitch, soundSwitch;
    public Image music, sound;
    private Sprite[] uiSprites;

    // Start is called before the first frame update
    void Start()
    {
        musicSwitch = PlayerPrefs.GetInt("Music", 1) == 1;
        soundSwitch = PlayerPrefs.GetInt("Sound", 1) == 1;
        uiSprites = Resources.LoadAll<Sprite>("Sprites/UI");
        UpdateSprite();
    }

    public void OnMusicClick()
    {
        musicSwitch = !musicSwitch;
        PlayerPrefs.SetInt("Music", musicSwitch ? 1 : 0);
        UpdateSprite();
    }

    public void OnSoundClick()
    {
        soundSwitch = !soundSwitch;
        PlayerPrefs.SetInt("Sound", musicSwitch ? 1 : 0);
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        music.sprite = uiSprites[musicSwitch ? 5 : 4];
        sound.sprite = uiSprites[soundSwitch ? 0 : 1];
    }
}
