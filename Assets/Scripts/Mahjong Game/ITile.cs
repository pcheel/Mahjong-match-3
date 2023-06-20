using UnityEngine;
using System.Collections.Generic;
using System;

public interface ITile
{
    public Action<Vector2, int, Sprite> InitializeAction {get; set;}
    public Action<bool> ChangeTopTilesCountAction {get;set;}
    public Action<Vector2> ChangePositionAction {get;set;}
    public Action DeleteTileViewAction {get;set;}
    public Action DeleteMatchTileViewAction {get;set;}
    public Vector2 position {get;}
    public List<ITile> topTiles {get;}
    public TileTypes tileType {get;}
    public int layer {get;}
    public void Activate();
    public void Initialize(TileData data, int layer, TileTypes type, TileManager tileManager);
    public void SetTopTiles(List<ITile> topTiles);
    public void SetDownTiles(List<ITile> downTiles);
    public void CheckTileState();
    public void ChangePosition(Vector2 newPosition);
    public void EndOfTileDelete();
}
