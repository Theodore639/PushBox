using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            }
            return instance;
        }
    }

    //��ͨ�������ؿ�
    public static int MaxLevel 
    { 
        get
        {
            return PlayerPrefs.GetInt("MaxLevel", 0);
        }
        set
        {
            PlayerPrefs.SetInt("MaxLevel", value);
        }
    }

    //��һ������Ĺؿ�
    public static int LastLevel
    {
        get
        {
            return PlayerPrefs.GetInt("LastLevel", 0);
        }
        set
        {
            PlayerPrefs.SetInt("LastLevel", value);
        }
    }

    private void Awake()
    {
        MapManager.ReadAllMap();
    }

    private void Start()
    {
        LevelManager.Instance.InitLevel(0);
    }

    public void StartLevel(int level)
    {
        
    }

    public void GamePause()
    {

    }

    public void GameResume()
    {

    }

}

