using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultPanel : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private string _loseText;
    [SerializeField] private string _winText;
    [Header("Dependencies")]
    [SerializeField] private TMP_Text _resultText;
    [SerializeField] private Button _replayButton;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private GameObject _panelGO;

    public Button replayButton => _replayButton;
    public Button nextLevelButton => _nextLevelButton;

    private const float SHOW_PANEL_DELAY = 1f;

    public void ShowWinPanel()
    {
        Invoke("WinPanel", SHOW_PANEL_DELAY);
    }
    public void ShowLosePanel()
    {
        Invoke("LosePanel", SHOW_PANEL_DELAY);
    }

    private void WinPanel()
    {
        _panelGO.SetActive(true);
        _resultText.text = _winText;
        _replayButton.gameObject.SetActive(true);
        _nextLevelButton.gameObject.SetActive(true);
    }
    private void LosePanel()
    {
        _panelGO.SetActive(true);
        _resultText.text = _loseText;
        _replayButton.gameObject.SetActive(true);
        _nextLevelButton.gameObject.SetActive(false);
    }
    private void Awake()
    {
        _nextLevelButton.onClick.AddListener(() => _panelGO.SetActive(false));
        _replayButton.onClick.AddListener(() => _panelGO.SetActive(false));
    }
}
