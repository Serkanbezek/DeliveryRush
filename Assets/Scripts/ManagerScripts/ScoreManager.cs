using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public static event Action LevelEnded;

    [SerializeField] private TextMeshProUGUI _totalScoreText;
    [SerializeField] private float _countDuration = 1f;

    private GameObject _levelScorePanelOnPlayer;
    private TextMeshProUGUI _levelScoreTextOnPlayer;
    private float _totalScore;
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



    private void OnEnable()
    {
        PackageDelivery.PackageDelivered += IncreaseLevelScore;
    }

    private void Start()
    {
        ShowSavedTotalScore();
    }

    private void OnDisable()
    {
        PackageDelivery.PackageDelivered -= IncreaseLevelScore;
    }


    private void ShowSavedTotalScore()
    {
        if (PlayerPrefs.HasKey("TotalScore"))
        {
            _totalScore = PlayerPrefs.GetFloat("TotalScore");
            _totalScoreText.text = ((int)_totalScore).ToString();
        }
        else
        {
            _totalScore = 0;
        }
    }

    private void IncreaseLevelScore(int deliveryValue)
    {
        _levelScore += deliveryValue;
        _levelScoreTextOnPlayer.text = _levelScore.ToString();
    }

    public float GetLevelScore()
    {
        return _levelScore;
    }

    public float GetTotalScore()
    {
        return _totalScore;
    }

    public void MultiplyLevelScore()
    {
        _levelScore = Mathf.RoundToInt(_levelScore * _levelEndScoreMultiplier);
        _levelScoreTextOnPlayer.text = _levelScore.ToString();
        _levelScorePanelOnPlayer.transform.DOScale(0.25f, 0.2f).SetLoops(2, LoopType.Yoyo).SetLink(_levelScorePanelOnPlayer)
            .OnComplete(() =>
        UIManager.Instance.ActivateLevelCompletedPanel());
    }

    private void ResetLevelScore()
    {
        _levelScore = 0;
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
        ResetLevelScore();
        ResetLevelEndScoreMultiplier();
        yield return new WaitForSeconds(0.5f);
        LevelEnded?.Invoke();
    }
    public void AddLevelScoreToTotalScore()
    {
        StartCoroutine(AddLevelScoreToTotalScoreRoutine());
    }

    public void RaiseLevelEndScoreMultiplier()
    {
        _levelEndScoreMultiplier += 0.1f;
    }

    private void ResetLevelEndScoreMultiplier()
    {
        _levelEndScoreMultiplier = 0.9f;
    }

    public void FindLevelScorePanelOnPlayer()
    {
        _levelScorePanelOnPlayer = GameObject.FindWithTag("Player").transform.GetChild(3).GetChild(0).gameObject;
    }

    public void GetLevelScoreTextOnPlayer()
    {
        _levelScoreTextOnPlayer = _levelScorePanelOnPlayer.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void DecreaseTotalScore(int courierPrice)
    {
        _totalScore -= courierPrice;
        _totalScoreText.text = ((int)_totalScore).ToString();
    }
}
