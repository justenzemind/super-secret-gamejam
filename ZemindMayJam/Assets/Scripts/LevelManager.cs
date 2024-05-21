using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Dice player;

    public AbilityInfo.AbilityKey[] _abilitiesInPlay;

    private void Start()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    }
}
