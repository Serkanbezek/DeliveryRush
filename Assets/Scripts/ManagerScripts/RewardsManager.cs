using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RewardsManager : MonoBehaviour
{
    [SerializeField] private GameObject _coinPileParent;
    [SerializeField] private GameObject _targetCoin;
    [SerializeField] private Vector3[] _initialPos;

    private void Start()
    {
        for (int i = 0; i < _coinPileParent.transform.childCount; i++)
        {
            _initialPos[i] = _coinPileParent.transform.GetChild(i).position;
        }
    }

    private void OnEnable()
    {
        ScoreManager.LevelEnded += DeActivateCoinPileParent;
    }

    private void OnDisable()
    {
        ScoreManager.LevelEnded -= DeActivateCoinPileParent;
    }

    public void GetCoins()
    {
        float transferDelay = 0f;
        float defaultCoinScale = 0.28f;
        Vector2 targetCoinPos = _targetCoin.GetComponent<RectTransform>().localPosition;
        ResetCoinPilePos();
        _coinPileParent.SetActive(true);
        for (int i = 0; i < _coinPileParent.transform.childCount; i++)
        {
            Transform coin = _coinPileParent.transform.GetChild(i);
            coin.DOScale(defaultCoinScale, 0.3f).SetDelay(transferDelay).SetEase(Ease.OutBack).SetLink(coin.gameObject);
            coin.GetComponent<RectTransform>().DOAnchorPos(targetCoinPos, 1f)
                .SetDelay(transferDelay + 0.5f).SetEase(Ease.InBack).SetLink(coin.gameObject);
            coin.DOScale(0, 0.3f).SetDelay(transferDelay + 1.8f).SetEase(Ease.OutBack).SetLink(coin.gameObject);
            transferDelay += 0.1f;
        }
    }

    private void ResetCoinPilePos()
    {
        for (int i = 0; i < _coinPileParent.transform.childCount; i++)
        {
            _coinPileParent.transform.GetChild(i).position = _initialPos[i];
        }
    }

    private void DeActivateCoinPileParent()
    {
        _coinPileParent.SetActive(false);
    }


}
