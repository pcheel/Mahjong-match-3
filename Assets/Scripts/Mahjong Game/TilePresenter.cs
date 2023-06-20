using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePresenter
{
    private ITile _tileModel;
    private ITileView _tileView;

    public TilePresenter(ITile tileModel, ITileView tileView)
    {
        _tileModel = tileModel;
        _tileView = tileView;
        _tileModel.InitializeAction += _tileView.InitializeTileView;
        _tileModel.ChangeTopTilesCountAction += _tileView.ChangeLockState;
        _tileModel.ChangePositionAction += _tileView.MoveTileToNewPosition;
        _tileView.TapOnTileAction += _tileModel.Activate;
        _tileModel.DeleteTileViewAction += _tileView.DeleteTileView;
        _tileView.EndOfDeleteTileAction += _tileModel.EndOfTileDelete;
    }

    private void OnEnable()
    {
        
    }
    private void OnDisable() 
    {
        _tileModel.InitializeAction -= _tileView.InitializeTileView;
        _tileModel.ChangeTopTilesCountAction -= _tileView.ChangeLockState;
        _tileModel.ChangePositionAction -= _tileView.MoveTileToNewPosition;
        _tileView.TapOnTileAction -= _tileModel.Activate;
        _tileModel.DeleteTileViewAction -= _tileView.DeleteTileView;
        _tileView.EndOfDeleteTileAction -= _tileModel.EndOfTileDelete;

    }
}