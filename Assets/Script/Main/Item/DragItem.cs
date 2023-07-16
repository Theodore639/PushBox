using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IEndDragHandler
{
    private RectTransform canvas;          //得到canvas的ugui坐标
    private RectTransform imgRect;        //得到图片的ugui坐标
    Vector2 offset = new Vector3();    //用来得到鼠标和图片的差值

    // Use this for initialization
    void Start()
    {
        Init();
    }

    public void Init()
    {
        imgRect = GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
    }

    //当鼠标按下时调用 接口对应  IPointerDownHandler
    public void OnPointerDown(PointerEventData eventData)
    {
        if (canvas == null)
            Init();
        Vector2 mouseDown = eventData.position;    //记录鼠标按下时的屏幕坐标
        Vector2 mouseUguiPos = new Vector2();   //定义一个接收返回的ugui坐标
        bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mouseDown, eventData.enterEventCamera, out mouseUguiPos);
        if (isRect)   //如果在
        {
            //计算图片中心和鼠标点的差值
            offset = imgRect.anchoredPosition - mouseUguiPos;
        }
    }

    //当鼠标拖动时调用   对应接口 IDragHandler
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mouseDrag = eventData.position;   //当鼠标拖动时的屏幕坐标
        Vector2 uguiPos = new Vector2();   //用来接收转换后的拖动坐标
        //和上面类似
        bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mouseDrag, eventData.enterEventCamera, out uguiPos);
        if (isRect)
        {
            //设置图片的ugui坐标与鼠标的ugui坐标保持不变
            imgRect.anchoredPosition = offset + uguiPos;
            DargUpdate(imgRect.anchoredPosition);
        }
    }

    //当鼠标抬起时调用  对应接口  IPointerUpHandler
    public void OnPointerUp(PointerEventData eventData)
    {
        offset = Vector2.zero;
        EndDrag();
    }

    //当鼠标结束拖动时调用   对应接口  IEndDragHandler
    public void OnEndDrag(PointerEventData eventData)
    {
        offset = Vector2.zero;
    }

    public virtual void DargUpdate(Vector3 position)
    { }

    public virtual void EndDrag()
    { }

}
