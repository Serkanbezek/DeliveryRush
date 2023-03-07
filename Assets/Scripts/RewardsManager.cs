using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RewardsManager : MonoBehaviour
{
    [SerializeField] private GameObject _coinPileParent;

    public void GetCoins()
    {
        float transferDelay = 0f;
        _coinPileParent.SetActive(true);
        for (int i = 0; i < _coinPileParent.transform.childCount; i++)
        {
            Transform coin = _coinPileParent.transform.GetChild(i);
            coin.DOScale(0.28f, 0.3f).SetDelay(transferDelay).SetEase(Ease.OutBack);
            coin.GetComponent<RectTransform>().DOAnchorPos(new Vector2(353, 664.7f), 1f)
                .SetDelay(transferDelay + 0.5f).SetEase(Ease.InBack);
            transferDelay += 0.1f;
        }
    }
    
}
