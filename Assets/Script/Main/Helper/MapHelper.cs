using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapHelper
{
    static float minColorLength = 60;
    static float minBombLength = 250;
    static float minBombmin = 64;
    static int[] forwardX = new int[4] { 1, 0, -1, 0 };
    static int[] forwardY = new int[4] { 0, -1, 0, 1 };

    public static Block FindColorBlock(List<Block> blocks, Vector3 position)
    {
        float min = minColorLength;
        Block result = null;
        foreach (Block block in blocks)
        {
            if (block.color != BlockColor.AllColor)
            {
                float length = (block.GetComponent<Transform>().position - position).magnitude;
                if (length < min)
                {
                    min = length;
                    result = block;
                }
            }
        }
        return result;
    }

    public static List<Block> FindBombBlock(List<Block> blocks, Vector3 position)
    {

        List<Block> result = new List<Block>();
        foreach (Block block in blocks)
        {
            Vector3 p = block.GetComponent<Transform>().position;
            if (p.x == position.x)
                p.x += 0.1f;
            if (p.y == position.y)
                p.y += 0.1f;
            block.distance = Mathf.Abs(p.x - position.x) + Mathf.Abs(p.y - position.y);
        }
        blocks.Sort((x, y) => x.distance.CompareTo(y.distance));
        if (blocks.Count > 0)
        {
            if (blocks[0].distance < minBombmin)
            {
                result.Add(blocks[0]);
                Block newBlock;
                for (int i = 0; i < 4; i++)
                {
                    newBlock = LevelManager.Instance.FindBlock(blocks[0].x + forwardX[i], blocks[0].y + forwardY[i]);
                    if (newBlock != null)
                        result.Add(newBlock);
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    if (blocks.Count > i && blocks[i].distance < minBombLength)
                    {
                        result.Add(blocks[i]);
                    }
                }
            }
        }

        return result;
    }
}
