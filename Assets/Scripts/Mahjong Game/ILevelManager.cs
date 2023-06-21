using System.Collections.Generic;
using System;

public interface ILevelManager
{
    public Action WinLevelAction {get;set;}
    public void ReplayLevel();
    public void PlayNextLevel();
    public void CheckWinLevel(List<List<ITile>> listOfTiles);
}
