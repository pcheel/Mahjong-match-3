using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralTileLineView : MonoBehaviour, ITileLineView
{
    public void SetPosition(Vector2 newPosition)
    {
        transform.position = newPosition;
    }
}
