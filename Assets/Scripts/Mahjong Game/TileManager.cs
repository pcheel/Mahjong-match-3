using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private GameObject _factoryPrefab;
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private GameObject _tilesLinePrefab;
    [SerializeField] private Sprite _gearIcon;
    [SerializeField] private Sprite _heartIcon;
    [SerializeField] private Sprite _hexagonIcon;
    [SerializeField] private Sprite _starIcon;
    [SerializeField] private Vector2 _tileLinePosition;
    [SerializeField] private List<Vector2> _linePoints;
    [SerializeField] private ResultPanel _resultPanel;
    // [SerializeField] private LevelManager _levelManager;

    private LevelData _levelData;
    private IFactory _factory;
    private ILevelManager _levelManager;
    private List<List<ITile>> _tilesOnMap;
    // private List<ITile> _tilesInLine;
    private ITileLine _tileLine;
    private List<TileTypes> _allTypes;
    private const float HALF_TILE_SIZE = 0.5f;

    public void AddTileToLine(ITile tile)
    {
        _tileLine.AddTileToLine(tile);
        _tilesOnMap[tile.layer].Remove(tile);
        _levelManager.CheckWinLevel(_tilesOnMap);
    }
    public void LoadLevel(LevelData levelData)
    {
        _levelData = levelData;
        CreateAndInitializeTileLine();
        CreateAllTypes();
        CreateAllTiles();
        SetTopAndDownTiles();
    }
    public void ShiftLine()
    {
        _tileLine.ShiftPointsLeftForSpawnWithDelay();
    }
    public Sprite GetTileIcon(TileTypes type)
    {
        switch(type)
        {
            case TileTypes.Heart:
                return _heartIcon;
            case TileTypes.Gear:
                return _gearIcon;
            case TileTypes.Star:
                return _starIcon;
            case TileTypes.Hexagon:
                return _hexagonIcon;
            default:
                return null;
        }
    }
    // public void DeleteTileF

    private void CreateAndInitializeTileLine()
    {
        _tileLine = _factory.CreateTileLineModel(_linePoints);
        ITileLineView tileLineView = _factory.CreateTileLineView(_tilesLinePrefab, transform.parent);
        tileLineView.SetPosition(_tileLinePosition);
        // TileLinePresenter tileLinePresenter = _factory.CreateTileLinePresenter(_tileLine, tileLineView);
    }
    private void CreateAllTiles()
    {
        for(int i = 0; i < _levelData.layerDatas.Count; i++)
        {
            _tilesOnMap.Add(new List<ITile>());
            for(int j = 0; j < _levelData.layerDatas[i].tileDatas.Count; j++)
            {
                _tilesOnMap[i].Add(CreateAndInitializeTile(_levelData.layerDatas[i].tileDatas[j], i));
            }
        }
    }
    private ITile CreateAndInitializeTile(TileData data, int layer)
    {
        ITile tileModel = _factory.CreateTileModel();
        ITileView tileView = _factory.CreateTileView(_tilePrefab, transform);
        TilePresenter tilePretender = _factory.CreateTilePresenter(tileModel, tileView);
        TileTypes type = _allTypes[Random.Range(0, _allTypes.Count)];
        _allTypes.Remove(type);
        tileModel.Initialize(data, layer, type, this);
        return tileModel;
    }
    private void CreateAllTypes()
    {
        _allTypes = new List<TileTypes>();
        for (int i = 0; i < _levelData.tileTypes.Count; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                _allTypes.Add(_levelData.tileTypes[i]);
            }
        }
    }
    private void CreateFactory()
    {
        GameObject factoryGO = Instantiate(_factoryPrefab, transform.parent);
        _factory = factoryGO.GetComponent<IFactory>();
    }
    private void SetTopAndDownTiles()
    {
        for (int i = 0; i < _tilesOnMap.Count; i++)
        {
            for (int j = 0; j < _tilesOnMap[i].Count; j++)
            {
                if (i == 0 && _tilesOnMap.Count > 1)
                {
                    SetTopTiles(i, j);
                }
                else if (i == _tilesOnMap.Count - 1 && _tilesOnMap.Count > 1)
                {
                    SetDownTiles(i, j);
                }
                else if (_tilesOnMap.Count > 1 && i < _tilesOnMap.Count - 1 && i > 0)
                {
                    SetTopTiles(i, j);
                    SetDownTiles(i, j);
                }
                _tilesOnMap[i][j].CheckTileState();
            }
        }
    }
    private void SetTopTiles(int i, int j)
    {
        List<ITile> topTiles = new List<ITile>();
        for (int k = 0; k < _tilesOnMap[i + 1].Count; k++)
        {
            if (Mathf.Abs(_tilesOnMap[i + 1][k].position.x - _tilesOnMap[i][j].position.x) <= 1f && Mathf.Abs(_tilesOnMap[i + 1][k].position.y - _tilesOnMap[i][j].position.y) <= 1f)
            {
                topTiles.Add(_tilesOnMap[i + 1][k]);
            }
        }
        _tilesOnMap[i][j].SetTopTiles(topTiles);
    }
    private void SetDownTiles(int i, int j)
    {
        List<ITile> downTiles = new List<ITile>();
        for (int k = 0; k < _tilesOnMap[i - 1].Count; k++)
        {
            if (Mathf.Abs(_tilesOnMap[i - 1][k].position.x - _tilesOnMap[i][j].position.x) <= 1f && Mathf.Abs(_tilesOnMap[i - 1][k].position.y - _tilesOnMap[i][j].position.y) <= 1f)
            {
                downTiles.Add(_tilesOnMap[i - 1][k]);
            }
        }
        _tilesOnMap[i][j].SetDownTiles(downTiles);
    }
    private void Start() 
    {
        _levelManager = _factory.CreateLevelManager(this);
        _levelManager.WinLevelAction += _resultPanel.ShowWinPanel;
        _tileLine.LoseLevelAction += _resultPanel.ShowLosePanel;
        _resultPanel.nextLevelButton.onClick.AddListener(_levelManager.ReplayLevel);
        _resultPanel.replayButton.onClick.AddListener(_levelManager.PlayNextLevel);
    }
    private void Awake() 
    {
        CreateFactory();
        _tilesOnMap = new List<List<ITile>>();
    }
    private void OnDisable()
    {
        _levelManager.WinLevelAction -= _resultPanel.ShowWinPanel;
        _resultPanel.nextLevelButton.onClick.RemoveListener(_levelManager.PlayNextLevel);
        _resultPanel.replayButton.onClick.RemoveListener(_levelManager.ReplayLevel);
    }
}
