using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public delegate void OnPointerEnterDelegate(string objectName, PointerEventData eventData);
public delegate void OnPointerExitDelegate(string objectName, PointerEventData eventData);

public class UIInfo : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onPointerEnterDelegate != null)
        {
            onPointerEnterDelegate(name, eventData);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (onPointerExitDelegate != null)
        {
            onPointerExitDelegate(name, eventData);
        }
    }
}
