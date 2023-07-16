using System;
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

    /// <summary>
    /// 寻找变色道具可变色的block
    /// </summary>
    /// <param name="blocks"></param>
    /// <param name="position"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 寻找炸弹道具可炸毁的block
    /// </summary>
    /// <param name="blocks"></param>
    /// <param name="position"></param>
    /// <returns></returns>
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

    #region 预测Map
    static BlockColor[,] curMap = new BlockColor[LevelManager.MaxX, LevelManager.MaxY];
    static Block[,] predictionMap = new Block[LevelManager.MaxX, LevelManager.MaxY];

    /// <summary>
    /// 预测地图
    /// </summary>
    /// <param name="blocks"></param>
    /// <returns></returns>
    public static List<Block> PredictionMap(List<Block> blocks)
    {
        List<Block> result = new List<Block>();
        ClearMapData(curMap);
        int count = 0;
        foreach (Block block in blocks)
        {
            curMap[block.x, block.y] = block.color;
        }
        for (int f = (int)Forward.Up; f <= (int)Forward.Right; f++)
        {
            predictionMap = new Block[LevelManager.MaxX, LevelManager.MaxY];
            Forward forward = (Forward)f;
            foreach (Block block in blocks)
            {
                count = 0;
                switch (forward)
                {
                    case Forward.Left:
                        for (int i = block.x - 1; i >= 0; i--)
                        {
                            if (curMap[i, block.y] == BlockColor.None)
                                count++;
                        }
                        predictionMap[block.x - count, block.y] = block;
                        break;
                    case Forward.Right:
                        for (int i = block.x + 1; i < LevelManager.MaxX; i++)
                        {
                            if (curMap[i, block.y] == BlockColor.None)
                                count++;
                        }
                        predictionMap[block.x + count, block.y] = block;
                        break;
                    case Forward.Up:
                        for (int i = block.y - 1; i >= 0; i--)
                        {
                            if (curMap[block.x, i] == BlockColor.None)
                                count++;
                        }
                        predictionMap[block.x, block.y - count] = block;
                        break;
                    case Forward.Down:
                        for (int i = block.y + 1; i < LevelManager.MaxY; i++)
                        {
                            if (curMap[block.x, i] == BlockColor.None)
                                count++;
                        }
                        predictionMap[block.x, block.y + count] = block;
                        break;
                }
            }

            for (int i = 0; i < predictionMap.GetLength(0); i++)
                for (int j = 0; j < predictionMap.GetLength(1); j++)
                {
                    if (predictionMap[i, j] != null)
                    {
                        result = CheckBlock(predictionMap[i, j], i, j);
                        if (result.Count > 0)
                            return result;
                    }
                }
        }
        return result;
    }

    private static List<Block> CheckBlock(Block block, int x, int y)
    {
        Block blockA, blockB;
        List<Block> result = new List<Block>();
        if (x > 0 && x < LevelManager.MaxX - 1)
        {
            blockA = predictionMap[x - 1, y];
            blockB = predictionMap[x + 1, y];
            if (blockA != null && blockB != null && CheckColor(block, blockA) && CheckColor(block, blockB) && CheckColor(blockA, blockB))
            {
                result.Add(block);
                result.Add(blockA);
                result.Add(blockB);
                return result;
            }
        }
        if (y > 0 && y < LevelManager.MaxY - 1)
        {
            blockA = predictionMap[x, y - 1];
            blockB = predictionMap[x, y + 1];
            if (blockA != null && blockB != null && CheckColor(block, blockA) && CheckColor(block, blockB) && CheckColor(blockA, blockB))
            {
                result.Add(block);
                result.Add(blockA);
                result.Add(blockB);
                return result;
            }
        }
        return result;
    }

    private static bool CheckColor(Block a, Block b)
    {
        if (a.color == BlockColor.AllColor || b.color == BlockColor.AllColor)
            return true;
        return a.color == b.color;
    }

    private static void ClearMapData(BlockColor[,] array)
    {
        for (int i = 0; i < array.GetLength(0); i++)
            for (int j = 0; j < array.GetLength(1); j++)
                array[i, j] = BlockColor.None;
    }
    #endregion
}
