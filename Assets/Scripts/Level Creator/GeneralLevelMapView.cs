using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralLevelMapView : MonoBehaviour, ILevelMapView
{
    [SerializeField] private Button _addLayerButton;
    [SerializeField] private Button _removeLayer;
}
