using System.Collections.Generic;
using UnityEngine;
using System;

public class GeneralTileLine : ITileLine
{
    private List<ITile> _tilesInLine;
    private List<ITile> _tilesToDelete;
    private List<Vector2> _tilesPositions;
    private List<int> _indexes;
    private TileManager _tileManager;
    private const int MATCH_NUMBER = 3;
    private const float DISTANCE_BETWEEN_TILES_IN_LINE = 1.11f;

    public Action LoseLevelAction {get;set;}

    public GeneralTileLine(List<Vector2> tilesPositions, TileManager tileManager)
    {
        _tilesInLine = new List<ITile>();
        _tilesToDelete = new List<ITile>();
        _indexes = new List<int>();
        _tilesPositions = tilesPositions;
        _tileManager = tileManager;
    }
    public void AddTileToLine(ITile tile)
    {
        if (_tilesPositions.Count == _tilesInLine.Count)
        {
            LoseLevelAction?.Invoke();
            ClearTileLine();
            return;
        }

        for (int i = _tilesInLine.Count - 1; i >= 0; i--)
        {
            if (_tilesInLine[i].tileType == tile.tileType)
            {
                _tilesInLine.Add(_tilesInLine[_tilesInLine.Count - 1]);
                for (int j = _tilesInLine.Count - 2; j > i + 1; j--)
                {
                    _tilesInLine[j] = _tilesInLine[j - 1];
                }
                _tilesInLine[i + 1] = tile;
                HandleAfterAppendTileToTileline(tile);
                return;
            }
        }
        _tilesInLine.Add(tile);
        HandleAfterAppendTileToTileline(tile);
    }
    public void ChangeTilePositions()
    {
        for (int i = 0; i < _tilesInLine.Count; i++)
        {
            _tilesInLine[i].ChangePosition(_tilesPositions[i]);
        }
    }
    public void ShiftPointsLeftForSpawnWithDelay()
    {
        ShiftPointsForSpawn(false);
    }

    private void CheckMatch()
    {
        int matchCounter = 0;
        TileTypes lastType = TileTypes.None;
        for (int i = 0; i < _tilesInLine.Count; i++)
        {
            if (_tilesInLine[i].tileType == lastType)
            {
                matchCounter++;
                if (matchCounter == MATCH_NUMBER)
                {
                    matchCounter = 0;
                    _indexes.Add(i - 2);
                    DeleteMatchingTileViews(lastType);
                    ShiftPointsForSpawn(true);
                }
            }
            else
            {
                lastType = _tilesInLine[i].tileType;
                matchCounter = 1;
            }
        }
    }
    private void DeleteMatchingTileViews(TileTypes matchType)
    {
        for (int i = _tilesInLine.Count - 1; i >= 0; i--)
        {
            if (_tilesInLine[i].tileType == matchType)
            {
                _tilesInLine[i].DeleteTileViewAction?.Invoke(true);
                _tilesInLine.RemoveAt(i);
            }
        }
    }
    private void ShiftPointsForSpawn(bool isRightShift)
    {
        if (isRightShift)
        {
            for (int i = _indexes[_indexes.Count - 1]; i < _tilesPositions.Count; i++)
            {
                _tilesPositions[i] += new Vector2(3 * DISTANCE_BETWEEN_TILES_IN_LINE, 0f);
            }
        }
        else
        {
            int index = _indexes[0];
            _indexes.RemoveAt(0);
            for (int i = index; i < _tilesPositions.Count; i++)
            {
                _tilesPositions[i] -= new Vector2(3 * DISTANCE_BETWEEN_TILES_IN_LINE, 0f);
            }
        }
        ChangeTilePositions();
    }
    private void HandleAfterAppendTileToTileline(ITile tile)
    {
        ChangeTilePositions();
        CheckMatch();
        _tileManager.RemoveTileFromMapAndCheckWinLevel(tile);
    }
    private void ClearTileLine()
    {
        foreach(var tile in _tilesInLine)
        {
            tile.DeleteTileViewAction?.Invoke(false);
        }
        _tilesInLine.Clear();
    }
}
