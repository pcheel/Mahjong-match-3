using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : ITile
{
    private List<ITile> _topTiles;
    private List<ITile> _downTiles;

    public Tile()
    {
        _topTiles = new List<ITile>();
        _downTiles = new List<ITile>();
    }
    public void Activate()
    {

    }
}
