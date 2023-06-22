using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMapPresenter
{
    private ILevelMap _levelMap;
    private ILevelMapView _levelMapView;

    public LevelMapPresenter (ILevelMap levelMap, ILevelMapView levelMapView)
    {
        _levelMap = levelMap;
        _levelMapView = levelMapView;
    }
}
