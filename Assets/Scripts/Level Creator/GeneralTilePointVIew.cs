using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class GeneralTilePointView : MonoBehaviour, ITilePointView, IPointerClickHandler
{
    [Header("Sprites")]
    [SerializeField] private Sprite _pointIcon;
    [SerializeField] private Sprite _tileIcon;

    private SpriteRenderer _spriteRenderer;

    public Action TapOnTilePointAction {get;set;}

    public void InitializationView(Vector2 position)
    {
        transform.position = new Vector3(position.x, position.y, 0f);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        TapOnTilePointAction?.Invoke();
    }
    public void ChangeIcon(bool isEmpty)
    {
        _spriteRenderer.sprite = isEmpty ? _pointIcon : _tileIcon;
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
