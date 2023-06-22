using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ITilePointView
{
    public Action TapOnTilePointAction {get;set;}
    public void InitializationView(Vector2 position);
    public void ChangeIcon(bool isEmpty);
}
