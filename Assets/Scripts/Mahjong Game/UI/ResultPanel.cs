using System.Collections;
using System.Collections.Generic;
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

    public Button replayButton => _replayButton;
    public Button nextLevelButton => _nextLevelButton;

    public void ShowWinPanel()
    {
        Invoke("WinPanel", 1f);
    }
    public void ShowLosePanel()
    {
        Debug.Log("showLosePanel");
        Invoke("LosePanel", 1f);
    }

    private void WinPanel()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        _resultText.text = _winText;
        _replayButton.gameObject.SetActive(true);
        _nextLevelButton.gameObject.SetActive(true);
    }
    private void LosePanel()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        _resultText.text = _loseText;
        _replayButton.gameObject.SetActive(true);
        _nextLevelButton.gameObject.SetActive(false);
    }
    private void Awake()
    {
        _nextLevelButton.onClick.AddListener(() => transform.GetChild(0).gameObject.SetActive(false));
        _replayButton.onClick.AddListener(() => transform.GetChild(0).gameObject.SetActive(false));
    }
}
