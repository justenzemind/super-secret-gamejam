using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityUIDropTo : CustomDropTo
{
    [SerializeField] int _myIndex;
    [SerializeField] Image _background;
    [SerializeField] TextMeshProUGUI _text;
    
    public override void UpdateSelf<T>(T data)
    {
        AbilityInfo info = data as AbilityInfo;
        _background.color = info._color;
        _text.text = info._abilityName;

        LevelManager.instance.player.AssignAbility(_myIndex, info);
    }
}
