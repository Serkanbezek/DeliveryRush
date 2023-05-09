using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private Image _levelCompletedPanel;
    [SerializeField] private Image _levelFailedPanel;
    [SerializeField] private TextMeshProUGUI _levelCompletedPanelLevelScoreText;
    [SerializeField] private Button _getCoinsButton;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private GameObject _fadetransition;

    private int _level;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        ScoreManager.LevelEnded += FadeAnimation;
        ScoreManager.LevelEnded += DeactivateLevelCompletedPanel;
        ScoreManager.LevelEnded += ChangeLevelText;
    }

    private void Start()
    {
        ShowSavedLevel();
    }

    private void OnDisable()
    {
        ScoreManager.LevelEnded -= DeactivateLevelCompletedPanel;
        ScoreManager.LevelEnded -= ChangeLevelText;
        ScoreManager.LevelEnded -= FadeAnimation;
    }



    private void ShowSavedLevel()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            _level = PlayerPrefs.GetInt("SavedLevel");
            _levelText.text = "LEVEL " + _level.ToString();
        }
        else
        {
            _level = 1;
        }
    }

    public void ActivateLevelCompletedPanel()
    {
        float levelscore = ScoreManager.Instance.GetLevelScore();
        float totalScore = ScoreManager.Instance.GetTotalScore();
        _levelCompletedPanel.gameObject.SetActive(true);
        GameManager.Instance.SaveLevelData();
        PlayerPrefs.SetFloat("TotalScore", totalScore + levelscore);
        LevelCompletedPanelLevelScoreText();
        _levelCompletedPanel.rectTransform.DOAnchorPos(new Vector2(0, 35), 0.5f).SetEase(Ease.OutBack).SetLink(_levelCompletedPanel.gameObject);
    }

    public void ActivateLevelFailedPanel()
    {
        _levelFailedPanel.gameObject.SetActive(true);
        _levelFailedPanel.rectTransform.DOAnchorPos(new Vector2(0, 35), 0.5f).SetEase(Ease.OutBack).SetLink(_levelFailedPanel.gameObject);
    }

    public void DeactivateLevelFailedPanel()
    {
        _levelFailedPanel.gameObject.SetActive(false);
        _levelFailedPanel.rectTransform.anchoredPosition = new Vector2(0, 1250);
    }

    private void DeactivateLevelCompletedPanel()
    {
        StartCoroutine(DeactivateLevelCompletedPanelCoroutine());
    }

    private IEnumerator DeactivateLevelCompletedPanelCoroutine()
    {
        yield return new WaitForSeconds(1);
        _levelCompletedPanel.gameObject.SetActive(false);
        _getCoinsButton.gameObject.SetActive(true);
        _levelCompletedPanel.rectTransform.anchoredPosition = new Vector2(0, 1250);
    }

    public void DeactivateGetCoinsButton()
    {
        _getCoinsButton.gameObject.SetActive(false);
    }

    private void LevelCompletedPanelLevelScoreText()
    {
        float levelScore = ScoreManager.Instance.GetLevelScore();
        _levelCompletedPanelLevelScoreText.text = levelScore.ToString();
    }

    private IEnumerator ChangeLevelTextCoroutine()
    {
        _level += 1;
        if (_level > 10)
        {
            _level = 10;
        }
        yield return new WaitForSeconds(1f);
        _levelText.text = "LEVEL " + _level.ToString();
    }

    private void ChangeLevelText()
    {
        StartCoroutine(ChangeLevelTextCoroutine());
    }

    private void FadeAnimation()
    {
        _fadetransition.SetActive(true);
        _fadetransition.GetComponent<CanvasGroup>().DOFade(1, 1).SetLink(_fadetransition);
        _fadetransition.GetComponent<CanvasGroup>().DOFade(0, 0.1f).SetDelay(1.1f).SetLink(_fadetransition).OnComplete(() =>
        _fadetransition.SetActive(false));
    }
}
