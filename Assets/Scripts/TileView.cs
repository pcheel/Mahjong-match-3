using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;

public class TileView : MonoBehaviour, ITileView, IPointerClickHandler
{
    [SerializeField] private GameObject _darkPanelGO;
    [SerializeField] private SpriteRenderer _spriteRendererIcon;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _spriteRendererPanel;
    private TileAnimationQueue _tileAnimationQueue;
    private Tween _movingTween;

    public Action TapOnTileAction {get;set;}
    public Action DeleteTileAction {get;set;}

    public void InitializeTileView(Vector2 position, int layer, Sprite icon)
    {
        Vector3 newPosition = new Vector3(position.x, position.y, 0f);
        _spriteRendererIcon.sprite = icon;
        _spriteRenderer.sortingOrder = layer + 5;
        _spriteRendererIcon.sortingOrder = layer + 6;
        _spriteRendererPanel.sortingOrder = layer + 6;
        transform.position = newPosition;
    }
    public void ChangeLockState(bool isLocked)
    {
        if (isLocked)
        {
            _darkPanelGO.SetActive(true);
        }
        else
        {
            _darkPanelGO.SetActive(false);
        }
    }
    public void MoveTileToNewPosition(Vector2 newPosition)
    {
        _movingTween = transform.DOLocalMove(newPosition, 0.5f);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        TapOnTileAction?.Invoke();
    }
    public void DeleteTileView()
    {
        // _movingTween.Kill();
        DOTween.Sequence().Append(transform.DOScale(1.2f, 0.3f)).Append(transform.DOScale(1.0f, 0.3f).OnComplete(()=>DeleteTile()));
        // Destroy(this.gameObject);
    }
    public void KillMoveTween()
    {
        if (_movingTween != null)
        {
            _movingTween.Kill();
            // DOTween.Kill(_movingTween);
        }
    }

    private void DeleteTile()
    {
        // DeleteTileAction?.Invoke();
        Destroy(this.gameObject);
    }
    private void Awake() 
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRendererPanel = _darkPanelGO.GetComponent<SpriteRenderer>();
    }

    // public void OnPointerEnter(PointerEventData eventData)
    // {
    //     Debug.Log("enter");
    // }
    // private void OnMouseDown()
    // {
    //     Debug.Log("click");
    // }
}
