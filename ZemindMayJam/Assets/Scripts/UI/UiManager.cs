using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public bool isDragging = false;
    public GameObject dropTo;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
            return;
        }
        Destroy(this.gameObject);
    }

    public void UpdateDropTo<T>(T component) where T : class
    {
        dropTo?.GetComponent<CustomDropTo>().UpdateSelf<T>(component);
    }


}
