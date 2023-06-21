using UnityEngine;

public class TilePresenter
{
    private ITile _tileModel;
    private ITileView _tileView;

    public TilePresenter(ITile tileModel, ITileView tileView, ITileLine tileLine, Vector2 position, int layer, TileTypes type, TileManager tileManager)
    {
        _tileModel = tileModel;
        _tileView = tileView;
        Enable();
        _tileModel.Initialize(position, layer, type, tileLine);
        _tileView.InitializeTileView(this, position, layer, tileManager.GetTileIcon(type));
    }
    public void ChangePosition(Vector2 newPosition)
    {
        _tileView.MoveTileToNewPosition(newPosition);
    }
    public void ChangeTopTilesCount(bool isLocked)
    {
        _tileView.ChangeLockState(isLocked);
    }
    public void ActivateTile()
    {
        _tileModel.Activate();
    }
    public void DeleteTileView(bool isMatchDelete)
    {
        if (isMatchDelete)
        {
            _tileView.DeleteMatchTileView();
        }
        else
        {
            _tileView.DeleteTileView();
        }
    }
    public void EndOfDeleteTileView()
    {
        _tileModel.EndOfTileDelete();
    }
    public void Enable()
    {
        _tileModel.ChangePositionAction += ChangePosition;
        _tileModel.ChangeTopTilesCountAction += ChangeTopTilesCount;
        _tileView.TapOnTileAction += ActivateTile;
        _tileModel.DeleteTileViewAction += DeleteTileView;
        _tileView.EndOfDeleteTileAction += EndOfDeleteTileView;
    }
    public void Disable() 
    {
        _tileModel.ChangePositionAction -= ChangePosition;
        _tileModel.ChangeTopTilesCountAction -= ChangeTopTilesCount;
        _tileView.TapOnTileAction -= ActivateTile;
        _tileModel.DeleteTileViewAction -= DeleteTileView;
        _tileView.EndOfDeleteTileAction -= EndOfDeleteTileView;
    }
}
