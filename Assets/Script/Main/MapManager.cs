using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager
{
    private static MapManager _instance;
    public static MapManager Instance
    {
        get
        {
            _instance ??= new MapManager();
            return _instance;
        }
    }

    static List<MapData> mapDataList;    

    public static MapData GetMapData(int level)
    {
        if (level < mapDataList.Count)
        {
            return mapDataList[level].Clone();
        }
        else
        {
            Debug.LogWarning("MapManager.GetMapData error, level out of range");
            return new MapData();
        }
    }

    public static void ReadAllMap()
    {
        mapDataList = new List<MapData>();
        MapData mapData = new()
        {
            levelId = 0,
            mapNode = new NodeState[3, 3] {
                { NodeState.Empty,NodeState.Empty,NodeState.Empty, },
                { NodeState.Empty,NodeState.Empty,NodeState.Empty, },
                { NodeState.Empty,NodeState.Empty,NodeState.Empty, },
            },
            x = 1,
            y = 1
        };
        mapData = new()
        {
            levelId = 0,
            mapNode = new NodeState[4, 4] {
                { NodeState.Empty,NodeState.Empty,NodeState.Shadow, NodeState.Empty, },
                { NodeState.Empty,NodeState.Empty,NodeState.Empty, NodeState.Empty, },
                { NodeState.Empty,NodeState.Empty,NodeState.Empty, NodeState.Empty, },
                { NodeState.Empty,NodeState.Shadow,NodeState.Empty, NodeState.Empty, },
            },
            x = 1,
            y = 1
        };
        mapDataList.Add(mapData);   
    }

    public static int GetMapCount()
    {
        return mapDataList.Count;
    }
}

public struct MapData
{
    public int levelId;
    public NodeState[,] mapNode;
    public int x, y;//起点坐标

    public MapData Clone()
    {
        MapData mapData = new()
        {
            levelId = levelId,
            mapNode = new NodeState[mapNode.GetLength(0), mapNode.GetLength(1)],
            x = x,
            y = y
        };
        for(int i = 0; i < mapNode.GetLength(0); i++)
            for (int j = 0; j < mapNode.GetLength(1); j++)
            {
                mapData.mapNode[i, j] = mapNode[i, j];
            }
        return mapData;
    }
}

public enum NodeState
{
    None = 0,//什么都没有，不可达
    Empty = 1,
    Covered = 2,
    Shadow = 3,
    CoveredShadow = 4,
    Stone = 5,
}
