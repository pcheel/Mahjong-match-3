using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TilePoints", menuName = "Data/TilePoints", order = 51)]
public class LevelTilePointsData : ScriptableObject
{
    public List<Vector2> _tilePoints;
}
