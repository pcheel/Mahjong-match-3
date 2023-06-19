using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
using DG.Tweening;

public class GeneralTileLine : ITileLine
{
    private List<ITile> _tilesInLine;
    private List<ITile> _tilesToDelete;
    private List<Vector2> _tilesPositions;
    private const int MATCH_NUMBER = 3;

    public GeneralTileLine(List<Vector2> tilesPositions)
    {
        _tilesInLine = new List<ITile>(tilesPositions.Count);
        _tilesToDelete = new List<ITile>();
        _tilesPositions = tilesPositions;
    }
    public bool AddTileToLine(ITile tile)
    {
        if (_tilesPositions.Count == _tilesInLine.Count)
        {
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
            // if (!_tilesInLine[i].isLocked)
            // {
            // }
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
                    DeleteMatchingTilesView(lastType);
                }
            }
            // else if (matchCounter == 0)
            // {
            //     lastType = _tilesInLine[i].tileType;
            //     matchCounter = 1;
            // }
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
        _tilesInLine.Remove(tile);
    }
    private void KillMovingTweens()
    {
        foreach(var tile in _tilesInLine)
        {
            tile.KillMovingTweensAction?.Invoke();
        }
    }
    private async void RemoveTileFromList(ITile tile)
    {
        // await Task.Run( () => _tilesInLine.Remove(tile));
        await Task.Run(() => RemoveTile(tile));
        // DOTween.Kill()
        DOTween.KillAll();
        // DOTween.ClearCachedTweens();
        // KillMovingTweens();
        // DOTween.CompleteAll();
        SortingTilesInLine();
    }
}
