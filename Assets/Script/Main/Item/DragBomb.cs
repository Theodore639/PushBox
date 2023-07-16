using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragBomb : DragItem
{
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        Init();
    }

    public override void DargUpdate(Vector3 position)
    {
        base.DargUpdate(position);
        LevelManager.Instance.BombDragUpdate(position);
    }

    public override void EndDrag()
    {
        base.EndDrag();
        LevelManager.Instance.BombDragEnd();
    }
}
