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


}

//public struct MapData
//{
//    public int levelId;
//    public NodeState[,] mapNode;
//    public int x, y;//起点坐标

//    public MapData Clone()
//    {
//        MapData mapData = new()
//        {
//            levelId = levelId,
//            mapNode = new NodeState[mapNode.GetLength(0), mapNode.GetLength(1)],
//            x = x,
//            y = y
//        };
//        for (int i = 0; i < mapNode.GetLength(0); i++)
//            for (int j = 0; j < mapNode.GetLength(1); j++)
//            {
//                mapData.mapNode[i, j] = mapNode[i, j];
//            }
//        return mapData;
//    }
//}

public enum NodeState
{
    None = 0,//什么都没有，不可达
    Empty = 1,
    Covered = 2,
    Shadow = 3,
    CoveredShadow = 4,
    Stone = 5,
}
