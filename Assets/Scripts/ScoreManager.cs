using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelscoreText;

    private int _levelScore = 0;

    private void OnEnable()
    {
        PackageDelivery.PackageDelivered += UpdateLevelScore;
    }

    private void OnDisable()
    {
        PackageDelivery.PackageDelivered -= UpdateLevelScore;
    }

    private void UpdateLevelScore(int deliveryValue)
    {
        _levelScore += deliveryValue;
        _levelscoreText.text = _levelScore.ToString();
    }

    public int GetLevelScore()
    {
        return _levelScore;
    }

}
