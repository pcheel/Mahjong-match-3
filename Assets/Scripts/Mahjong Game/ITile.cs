using UnityEngine;
using System.Collections.Generic;
using System;

public interface ITile
{
    public Action<bool> ChangeTopTilesCountAction {get;set;}
    public Action<Vector2> ChangePositionAction {get;set;}
    public Action<bool> DeleteTileViewAction {get;set;}
    public Vector2 position {get;}
    public List<ITile> topTiles {get;}
    public TileTypes tileType {get;}
    public int layer {get;}
    public void Activate();
    public void Initialize(Vector2 position, int layer, TileTypes type, ITileLine tileLine);
    public void SetTopTiles(List<ITile> topTiles);
    public void SetDownTiles(List<ITile> downTiles);
    public void CheckTileState();
    public void ChangePosition(Vector2 newPosition);
    public void EndOfTileDelete();
}
