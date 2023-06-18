using System.Collections;
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
    public TilePresenter CreateTilePresenter(ITile tileModel, ITileView tileView)
    {
        return new TilePresenter(tileModel, tileView);
    }
    public ITileLine CreateTileLineModel(List<Vector2> linePoints)
    {
        return new GeneralTileLine(linePoints);
        // return null;
    }
    public ITileLineView CreateTileLineView(GameObject tileLinePrefab, Transform parent)
    {
        GameObject tileLineViewGO = Instantiate(tileLinePrefab, parent);
        return tileLineViewGO.GetComponent<ITileLineView>();
    }
    public TileLinePresenter CreateTileLinePresenter(ITileLine tileLineModel, ITileLineView tileLineView)
    {
        return new TileLinePresenter(tileLineModel, tileLineView);
    }
}
