using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePointPresenter
{
    private ITilePoint _tilePointModel;
    private ITilePointView _tilePointView;

    public TilePointPresenter(ITilePoint tilePoint, ITilePointView tilePointView, Vector2 position)
    {
        _tilePointModel = tilePoint;
        _tilePointView = tilePointView;
        _tilePointModel.Initialization(position);
        _tilePointView.InitializationView(position);
        Enable();
    }
    public void TapOnTilePoint()
    {
        _tilePointModel.Activate();
    }
    public void ActivateTilePointView(bool isEmpty)
    {
        _tilePointView.ChangeIcon(isEmpty);
    }
    public void Enable() 
    {
        _tilePointView.TapOnTilePointAction += TapOnTilePoint;
        _tilePointModel.ActivatePointAction += ActivateTilePointView;
    }
    public void Disable() 
    {
        _tilePointView.TapOnTilePointAction -= TapOnTilePoint;
        _tilePointModel.ActivatePointAction -= ActivateTilePointView;
    }
}
