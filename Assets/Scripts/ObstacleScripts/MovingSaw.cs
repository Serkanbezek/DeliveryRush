using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingSaw : Obstacle
{
    [SerializeField] private float _targetPosX;
    [SerializeField] private float _movementDuration;
    [SerializeField] private Vector3 _rotateVector;
    [SerializeField] private float _rotationDuration;


    private void Start()
    {
        MoveSawObstacleOnX();
        RotateSawObstacle();
    }

    private void MoveSawObstacleOnX()
    {
        transform.DOLocalMoveX(_targetPosX, _movementDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear).SetLink(transform.gameObject);
    }

    private void RotateSawObstacle()
    {
        transform.DORotate(_rotateVector, _rotationDuration).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear).SetLink(transform.gameObject);
    }
}
