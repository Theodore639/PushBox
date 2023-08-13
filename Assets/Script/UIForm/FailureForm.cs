using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailureForm : BaseForm
{
    private static FailureForm instance;
    public static FailureForm Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(FailureForm)) as FailureForm;
            }
            return instance;
        }
    }

    public void RestartClick()
    {
        Hide();
        MainForm.Instance.Show();
        LevelManager.Instance.InitLevel(LevelManager.Instance.level);
    }
}
