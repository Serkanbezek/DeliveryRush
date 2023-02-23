using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private int _score = 0;

    private void OnEnable()
    {
        PackageDelivery.PackageDelivered += UpdateScore;
    }

    private void OnDisable()
    {
        PackageDelivery.PackageDelivered -= UpdateScore;
    }

    private void UpdateScore()
    {
        _score += 10;
        _scoreText.text = "Score: " + _score.ToString();
    }
}
