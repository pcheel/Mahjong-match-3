using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GeneralTilePoint : ITilePoint
{
    private Vector2 _position;
    private bool _isEmpty;

    public Vector2 position => _position;
    public bool isEmpty => _isEmpty;
    public Action<bool> ActivatePointAction {get;set;}

    public GeneralTilePoint()
    {
        _isEmpty = true;
    }
    public void Activate()
    {
        if (_isEmpty)
        {
            _isEmpty = false;
        }
        else
        {
            _isEmpty = true;
        }
        ActivatePointAction?.Invoke(_isEmpty);
    }
    public void Initialization(Vector2 position)
    {
        _position = position;
    }
}
