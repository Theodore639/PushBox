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
    public int score;

    public List<Sprite> spriteList;
    public Sprite allColor;
    public GameObject block;
    public List<Block> blocks;
    public MapState mapState;
    int[,] blockState;
    int maxX = 5;
    int maxY = 5;
    int InitBlockNum = 5;
    float allColorRate = 0.1f;

    private void Awake()
    {

    }

    public void InitLevel()
    {
        blocks = new List<Block>();
        blockState = new int[maxX, maxY];
        score = 0;
        for (int i = 0; i < InitBlockNum; i++)
        {
            CreateRandomBlock();
        }
    }

    public void SetPosition(Transform t, int x, int y)
    {
        t.localPosition = new Vector3(maxX / -2.0f + x + 0.5f, maxY / 2.0f - y - 0.5f, 0) * 128;
    }

    private void Update()
    {
        if (mapState == MapState.Wait)
        {
            Forward f = ControllerManager.Instance.GetCommand();
            if (f != Forward.None)
            {
                StartCoroutine(Move(f));
            }
        }
    }

    IEnumerator Move(Forward forward)
    {
        if (MoveMap(forward))
        {
            mapState = MapState.Moving;
            yield return new WaitForSeconds(CONST.MoveAnimationTime);
            if (CheckMap())
            {
                mapState = MapState.Cleaning;
                yield return new WaitForSeconds(CONST.ClearAnimationTime);
            }
            else
            {
                mapState = MapState.Spwaning;
                CreateRandomBlock();
                yield return new WaitForSeconds(CONST.SpwanAnimationTime);
                if (CheckMap())
                {
                    mapState = MapState.Cleaning;
                    yield return new WaitForSeconds(CONST.ClearAnimationTime);
                }
            }
            mapState = MapState.Wait;
        }
        yield return 0;
    }

    private bool MoveMap(Forward forward)
    {
        bool isMoving = false;
        foreach (Block block in blocks)
        {
            int count = 0;
            switch (forward)
            {
                case Forward.Left:
                    for (int i = block.x - 1; i >= 0; i--)
                    {
                        if (blockState[i, block.y] == 0)
                            count++;
                    }
                    block.MoveTo(block.x - count, block.y);
                    break;
                case Forward.Right:
                    for (int i = block.x + 1; i < maxX; i++)
                    {
                        if (blockState[i, block.y] == 0)
                            count++;
                    }
                    block.MoveTo(block.x + count, block.y);
                    break;
                case Forward.Up:
                    for (int i = block.y - 1; i >= 0; i--)
                    {
                        if (blockState[block.x, i] == 0)
                            count++;
                    }
                    block.MoveTo(block.x, block.y - count);
                    break;
                case Forward.Down:
                    for (int i = block.y + 1; i < maxY; i++)
                    {
                        if (blockState[block.x, i] == 0)
                            count++;
                    }
                    block.MoveTo(block.x, block.y + count);
                    break;
            }
            if (count > 0)
                isMoving = true;
        }
        if (isMoving)
        {
            for (int i = 0; i < maxX; i++)
                for (int j = 0; j < maxY; j++)
                    blockState[i, j] = 0;
            foreach (Block block in blocks)
                blockState[block.x, block.y] = 1;
        }
        return isMoving;
    }

    /// <summary>
    /// 检查是否有可消除的block
    /// </summary>
    /// <returns></returns>
    public bool CheckMap()
    {
        int scorePoint = 0;
        bool isClear = false;
        foreach (Block block in blocks)
        {
            CheckBlock(block);
        }
        List<Block> clearList = new List<Block>();
        foreach (Block block in blocks)
        {
            if (blockState[block.x, block.y] > 1)
            {
                scorePoint += blockState[block.x, block.y] - 1;
                if (blockState[block.x, block.y] >= 3)
                    scorePoint += 2;
                clearList.Add(block);
                blockState[block.x, block.y] = 0;
                isClear = true;
            }
        }
        for (int i = clearList.Count - 1; i >= 0; i--)
        {
            clearList[i].Clear();
        }
        return isClear;
    }

    private void CheckBlock(Block block)
    {
        int x = block.x;
        int y = block.y;
        Block blockA, blockB;
        if (x > 0 && x < maxX - 1)
        {
            blockA = FindBlock(x - 1, y);
            blockB = FindBlock(x + 1, y);
            if (blockA != null && blockB != null && CheckColor(block, blockA) && CheckColor(block, blockB) && CheckColor(blockA, blockB))
            {
                blockState[block.x, block.y]++;
                blockState[blockA.x, blockA.y]++;
                blockState[blockB.x, blockB.y]++;
            }
        }
        if (y > 0 && y < maxY - 1)
        {
            blockA = FindBlock(x, y - 1);
            blockB = FindBlock(x, y + 1);
            if (blockA != null && blockB != null && CheckColor(block, blockA) && CheckColor(block, blockB) && CheckColor(blockA, blockB))
            {
                blockState[block.x, block.y]++;
                blockState[blockA.x, blockA.y]++;
                blockState[blockB.x, blockB.y]++;
            }
        }
    }

    private bool CheckColor(Block a, Block b)
    {
        if (a.color == BlockColor.AllColor || b.color == BlockColor.AllColor)
            return true;
        return a.color == b.color;
    }

    public void LevelFailur()
    {

    }

    public void ReDo()
    {

    }

    private Block FindBlock(int x, int y)
    {
        return blocks.Find(block => block.x == x && block.y == y);
    }

    private void CreateRandomBlock()
    {
        Block b = Instantiate(block, transform).GetComponent<Block>();
        int x = 0;
        int y = 0;
        GetRandomEmptyPosition(ref x, ref y);
        b.OnSpwan(GetRandomColor(), x, y);
        blockState[x, y] = 1;
        blocks.Add(b);
    }

    private BlockColor GetRandomColor()
    {
        if (Random.Range(0, 1.0f) < allColorRate)
            return BlockColor.AllColor;
        return (BlockColor)Random.Range(0, (int)BlockColor.ColorEnd);
    }

    private void GetRandomEmptyPosition(ref int x, ref int y)
    {
        List<int> result = new List<int>();
        for (int i = 0; i < maxX; i++)
            for (int j = 0; j < maxY; j++)
            {
                if (blockState[i, j] == 0)
                {
                    result.Add(i * maxY + j);
                }
            }
        if (result.Count > 0)
        {
            int r = result[Random.Range(0, result.Count)];
            x = r / maxY;
            y = r % maxY;
        }

    }


}
