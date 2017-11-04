using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public delegate void OnPointerClickDelegate(GameObject obj, PointerEventData eventData);
public delegate void OnPointerEnterDelegate(GameObject obj, PointerEventData eventData);
public delegate void OnPointerExitDelegate(GameObject obj, PointerEventData eventData);

public class UIMouseDelegate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    public OnPointerClickDelegate onPointerClickDelegate;
    public OnPointerEnterDelegate onPointerEnterDelegate;
    public OnPointerExitDelegate onPointerExitDelegate;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (onPointerClickDelegate != null)
        {
            onPointerClickDelegate(gameObject, eventData);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onPointerEnterDelegate != null)
        {
            onPointerEnterDelegate(gameObject, eventData);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (onPointerExitDelegate != null)
        {
            onPointerExitDelegate(gameObject, eventData);
        }
    }
}
