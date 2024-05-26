using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityInfo
{
    public string _abilityName;
    public Color _color;
    public System.Action _callback;

    public Dictionary<AbilityKey, System.Func<AbilityInfo>> _abilityMap = new Dictionary<AbilityKey, System.Func<AbilityInfo>> ()
    {
        { AbilityKey.DiceLightning, DiceLightning },
        { AbilityKey.DicePush, DicePush},
        { AbilityKey.DicePull, DicePull},
        { AbilityKey.DiceMindTrick, DiceMindTrick},
        { AbilityKey.DiceTimeSlip, DiceTimeSlip},
        { AbilityKey.DiceHeal, DiceHeal},
    };


    public enum AbilityKey
    {
        DiceLightning,
        DicePush,
        DicePull,
        DiceMindTrick,
        DiceTimeSlip,
        DiceHeal
    }

    public AbilityInfo(AbilityKey key)
    {
        AbilityInfo info = _abilityMap[key]();

        if(info == null)
        {
            return;
        }

        _abilityName = info._abilityName;
        _color = info._color;
        _callback = info._callback;
    }

    public AbilityInfo(string name, Color color, System.Action callback)
    {
        _abilityName = name;
        _color = color;
        _callback = callback;
    }

    public static AbilityInfo DiceLightning()
    {
        return new AbilityInfo("Dice Lightning", Color.blue, 
            () =>
            {
                Debug.Log("Dice <color=blue>Lightning!</color>");
            });
    }

    public static AbilityInfo DicePush()
    {
        return new AbilityInfo("Dice Push", Color.cyan, 
        () =>
        {
            Debug.Log("Dice <color=cyan>Push!</color>");
        });
    }

    public static AbilityInfo DicePull()
    {
        return new AbilityInfo("Dice Pull", Color.yellow,
        () =>
        {
            Debug.Log("Dice <color=yellow>Pull!</color>");
        });
    }

    public static AbilityInfo DiceMindTrick()
    {
        return new AbilityInfo("Dice Mind Trick", Color.magenta,
        () =>
        {
            Debug.Log("Dice <color=magenta>Mind Trick!</color>");
        });
    }

    public static AbilityInfo DiceTimeSlip()
    {
        return new AbilityInfo("Dice Time Slip", Color.white,
        () =>
        {
            Debug.Log("Dice <color=white>Time Slip!</color>");
        });
    }

    public static AbilityInfo DiceHeal()
    {
        return new AbilityInfo("Dice Heal", Color.green,
        () =>
        {
            Debug.Log("Dice <color=green>Heal!</color>");
        });
    }
}
