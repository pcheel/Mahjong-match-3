using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralTilePoint : ITilePoint
{
    private Vector2 _position;
    private bool _isOccupied;

    public Vector2 position => _position;

    public bool isOccupied => _isOccupied;

    public void Activate()
    {
        
    }
}
