using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ITilePoint
{
    public Vector2 position{get;}
    public bool isEmpty{get;}
    public Action<bool> ActivatePointAction {get;set;}
    public void Activate();
    public void Initialization(Vector2 position);
}
