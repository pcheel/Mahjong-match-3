using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreatorManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _factoryPrefab;
    [SerializeField] private GameObject _levelMapPrefab;

    private IFactory _factory;
    private LevelMapPresenter _levelMapPresenter;
    
    private void CreateFactory()
    {
        GameObject factoryGO = Instantiate(_factoryPrefab, transform.parent);
        _factory = factoryGO.GetComponent<IFactory>();
    }
    private void CreateLevelMap()
    {
        ILevelMap levelMapModel = _factory.CreateLevelMapModel();
        ILevelMapView levelMapView = _factory.CreateLevelMapView(_levelMapPrefab, transform);
        _levelMapPresenter = _factory.CreateLevelMapPresenter(levelMapModel, levelMapView);
    }
    private void Start() 
    {

    }
    private void Awake() 
    {
        CreateFactory();
    }
}
