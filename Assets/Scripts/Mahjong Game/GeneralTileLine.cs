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
        _tilesInLine = new List<ITile>(tilesPositions.Count);
        _tilesToDelete = new List<ITile>();
        _indexes = new List<int>();
        _tilesPositions = tilesPositions;
    }
    public void AddTileToLine(ITile tile)
    {
        if (_tilesPositions.Count == _tilesInLine.Count)
        {
            LoseLevelAction?.Invoke();
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
                SortingTilesInLine();
                CheckMatch();
                return;
            }
        }
        _tilesInLine.Add(tile);
        SortingTilesInLine();
        CheckMatch();
    }
    public void SortingTilesInLine()
    {
        for (int i = 0; i < _tilesInLine.Count; i++)
        {
            _tilesInLine[i].ChangePosition(_tilesPositions[i]);
        }
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
                    // for (int n = 0; n < _tilesPositions.Count; n++)
                    // {
                    //     Debug.Log($"pos {n}: {_tilesPositions[n]}");
                    // }
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
                // ITile tile = _tilesInLine[i];
                _tilesInLine[i].DeleteTileViewAction?.Invoke();
                RemoveTileFromList(_tilesInLine[i]);

                // RemoveTileFromList.Invoke(1f);
                // Coroutine
                // StartCoroutine(RemoveTileFromList(_tilesInLine[i]));
                // _tilesInLine[i].DeleteTile();
                // _tilesInLine.RemoveAt(i);
            }
        }
        // SortingTilesInLine();
    }
    private void RemoveTile(ITile tile)
    {
        // Thread.Sleep(500);
        // tile.DeleteTileViewAction?.Invoke();

        Thread.Sleep(1100);
        ShiftPointsForSpawn(false);
        // _tilesInLine.Remove(tile);
        // tile.DeleteTile();
        //подвинуть тайлы обратно
        //снова вызвать сортировку
    }
    private void KillMovingTweens()
    {
        foreach(var tile in _tilesInLine)
        {
            tile.KillMovingTweensAction?.Invoke();
        }
    }
    // private async void RemoveTileFromList(ITile tile)
    // {
    //     await Task.Run(() => RemoveTile(tile));
    //     DOTween.KillAll();
    //     SortingTilesInLine();
    // }
    private void RemoveTileFromList(ITile tile)
    {
        // int index = _tilesInLine.IndexOf(tile);
        // ShiftPointsForSpawn();
        _tilesInLine.Remove(tile);
        Debug.Log(_tilesInLine.Count);
        // await Task.Run(() => RemoveTile(tile));
        // DOTween.KillAll();
        // SortingTilesInLine();
    }
    private void ShiftPointsForSpawn(bool isRightShift)
    {
        if (isRightShift)
        {
            Debug.Log($"index {_indexes[_indexes.Count - 1]}");
            for (int i = _indexes[_indexes.Count - 1]; i < _tilesPositions.Count; i++)
            {
                _tilesPositions[i] += new Vector2(3 * DISTANCE_BETWEEN_TILES_IN_LINE, 0f);
                Debug.Log("shift");
            }
            SortingTilesInLine();
            Debug.Log("RightShift");
            // await Task.Run(() => ShiftPointsForSpawnWithDelay(false));
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
            Debug.Log("LeftShift");
        }
    }
    public void ShiftPointsLeftForSpawnWithDelay()
    {
        // Thread.Sleep(1200);
        ShiftPointsForSpawn(false);
    }
}
