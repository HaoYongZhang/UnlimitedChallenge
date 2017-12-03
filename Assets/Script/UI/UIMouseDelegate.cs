using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public delegate void OnPointerClickDelegate(GameObject obj, PointerEventData eventData);
public delegate void OnPointerDoubleClickDelegate(GameObject obj, PointerEventData eventData);
public delegate void OnPointerEnterDelegate(GameObject obj, PointerEventData eventData);
public delegate void OnPointerExitDelegate(GameObject obj, PointerEventData eventData);

public delegate void OnBeginDragDelegate(GameObject obj, PointerEventData eventData);
public delegate void OnDragDelegate(GameObject obj, PointerEventData eventData);
public delegate void OnEndDragDelegate(GameObject obj, PointerEventData eventData);
public delegate void OnDropDelegate(GameObject obj, PointerEventData eventData);

public class UIMouseDelegate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public OnPointerClickDelegate onPointerClickDelegate;
    public OnPointerDoubleClickDelegate onPointerDoubleClickDelegate;
    public OnPointerEnterDelegate onPointerEnterDelegate;
    public OnPointerExitDelegate onPointerExitDelegate;

    public OnBeginDragDelegate onBeginDragDelegate;
    public OnDragDelegate onDragDelegate;
    public OnEndDragDelegate onEndDragDelegate;
    public OnDropDelegate onDropDelegate;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 鼠标点击事件
    /// </summary>
    /// <param name="eventData">Event data.</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        //双击
        if (eventData.clickCount == 2)
        {
            if (onPointerDoubleClickDelegate != null)
            {
                onPointerDoubleClickDelegate(gameObject, eventData);
            }
        }
        //单击
        else
        {
            if (onPointerClickDelegate != null)
            {
                onPointerClickDelegate(gameObject, eventData);
            }

        }

    }

    /// <summary>
    /// 鼠标移入事件
    /// </summary>
    /// <param name="eventData">Event data.</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onPointerEnterDelegate != null)
        {
            onPointerEnterDelegate(gameObject, eventData);
        }
    }

    /// <summary>
    /// 鼠标移出事件
    /// </summary>
    /// <param name="eventData">Event data.</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (onPointerExitDelegate != null)
        {
            onPointerExitDelegate(gameObject, eventData);
        }
    }

    /// <summary>
    /// 鼠标开始拖动
    /// </summary>
    /// <param name="eventData">Event data.</param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (onBeginDragDelegate != null)
        {
            onBeginDragDelegate(gameObject, eventData);
        }
    }

    /// <summary>
    /// 鼠标拖动中
    /// </summary>
    /// <param name="eventData">Event data.</param>
    public void OnDrag(PointerEventData eventData)
    {
        if (onDragDelegate != null)
        {
            onDragDelegate(gameObject, eventData);
        }

    }

    /// <summary>
    /// 鼠标结束拖动
    /// </summary>
    /// <param name="eventData">Event data.</param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (onEndDragDelegate != null)
        {
            onEndDragDelegate(gameObject, eventData);
        }
    }

    /// <summary>
    /// 鼠标放开拖动
    /// </summary>
    /// <param name="eventData">Event data.</param>
    public void OnDrop(PointerEventData eventData)
    {
        if (onDropDelegate != null)
        {
            onDropDelegate(gameObject, eventData);
        }
    }
}
