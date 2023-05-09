using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayerForScoreManager : MonoBehaviour
{
    private void Awake()
    {
        ScoreManager.Instance.FindLevelScorePanelOnPlayer();
    }

    private void Start()
    {
        ScoreManager.Instance.GetLevelScoreTextOnPlayer();
    }
}
