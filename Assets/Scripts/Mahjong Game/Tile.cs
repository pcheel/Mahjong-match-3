using System.Collections.Generic;
using UnityEngine;
using System;

public class Tile : ITile
{
    private ITileLine _tileLine;
    private List<ITile> _topTiles;
    private List<ITile> _downTiles;
    private Vector2 _position;
    private int _layer;
    private bool _isLocked;
    private TileTypes _type;

    public Vector2 position => _position;
    public List<ITile> topTiles => _topTiles;
    public TileTypes tileType => _type;
    public int layer => _layer;
    public Action<bool> ChangeTopTilesCountAction {get;set;}
    public Action<Vector2> ChangePositionAction {get;set;}
    public Action<bool> DeleteTileViewAction {get;set;}

    public Tile()
    {
        _topTiles = new List<ITile>();
        _downTiles = new List<ITile>();
    }
    public void Activate()
    {
        if (_isLocked)
        {
            return;
        }

        _isLocked = true;
        DeleteTileFromDownTiles();
        _tileLine.AddTileToLine(this);
    }
    public void Initialize(Vector2 position, int layer, TileTypes type, ITileLine tileLine)
    {
        _position = position;
        _layer = layer;
        _type = type;
        _tileLine = tileLine;
    }
    public void SetTopTiles(List<ITile> topTiles)
    {
        _topTiles = topTiles;
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
        _tileLine.ShiftPointsLeftForSpawnWithDelay();
    }

    private void DeleteTileFromDownTiles()
    {
        foreach (var tile in _downTiles)
        {
            tile.topTiles.Remove(this);
            tile.CheckTileState();
        }
    }
}
