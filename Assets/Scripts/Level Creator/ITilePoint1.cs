using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITilePoint
{
    public Vector2 position{get;}
    public bool isOccupied{get;}
    public void Activate();
}
