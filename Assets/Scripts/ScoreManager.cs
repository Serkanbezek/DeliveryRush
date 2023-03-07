using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private GameObject _levelScorePanel;
    [SerializeField] private TextMeshProUGUI _totalScoreText;
    [SerializeField] private float _countDuration = 1f;

    private TextMeshProUGUI _levelScoreText;
    private float _totalScore = 0;
    private float _levelScore = 0;
    private float _levelEndScoreMultiplier = 0.9f;


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

    private void Start()
    {
        _levelScoreText = _levelScorePanel.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        PackageDelivery.PackageDelivered += IncreaseLevelScore;
    }

    private void OnDisable()
    {
        PackageDelivery.PackageDelivered -= IncreaseLevelScore;
    }

    private void IncreaseLevelScore(int deliveryValue)
    {
        _levelScore += deliveryValue;
        _levelScoreText.text = _levelScore.ToString();
    }

    public float GetLevelScore()
    {
        return _levelScore;
    }

    public void RaiseLevelEndScoreMultiplier()
    {
        _levelEndScoreMultiplier += 0.1f;
    }

    public void MultiplyLevelScore()
    {
        _levelScore = Mathf.RoundToInt(_levelScore * _levelEndScoreMultiplier);
        _levelScoreText.text = _levelScore.ToString();
        _levelScorePanel.transform.DOScale(0.25f, 0.2f).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
        UIManager.Instance.ActivateGetCoinsButton());
    }

    private IEnumerator AddLevelScoreToTotalScoreRoutine()
    {
        float targetValue = _totalScore + _levelScore;
        float countRate = targetValue - _totalScore / _countDuration;
        yield return new WaitForSeconds(1.5f);
        while(_totalScore != targetValue)
        {
            _totalScore = Mathf.MoveTowards(_totalScore, targetValue, countRate * Time.deltaTime);
            _totalScoreText.text = ((int)_totalScore).ToString();
            yield return null;
        }
    }
    public void AddLevelScoreToTotalScore()
    {
        StartCoroutine(AddLevelScoreToTotalScoreRoutine());
    }
}
