using UnityEngine;
using System;

public interface ITileView
{
    public Action TapOnTileAction {get;set;}
    public Action DeleteTileAction {get;set;}
    public void InitializeTileView(Vector2 position, int layer, Sprite icon);
    public void ChangeLockState(bool isLocked);
    public void MoveTileToNewPosition(Vector2 newPosition);
    public void DeleteTileView();
}
