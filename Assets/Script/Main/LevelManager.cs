using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(LevelManager)) as LevelManager;
            }
            return instance;
        }
    }

    public GameObject block, dog;
    MapData curMapData;
    Block[,] blocks;
    GameObject dogObj;
    int maxX;
    int maxY;

    public void InitLevel(int index)
    {
        curMapData = MapManager.GetMapData(index);
        blocks = new Block[curMapData.mapNode.GetLength(0), curMapData.mapNode.GetLength(1)];
        maxX = blocks.GetLength(0);
        maxY = blocks.GetLength(1);
        for (int i = 0; i < maxX; i++)
            for (int j = 0; j < maxY; j++)
            {
                blocks[i, j] = Instantiate(block, transform).GetComponent<Block>();
                SetPosition(blocks[i, j].transform, i, j);
            }
        dogObj = Instantiate(dog, transform).gameObject;
        CoverNode(curMapData.x, curMapData.y);
        UpdateLevel();
    }

    private void SetPosition(Transform t, int x, int y)
    {
        t.localPosition = new Vector3(maxX / -2.0f + x + 0.5f, maxY / 2.0f - y, 0) * 1.5f;
    }

    private void Update()
    {
        Forward f = ControllerManager.Instance.GetCommand();
        if (f != Forward.None)
        {
            Move(f);
        }
    }

    //Õ¹Ê¾levelÍ¼Ïñ
    private void UpdateLevel()
    {
        for (int i = 0; i < maxX; i++)
            for (int j = 0; j < maxY; j++)
            {
                blocks[i, j].SetState(curMapData.mapNode[i, j]);
            }
        SetPosition(dogObj.transform, curMapData.x, curMapData.y);
    }

    public void Move(Forward forward)
    {
        switch (forward)
        {
            case Forward.Left:
                for (int i = curMapData.x - 1; i >= 0; i--)
                {
                    if (!IsCanThrough(curMapData.mapNode[i, curMapData.y]))
                        break;
                    curMapData.x = i;
                    CoverNode(i, curMapData.y);
                }
                break;
            case Forward.Right:
                for (int i = curMapData.x + 1; i < maxY; i++)
                {
                    if (!IsCanThrough(curMapData.mapNode[i, curMapData.y]))
                        break;
                    curMapData.x = i;
                    CoverNode(i, curMapData.y);
                }
                break;
            case Forward.Up:
                for (int j = curMapData.y - 1; j >= 0; j--)
                {
                    if (!IsCanThrough(curMapData.mapNode[curMapData.x, j]))
                        break;
                    curMapData.y = j;
                    CoverNode(curMapData.x, j);
                }
                break;
            case Forward.Down:
                for (int j = curMapData.y + 1; j < maxX; j++)
                {
                    if (!IsCanThrough(curMapData.mapNode[curMapData.x, j]))
                        break;
                    curMapData.y = j;
                    CoverNode(curMapData.x, j);
                }
                break;
        }
        UpdateLevel();
        CheckLevel();
    }

    public void CheckLevel()
    {
        bool isComplete = true;
        for (int i = 0; i < curMapData.mapNode.GetLength(0); i++)
            for (int j = 0; j < curMapData.mapNode.GetLength(1); j++)
                if (curMapData.mapNode[i, j] != NodeState.None && !IsCoverd(curMapData.mapNode[i, j]))
                {
                    isComplete = false;
                }
        if (isComplete)
        {
            LevelComplete();
            return;
        }

        bool isFailure = true;
        int[] xx = new int[4] { 1, -1, 0, 0 };
        int[] yy = new int[4] { 0, 0, 1, -1 };
        int x, y;
        for (int i = 0; i < 4; i++)
        {
            x = curMapData.x + xx[i];
            y = curMapData.y + yy[i];
            if (x < 0 || x >= maxX)
                continue;
            if (y < 0 || y >= maxY)
                continue;
            if (IsCanThrough(curMapData.mapNode[x, y]))
                isFailure = false;
        }
        if (isFailure)
        {
            LevelFailur();
        }
    }

    public void LevelComplete()
    {

    }

    public void LevelFailur()
    {

    }

    public void RestartLevel()
    {

    }

    private bool IsCanThrough(NodeState nodeState)
    {
        return nodeState == NodeState.Empty || nodeState == NodeState.Shadow || nodeState == NodeState.CoveredShadow;
    }

    private bool IsCoverd(NodeState nodeState)
    {
        return nodeState == NodeState.Covered || nodeState == NodeState.CoveredShadow;
    }

    private void CoverNode(int x, int y)
    {
        if (curMapData.mapNode[x, y] == NodeState.Empty)
            curMapData.mapNode[x, y] = NodeState.Covered;
        if (curMapData.mapNode[x, y] == NodeState.Shadow)
            curMapData.mapNode[x, y] = NodeState.CoveredShadow;
    }

}
