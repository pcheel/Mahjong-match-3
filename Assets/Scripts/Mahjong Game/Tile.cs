using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tile : ITile
{
    private TileManager _tileManager;
    private List<ITile> _topTiles;
    private List<ITile> _downTiles;
    private Vector2 _position;
    private int _layer;
    private bool _isLocked;
    private TileTypes _type;

    public Vector2 position => _position;
    public List<ITile> topTiles => _topTiles;
    public TileTypes tileType => _type;
    public bool isLocked => _isLocked;
    public int layer => _layer;
    // public List<ITile> downTiles => _downTiles;
    public Action<Vector2, int, Sprite> InitializeAction {get;set;}
    public Action<bool> ChangeTopTilesCountAction {get;set;}
    public Action<Vector2> ChangePositionAction {get;set;}
    public Action DeleteTileViewAction {get;set;}
    public Action KillMovingTweensAction {get;set;}

    public Tile()
    {
        _topTiles = new List<ITile>();
        _downTiles = new List<ITile>();
    }
    public void Activate()
    {
        if (!_isLocked)
        {
            _isLocked = true;
            DeleteTileFromDownTiles();
            _tileManager.AddTileToLine(this);
        }
    }
    public void Initialize(TileData data, int layer, TileTypes type, TileManager tileManager)
    {
        _position = data.position;
        _layer = layer;
        _tileManager = tileManager;
        _type = type;
        InitializeAction?.Invoke(_position, layer, _tileManager.GetTileIcon(_type));
    }
    public void SetTopTiles(List<ITile> topTiles)
    {
        _topTiles = topTiles;
        // ChangeTopTilesCount();
    }
    public void SetDownTiles(List<ITile> downTiles)
    {
        _downTiles = downTiles;
    }
    public void CheckTileState()
    {
        _isLocked = _topTiles.Count == 0 ? false : true;
        ChangeTopTilesCountAction?.Invoke(_isLocked);
    }
    public void ChangePosition(Vector2 newPosition)
    {
        _position = newPosition;
        ChangePositionAction?.Invoke(newPosition);
    }
    public void EndOfTileDelete()
    {
        _tileManager.ShiftLine();
    }
    // public void DeleteTile()
    // {
    //     // DeleteTileVAction?.Invoke();
    //     _tileManager.SortingTileLine();
    // }
    // public void KillMovingTweens()
    // {
    //     KillMovingTweensAction?.Invoke();
    // }

    private void DeleteTileFromDownTiles()
    {
        foreach (var tile in _downTiles)
        {
            tile.topTiles.Remove(this);
            tile.CheckTileState();
        }
    }
}
