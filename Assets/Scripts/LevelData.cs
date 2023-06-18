using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData", order = 51)]
public class LevelData : ScriptableObject
{
    public List<TileLayerData> layerDatas;
    public List<TileTypes> tileTypes;
}

[Serializable]
public class TileLayerData
{
    public List<TileData> tileDatas;    
}

[Serializable]
public class TileData
{
    public Vector2 position;
    // public TileTypes type;
    // public Sprite icon;
    // public int id;
    // public List<int> topTilesID;
    // public List<int> downTilesID;
}
