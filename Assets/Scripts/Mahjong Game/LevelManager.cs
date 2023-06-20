using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : ILevelManager
{
    private List<LevelData> _levels;
    private TileManager _tileManager;
    private LevelData _currentLevel;
    private List<LevelData> _completedLevels;
    private List<LevelData> _uncompletedLevels;

    public LevelData currentLevel => _currentLevel;

    public Action WinLevelAction {get;set;}

    public LevelManager(TileManager tileManager)
    {
        _completedLevels = new List<LevelData>();
        _uncompletedLevels = new List<LevelData>();
        // for (int i = 0; i < Resources.E)
        _levels = new List<LevelData>(Resources.LoadAll<LevelData>($"LevelDatas/"));
        Debug.Log(_levels.Count);
        _uncompletedLevels = _levels;
        _tileManager = tileManager;
        // PlayNextLevel();
        RandomAndStartLevel();
    }
    public void ReplayLevel()
    {
        Debug.Log("ReplayLevel");
        _tileManager.LoadLevel(_currentLevel);
    }
    public void PlayNextLevel()
    {
        // if (_currentLevel != null)
        // {
        //     _completedLevels.Add(_currentLevel);
        //     _uncompletedLevels.Remove(_currentLevel);
        // }
        _completedLevels.Add(_currentLevel);
        _uncompletedLevels.Remove(_currentLevel);
        RandomAndStartLevel();
    }
    public void CheckWinLevel(List<List<ITile>> tiles)
    {
        // if (tiles.Count == 0)
        // {
        //     WinLevelAction?.Invoke();
        // }
        foreach (var listOfTiles in tiles)
        {
            if (listOfTiles.Count > 0)
            {
                return;
            }
            // foreach(var tile in listOfTiles)
            // {
            //     // if (tile != null)
            //     // { 
            //     //     return;
            //     // }
            // }
        }
        WinLevelAction?.Invoke();
    }

    private void RandomAndStartLevel()
    {
        int randomLevel = UnityEngine.Random.Range(0, _uncompletedLevels.Count);
        _currentLevel = _uncompletedLevels[randomLevel];
        _tileManager.LoadLevel(_currentLevel);
    }
    // private IEnumerator WinLevel()
    // {
    //     yield return new WaitForSeconds(1f);
    //     WinLevelAction?.Invoke();
    // }
    // public void 
}
