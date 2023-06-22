using System.Collections.Generic;
using UnityEngine;

public class GeneralFactory : MonoBehaviour, IFactory
{
    public ITile CreateTileModel()
    {
        return new Tile();
    }
    public ITileView CreateTileView(GameObject tilePrefab, Transform parent)
    {
        GameObject tileGO = Instantiate(tilePrefab, parent);
        return tileGO.GetComponent<ITileView>();
    }
    public TilePresenter CreateTilePresenter(ITile tileModel, ITileView tileView, ITileLine tileLine, Vector2 position, int layer, TileTypes type, TileManager tileManager)
    {
        return new TilePresenter(tileModel, tileView, tileLine, position, layer, type, tileManager);
    }
    public ITileLine CreateTileLineModel(List<Vector2> linePoints, TileManager tileManager)
    {
        return new GeneralTileLine(linePoints, tileManager);
    }
    public ITileLineView CreateTileLineView(GameObject tileLinePrefab, Transform parent)
    {
        GameObject tileLineViewGO = Instantiate(tileLinePrefab, parent);
        return tileLineViewGO.GetComponent<ITileLineView>();
    }
    public ILevelManager CreateLevelManager(TileManager tileManager)
    {
        return new LevelManager(tileManager);
    }
    public ILevelMap CreateLevelMapModel(LevelCreatorManager levelCreatorManager ,IFactory factory, GameObject tilePointPrefab)
    {
        return new GeneralLevelMap(levelCreatorManager, factory, tilePointPrefab);
    }
    public ILevelMapView CreateLevelMapView(GameObject levelMapPrefab, Transform parent)
    {
        GameObject levelMapGO = Instantiate(levelMapPrefab, parent);
        return levelMapGO.GetComponent<ILevelMapView>();
    }
    public LevelMapPresenter CreateLevelMapPresenter(ILevelMap levelMap, ILevelMapView levelMapView)
    {
        return new LevelMapPresenter(levelMap, levelMapView);
    }
    public ITilePoint CreateTilePointModel()
    {
        return new GeneralTilePoint();
    }
    public ITilePointView CreateTilePointView(GameObject tilePointPrefab, Transform parent)
    {
        GameObject tilePointGO = Instantiate(tilePointPrefab, parent);
        return tilePointGO.GetComponent<ITilePointView>();
    }
    public TilePointPresenter CreateTilePointPresenter(ITilePoint tilePoint, ITilePointView tilePointView, Vector2 position)
    {
        return new TilePointPresenter(tilePoint, tilePointView, position);
    }
}
