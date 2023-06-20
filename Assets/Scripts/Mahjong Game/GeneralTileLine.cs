using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
using DG.Tweening;
using System;

public class GeneralTileLine : ITileLine
{
    private List<ITile> _tilesInLine;
    private List<ITile> _tilesToDelete;
    private List<Vector2> _tilesPositions;
    private List<int> _indexes;
    private const int MATCH_NUMBER = 3;
    private const float DISTANCE_BETWEEN_TILES_IN_LINE = 1.11f;

    public Action LoseLevelAction {get;set;}

    public GeneralTileLine(List<Vector2> tilesPositions)
    {
        _tilesInLine = new List<ITile>();
        _tilesToDelete = new List<ITile>();
        _indexes = new List<int>();
        _tilesPositions = tilesPositions;
    }
    public bool AddTileToLine(ITile tile)
    {
        if (_tilesPositions.Count == _tilesInLine.Count)
        {
            Debug.Log("Lose");
            LoseLevelAction?.Invoke();
            ClearTileLine();
            return false;
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
                SortingTilesInLine();
                CheckMatch();
                return true;
            }
        }
        _tilesInLine.Add(tile);
        SortingTilesInLine();
        CheckMatch();
        return true;
    }
    public void SortingTilesInLine()
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
                    DeleteMatchingTilesView(lastType);
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
    private void DeleteMatchingTilesView(TileTypes matchType)
    {
        for (int i = _tilesInLine.Count - 1; i >= 0; i--)
        {
            if (_tilesInLine[i].tileType == matchType)
            {
                _tilesInLine[i].DeleteMatchTileViewAction?.Invoke();
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
            SortingTilesInLine();
        }
        else
        {
            int index = _indexes[0];
            _indexes.RemoveAt(0);
            for (int i = index; i < _tilesPositions.Count; i++)
            {
                _tilesPositions[i] -= new Vector2(3 * DISTANCE_BETWEEN_TILES_IN_LINE, 0f);
            }
            SortingTilesInLine();
        }
    }
    private void ClearTileLine()
    {
        foreach(var tile in _tilesInLine)
        {
            tile.DeleteTileViewAction?.Invoke();
        }
        _tilesInLine.Clear();
    }
}
