using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapHelper
{
    static float minLength = 10;

    public static Block FindColorBlock(List<Block> blocks, Vector2 position)
    {
        float min = minLength;
        Block result = null;
        foreach (Block block in blocks)
        {
            if (block.color != BlockColor.AllColor)
            {
                float length = (block.GetComponent<RectTransform>().anchoredPosition - position).magnitude;
                if (length < min)
                {
                    min = length;
                    result = block;
                }
            }
        }
        return result;
    }

    public static List<Block> FindBombBlock(List<Block> blocks, Vector2 position)
    {
        float min = minLength;
        int num = 4;
        float minmin = 5;
        List<Block> result = new List<Block>();
        foreach (Block block in blocks)
        {
            block.distance = (block.GetComponent<RectTransform>().anchoredPosition - position).magnitude;
        }
        blocks.Sort((x, y) => x.distance.CompareTo(y.distance));
        int j = 0;
        for (int i = 0; i < num; i++)
        {
            if (blocks[j].distance < min)
            {
                result.Add(blocks[j]);
                if (blocks[j].distance > minmin)
                    j++;
            }
        }
        return result;
    }
}
