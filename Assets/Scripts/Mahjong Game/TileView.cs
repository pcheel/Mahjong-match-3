using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;

public class TileView : MonoBehaviour, ITileView, IPointerClickHandler
{
    [SerializeField] private GameObject _darkPanelGO;
    [SerializeField] private SpriteRenderer _spriteRendererIcon;

    private TilePresenter _tilePresenter;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _spriteRendererPanel;
    private const float DURATION_OF_MOVEMENT_ANIMATION = 0.5f;
    private const float DURATION_OF_DEATH_ANIMATION = 0.6f;

    public static int _matchTilesCounter;

    public Action TapOnTileAction {get;set;}
    public Action EndOfDeleteTileAction {get;set;}

    public void InitializeTileView(TilePresenter tilePresenter, Vector2 position, int layer, Sprite icon)
    {
        _tilePresenter = tilePresenter;
        Vector3 newPosition = new Vector3(position.x, position.y, 0f);
        _spriteRendererIcon.sprite = icon;
        _spriteRenderer.sortingOrder = 2 * layer + 5;
        _spriteRendererIcon.sortingOrder = 2 * layer + 6;
        _spriteRendererPanel.sortingOrder = 2 * layer + 6;
        transform.position = newPosition;
    }
    public void ChangeLockState(bool isLocked)
    {
        _darkPanelGO.SetActive(isLocked);
    }
    public void OnEnable()
    {
        _tilePresenter?.Enable();
    }
    public void OnDisable()
    {
        _tilePresenter?.Disable();
    }
    public void MoveTileToNewPosition(Vector2 newPosition)
    {
        transform.DOLocalMove(newPosition, DURATION_OF_MOVEMENT_ANIMATION);
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
        StartCoroutine(DeleteTileWithDelay());
        DOTween.Sequence()
            .Append(transform.DOScale(1.2f, DURATION_OF_DEATH_ANIMATION / 2).SetDelay(DURATION_OF_MOVEMENT_ANIMATION))
            .Append(transform.DOScale(1.0f, DURATION_OF_DEATH_ANIMATION / 2).OnComplete(() => EndOfDeleteMatchTiles()));
    }
    
    private IEnumerator DeleteTileWithDelay()
    {
        yield return new WaitForSeconds(DURATION_OF_DEATH_ANIMATION + DURATION_OF_MOVEMENT_ANIMATION);
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
