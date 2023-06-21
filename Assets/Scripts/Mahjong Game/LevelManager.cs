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

    public Action WinLevelAction {get;set;}

    public LevelManager(TileManager tileManager)
    {
        _completedLevels = new List<LevelData>();
        _uncompletedLevels = new List<LevelData>();
        _tileManager = tileManager;
        LoadAndSetLevelsData();
        RandomAndStartLevel();
    }
    public void ReplayLevel()
    {
        _tileManager.LoadLevel(_currentLevel);
    }
    public void PlayNextLevel()
    {
        _completedLevels.Add(_currentLevel);
        _uncompletedLevels.Remove(_currentLevel);
        if (_uncompletedLevels.Count == 0)
        {
            _uncompletedLevels = new List<LevelData>(_completedLevels);
            _completedLevels.Clear();
        }
        RandomAndStartLevel();
    }
    public void CheckWinLevel(List<List<ITile>> tilesOnMap)
    {
        foreach (var layerOfTiles in tilesOnMap)
        {
            if (layerOfTiles.Count > 0)
            {
                return;
            }
        }
        WinLevelAction?.Invoke();
    }

    private void RandomAndStartLevel()
    {
        int randomLevel = UnityEngine.Random.Range(0, _uncompletedLevels.Count);
        _currentLevel = _uncompletedLevels[randomLevel];
        _tileManager.LoadLevel(_currentLevel);
    }
    private void LoadAndSetLevelsData()
    {
        _levels = new List<LevelData>(Resources.LoadAll<LevelData>($"LevelDatas/"));
        _uncompletedLevels = _levels;
    }
}
