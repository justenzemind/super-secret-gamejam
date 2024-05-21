using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class AbilityUI : CustomDrag
{
    public AbilityInfo.AbilityKey _ability;
    AbilityInfo _abilityInfo;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] Image _background;

    private void Start()
    {
        UpdateSelf(new AbilityInfo(_ability));
    }

    public void UpdateSelf(AbilityInfo info)
    {
        _abilityInfo = info;
        UpdateSelf();
    }

    public void UpdateSelf()
    {
        _text.text = _abilityInfo._abilityName;
        _background.color = _abilityInfo._color;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        UiManager.instance.UpdateDropTo(_abilityInfo);
    }
}