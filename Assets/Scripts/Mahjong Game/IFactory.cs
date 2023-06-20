using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactory
{
    public ITile CreateTileModel();
    public ITileView CreateTileView(GameObject tilePrefab, Transform parent);
    public TilePresenter CreateTilePresenter(ITile tileModel, ITileView tileView);
    public ITileLine CreateTileLineModel(List<Vector2> linePoints);
    public ITileLineView CreateTileLineView(GameObject tileLinePrefab, Transform parent);
    public ILevelManager CreateLevelManager(TileManager tileManager);
}
