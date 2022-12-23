using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public int level;
    public bool isLock;

    public void OnSelectLevel()
    {
        LevelManager.Instance.InitLevel(level);
    }
}
