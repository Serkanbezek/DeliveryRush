using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int _maxLevel = 10;

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
        SetTargetFrameRate();
        LoadRequiredLevel();
    }

    private void OnEnable()
    {
        ScoreManager.LevelEnded += LoadNextLevel;
    }

    private void OnDisable()
    {
        ScoreManager.LevelEnded -= LoadNextLevel;
    }

    private void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevelRoutine());
    }

    private IEnumerator LoadNextLevelRoutine()
    {
        yield return new WaitForSeconds(1);
        if (SceneManager.GetActiveScene().buildIndex == _maxLevel)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private IEnumerator LoadSavedLevelRoutine()
    {
        int savedLevel = PlayerPrefs.GetInt("SavedLevel");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(savedLevel);
    }

    private void LoadRequiredLevel()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            StartCoroutine(LoadSavedLevelRoutine());
        }
        else
        {
            LoadNextLevel();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SaveLevelData()
    {
        if (SceneManager.GetActiveScene().buildIndex == _maxLevel)
        {
            PlayerPrefs.SetInt("SavedLevel", SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            PlayerPrefs.SetInt("SavedLevel", SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void SetTargetFrameRate()
    {
        Application.targetFrameRate = 60;
    }
}