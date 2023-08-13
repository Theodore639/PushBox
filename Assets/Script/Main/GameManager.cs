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

    }

    private void Start()
    {

    }

    public void StartGame(int level)
    {
        LevelManager.Instance.InitLevel(level);
    }

    public void GameOver()
    {

    }

    public void GamePause()
    {

    }

    public void GameResume()
    {

    }

}

