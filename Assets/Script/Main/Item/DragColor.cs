using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragColor : DragItem
{
    Vector3 rotation = new Vector3(0, 0, 1);
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation);
    }

    public override void DargUpdate(Vector2 position)
    {
        base.DargUpdate(position);
        LevelManager.Instance.ColorDragUpdate(position);
    }

    public override void EndDrag()
    {
        base.EndDrag();
        LevelManager.Instance.ColorDragEnd();
    }
}
