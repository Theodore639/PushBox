using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public SpriteRenderer dogSpriteRenderer;
    public SpriteRenderer blockSpriteRenderer;
    public Sprite emptyBlock, shadowBlock;
    public Sprite dog, shadowDog;


    public void SetState(NodeState state)
    {
        switch (state)
        {
            case NodeState.None:
                dogSpriteRenderer.sprite = null;
                blockSpriteRenderer.sprite = null;
                break;
            case NodeState.Empty:
                dogSpriteRenderer.sprite = null;
                blockSpriteRenderer.sprite = emptyBlock;
                break;
            case NodeState.Covered:
                dogSpriteRenderer.sprite = dog;
                blockSpriteRenderer.sprite = emptyBlock;
                break;
            case NodeState.Shadow:
                dogSpriteRenderer.sprite = null;
                blockSpriteRenderer.sprite = shadowBlock;
                break;
            case NodeState.CoveredShadow:
                dogSpriteRenderer.sprite = shadowDog;
                blockSpriteRenderer.sprite = shadowBlock;
                break;
        }
    }
}
