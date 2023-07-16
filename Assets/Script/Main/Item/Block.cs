using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    public BlockColor color;
    public GameObject bombObj, colorObj;
    public float distance;//用于计算与拖动时与手指之间的距离
    public int x, y;
    Image image;

    public void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnSpwan(BlockColor _color, int _x, int _y)
    {
        color = _color;
        x = _x;
        y = _y;
        LevelManager.Instance.SetPosition(transform, x, y);
        if (color != BlockColor.AllColor)
            image.sprite = LevelManager.Instance.spriteList[(int)color];
        else
            image.sprite = LevelManager.Instance.allColor;
        ShowSpwanAnimation();
    }

    public void Clear()
    {
        ShowClearAnimation();
        LevelManager.Instance.blocks.Remove(this);
        Destroy(gameObject, CONST.ClearAnimationTime);
    }

    public void MoveTo(int _x, int _y)
    {
        x = _x;
        y = _y;
        Vector3 oldPosition = transform.position;
        LevelManager.Instance.SetPosition(transform, x, y);
        iTween.MoveFrom(gameObject, oldPosition, CONST.MoveAnimationTime);
    }

    public void ChangeToColor()
    {
        color = BlockColor.AllColor;
        image.sprite = LevelManager.Instance.allColor;
        ShowColor(false);
    }

    public void ChangeToBomb()
    {
        Clear();
        ShowBomb(false);
    }

    public void ShowColor(bool isShow)
    {
        SetAlpha(isShow ? 0 : 1);
        colorObj.gameObject.SetActive(isShow);
    }

    public void ShowBomb(bool isShow)
    {
        SetAlpha(isShow ? 0 : 1);
        bombObj.gameObject.SetActive(isShow);
    }

    private void SetAlpha(float a)
    {
        image.color = new Color(1, 1, 1, a);
    }

    private void ShowSpwanAnimation()
    {
        iTween.ScaleFrom(gameObject, Vector3.zero, CONST.SpwanAnimationTime);
    }

    private void ShowClearAnimation()
    {
        iTween.ScaleTo(gameObject, Vector3.zero, CONST.SpwanAnimationTime);
    }
}
