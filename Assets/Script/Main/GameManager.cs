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

