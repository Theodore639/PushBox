using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectForm : BaseForm
{
    public Transform content;
    public GameObject button;

    public void OnInit()
    {
        for(int i = 1; i < MapManager.GetMapCount(); i++)
        {
            GameObject b = Instantiate(button, content);
            b.name = i.ToString();
            b.GetComponent<LevelButton>().level = i;
            b.transform.Find("Text").GetComponent<Text>().text = (i + 1).ToString();
        }
    }

    //更新关卡解锁状态
    public void UpdateLevelState()
    {
        for(int i = 0; i < MapManager.GetMapCount(); i++)
        {
            content.Find(i.ToString()).GetComponent<Button>().interactable = i <= GameManager.MaxLevel;
        }
    }

    public override void Show()
    {
        base.Show();
        if (content.childCount < 2)
            OnInit();
        UpdateLevelState();
    }
}
