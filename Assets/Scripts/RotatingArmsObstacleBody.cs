using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotatingArmsObstacleBody : Obstacle
{
    [SerializeField] private Vector3 _rotateVector;

    [SerializeField] private float _rotationDuration;


    private void Start()
    {
        RotateObstacleBody();
    }


    private void RotateObstacleBody()
    {
        transform.DORotate(_rotateVector, _rotationDuration).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }
}
