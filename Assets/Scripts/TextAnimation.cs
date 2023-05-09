using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 scaleRate = new(1.2f, 1.2f, 1.2f);
    [SerializeField] private float scaleDuration = 0.8f;

    private void Start()
    {
        ShopManager.Instance.ActivateShopButton();
        AnimateText();
    }

    private void AnimateText()
    {
        transform.DOScale(scaleRate, scaleDuration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetLink(gameObject);
    }

    public void DestroyTapToPlayButton()
    {
        GameObject TapToPlayButton = transform.parent.gameObject;
        ShopManager.Instance.DeactivateShopButton();
        Destroy(TapToPlayButton);
    }
}
