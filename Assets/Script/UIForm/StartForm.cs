using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartForm : BaseForm
{
    public override void Show()
    {
        base.Show();

    }

    public override void Hide()
    {
        base.Hide();

    }

    public void OnStartClick()
    {
        LevelManager.Instance.InitLevel(0);
        Hide();
    }
}
