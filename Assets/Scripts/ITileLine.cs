using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITileLine
{
    public bool AddTileToLine(ITile tile);
    public void SortingTilesInLine();
}
