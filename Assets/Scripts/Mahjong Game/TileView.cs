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
    // private int _matchTilesCounter;
    public static int _matchTilesCounter;

    public Action TapOnTileAction {get;set;}
    public Action EndOfDeleteTileAction {get;set;}

    public void InitializeTileView(Vector2 position, int layer, Sprite icon)
    {
        Vector3 newPosition = new Vector3(position.x, position.y, 0f);
        _spriteRendererIcon.sprite = icon;
        _spriteRenderer.sortingOrder = 2 * layer + 5;
        _spriteRendererIcon.sortingOrder = 2 * layer + 6;
        _spriteRendererPanel.sortingOrder = 2 * layer + 6;
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
        transform.DOLocalMove(newPosition, 0.5f);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        TapOnTileAction?.Invoke();
    }
    public void DeleteTileView()
    {
        Destroy(this.gameObject);
    }
    public void DeleteMatchTileView()
    {
        StartCoroutine(DeleteTile());
        DOTween.Sequence().Append(transform.DOScale(1.2f, 0.3f).SetDelay(0.5f)).Append(transform.DOScale(1.0f, 0.3f).OnComplete(() => EndOfDeleteMatchTiles()));
    }
    
    private IEnumerator DeleteTile()
    {
        yield return new WaitForSeconds(1.1f);
        Destroy(this.gameObject);
    }
    private void EndOfDeleteMatchTiles()
    {
        _matchTilesCounter++;
        if (_matchTilesCounter == 3)
        {
            _matchTilesCounter = 0;
            EndOfDeleteTileAction?.Invoke();
        }
    }
    private void Awake() 
    {
        _matchTilesCounter = 0;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRendererPanel = _darkPanelGO.GetComponent<SpriteRenderer>();
    }
}
