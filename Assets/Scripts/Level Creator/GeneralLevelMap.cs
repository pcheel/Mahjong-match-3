using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralLevelMap : ILevelMap
{
    private IFactory _factory;
    private LevelCreatorManager _levelCreatorManager;
    private List<List<ITilePoint>> _allTilePoints;
    private GameObject _tilePointPrefab;
    private const float UPPER_LEFT_POINT_Y = 3f;
    private const float UPPER_LEFT_POINT_X = -3f;
    private const float DISTANCE_BETWEEN_POINTS_IN_LINE = 0.5f;
    private const int POINTS_IN_HORIZONTAL_LINE = 14;
    private const int POINTS_IN_VERTICAL_LINE = 14;

    public GeneralLevelMap(LevelCreatorManager levelCreatorManager, IFactory factory, GameObject tilePointPrefab)
    {
        _factory = factory;
        _levelCreatorManager = levelCreatorManager;
        _tilePointPrefab = tilePointPrefab;
        _allTilePoints = new List<List<ITilePoint>>();
        CreateNewLayer(LayerType.Uneven);
    }
    public void CreateNewLayer(LayerType type)
    {
        Vector2 position = new Vector2();
        for (int i = 0; i < POINTS_IN_VERTICAL_LINE; i++)
        {
            for (int j = 0; j < POINTS_IN_VERTICAL_LINE; j++)
            {
                if (type == LayerType.Uneven && i % 2 == 0 && j % 2 == 0)
                {
                    position.x = UPPER_LEFT_POINT_X + DISTANCE_BETWEEN_POINTS_IN_LINE * i;
                    position.y = UPPER_LEFT_POINT_Y - DISTANCE_BETWEEN_POINTS_IN_LINE * j;
                    CreateTilePoint(position);
                }
                else if (type == LayerType.Even && 2 == 1 && j % 2 == 1)
                {
                    position.x = UPPER_LEFT_POINT_X + DISTANCE_BETWEEN_POINTS_IN_LINE * i;
                    position.y = UPPER_LEFT_POINT_Y - DISTANCE_BETWEEN_POINTS_IN_LINE * j;
                    CreateTilePoint(position);
                }
            }
        }
    }

    private LayerType CheckLayerParity(int layerNumber)
    {
        return layerNumber % 2 == 0 ? LayerType.Even : LayerType.Uneven;
    }
    private void CreateTilePoint(Vector2 position)
    {
        ITilePoint tilePoint = _factory.CreateTilePointModel();
        ITilePointView tilePointView = _factory.CreateTilePointView(_tilePointPrefab, _levelCreatorManager.transform);
        TilePointPresenter tilePointPresenter = _factory.CreateTilePointPresenter(tilePoint, tilePointView, position);
    }
}
