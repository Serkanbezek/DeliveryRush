using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ImmovableSaw : Obstacle
{
    [SerializeField] private Vector3 _rotateVector;
    [SerializeField] private float _rotationDuration;


    private void Start()
    {
        RotateSawObstacle();
    }

    private void RotateSawObstacle()
    {
        transform.DOLocalRotate(_rotateVector, _rotationDuration).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear).SetLink(transform.gameObject);
    }
}
