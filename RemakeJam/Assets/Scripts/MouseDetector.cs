using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public bool isMouseSelected;
	void Start () {
        isMouseSelected = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseSelected = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseSelected = false;
    }
}
