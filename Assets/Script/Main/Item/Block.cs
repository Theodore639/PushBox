using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    public BlockColor color;
    public GameObject shadow;
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

    public void ShowShadow(bool isShow)
    {
        shadow.gameObject.SetActive(isShow);
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
