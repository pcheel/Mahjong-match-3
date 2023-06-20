using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLinePresenter
{
    private ITileLine _tileLineModel;
    private ITileLineView _tileLineView;

    public TileLinePresenter(ITileLine tileLineModel, ITileLineView tileLineView)
    {
        _tileLineModel = tileLineModel;
        _tileLineView = tileLineView;
    }
}
