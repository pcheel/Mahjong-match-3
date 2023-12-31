using System.Collections.Generic;
using UnityEngine;

public interface IFactory
{
    public ITile CreateTileModel();
    public ITileView CreateTileView(GameObject tilePrefab, Transform parent);
    public TilePresenter CreateTilePresenter(ITile tileModel, ITileView tileView, ITileLine _tileLine, Vector2 position, int layer, TileTypes type, TileManager tileManager);
    public ITileLine CreateTileLineModel(List<Vector2> linePoints, TileManager tileManager);
    public ITileLineView CreateTileLineView(GameObject tileLinePrefab, Transform parent);
    public ILevelManager CreateLevelManager(TileManager tileManager);
    public ILevelMap CreateLevelMapModel(LevelCreatorManager levelCreatorManager ,IFactory factory, GameObject tilePointPrefab);
    public ILevelMapView CreateLevelMapView(GameObject levelMapPrefab, Transform parent);
    public LevelMapPresenter CreateLevelMapPresenter(ILevelMap levelMap, ILevelMapView _levelMapView);
    public ITilePoint CreateTilePointModel();
    public ITilePointView CreateTilePointView(GameObject tilePointPrefab, Transform parent);
    public TilePointPresenter CreateTilePointPresenter(ITilePoint tilePoint, ITilePointView tilePointView, Vector2 position);
}
