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
        _tileModel.DeleteMatchTileViewAction += _tileView.DeleteMatchTileView;
        _tileView.EndOfDeleteTileAction += _tileModel.EndOfTileDelete;
        _tileModel.DeleteTileViewAction += _tileView.DeleteTileView;
    }

    private void OnDisable() 
    {
        _tileModel.InitializeAction -= _tileView.InitializeTileView;
        _tileModel.ChangeTopTilesCountAction -= _tileView.ChangeLockState;
        _tileModel.ChangePositionAction -= _tileView.MoveTileToNewPosition;
        _tileView.TapOnTileAction -= _tileModel.Activate;
        _tileModel.DeleteMatchTileViewAction -= _tileView.DeleteMatchTileView;
        _tileView.EndOfDeleteTileAction -= _tileModel.EndOfTileDelete;
        _tileModel.DeleteTileViewAction -= _tileView.DeleteTileView;
    }
}
