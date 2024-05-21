using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command
{
    public string _name;
    System.Action _callback;
    
    public Command(System.Action callback, string name)
    {
        _callback = callback;
        _name = name;
    }

    public virtual void Execute()
    {
        if (_callback != null)
            _callback();
    }
}
