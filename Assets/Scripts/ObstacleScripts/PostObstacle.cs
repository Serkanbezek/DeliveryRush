using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PostObstacle : Obstacle
{
    [SerializeField] private float _targetPosX;
    [SerializeField] private float _movementDuration;
    [SerializeField] private Vector3 _rotateVector;
    [SerializeField] private float _rotationDuration;
    [SerializeField] private bool _isObstacleMoving;
    [SerializeField] private bool _isObstacleRotating;

    private void Start()
    {
        if (_isObstacleMoving)
        {
            MovePostObstacleOnX();
        }
        if (_isObstacleRotating)
        {
            RotatePostObstacle();
        }
    }

    private void MovePostObstacleOnX()
    {
        transform.DOLocalMoveX(_targetPosX, _movementDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear).SetLink(transform.gameObject);
    }

    private void RotatePostObstacle()
    {
        transform.DORotate(_rotateVector, _rotationDuration).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear).SetLink(transform.gameObject);
    }
}
