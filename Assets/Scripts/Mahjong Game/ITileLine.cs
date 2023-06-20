using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ITileLine
{
    public Action LoseLevelAction {get;set;}
    public void AddTileToLine(ITile tile);
    public void SortingTilesInLine();
    public void ShiftPointsLeftForSpawnWithDelay();
}
