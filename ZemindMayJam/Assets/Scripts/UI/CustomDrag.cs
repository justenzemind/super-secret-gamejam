using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomDrag : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] RectTransform rect;
    [SerializeField] Vector2 originalPosition;
    [SerializeField] CanvasGroup group;

    //Detect if a click occurs
    public virtual void OnPointerClick(PointerEventData pointerEventData)
    {
        //Debug.Log(name + " OnPointerClick()");
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        UiManager.instance.isDragging = true;
        UiManager.instance.dropTo = null;
        Image myImage = rect.GetComponent<Image>();
        myImage.enabled = false;
        originalPosition = rect.anchoredPosition;
        this.GetComponent<CanvasGroup>().blocksRaycasts = false;
        //Debug.Log(name + " OnBeginDrag()");
    }

    public virtual void OnDrag(PointerEventData data)
    {
        rect.position = Input.mousePosition;
        //Debug.Log(name + " OnDrag()");
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        UiManager.instance.isDragging = false;
        rect.GetComponent<Image>().enabled = true;
        rect.anchoredPosition = originalPosition;
        //Debug.Log(name + " OnEndDrag()");
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log(name + " OnPointerEnter()");
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log(name + " OnPointerExit()");
    }
}
