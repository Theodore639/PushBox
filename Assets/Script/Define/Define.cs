using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CONST
{
    public const float SpwanAnimationTime = 0.3f;
    public const float MoveAnimationTime = 0.3f;
    public const float ClearAnimationTime = 0.7f;
}

public enum BlockColor
{
    Red = 0,
    Blue = 1,
    Yellow = 2,
    Green = 3,
    Purple = 4,
    Orange = 6,
    ColorEnd = 5,
    None = 100,//¿Õ
    AllColor = 101,//²ÊÉ«
    Bomb = 102,//Õ¨µ¯
}

public enum MapState
{
    Wait = 0,
    Swiping = 1,
    Moving = 2,
    Cleaning = 3,
    Spwaning = 4,
}

public enum Forward
{
    None = 0,
    Up = 1,
    Down = 2,
    Left = 3,
    Right = 4,
}
