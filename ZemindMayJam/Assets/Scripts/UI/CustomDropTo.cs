using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomDropTo : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (UiManager.instance.isDragging)
        {
            UiManager.instance.dropTo = this.gameObject;
        }
        Debug.Log(name + " OnPointerEnter()");
    }

    public virtual void UpdateSelf<T>(T data) where T : class
    {

    }
}
