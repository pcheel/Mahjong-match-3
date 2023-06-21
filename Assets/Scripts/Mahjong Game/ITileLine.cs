using System;

public interface ITileLine
{
    public Action LoseLevelAction {get;set;}
    public void AddTileToLine(ITile tile);
    public void ChangeTilePositions();
    public void ShiftPointsLeftForSpawnWithDelay();
}
