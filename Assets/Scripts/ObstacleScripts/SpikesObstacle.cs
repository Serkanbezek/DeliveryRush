using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpikesObstacle : Obstacle
{
    [SerializeField] private float _targetPosY;
    [SerializeField] private float _movementDuration;


    private void Start()
    {
        MoveSpikesObstacleOnY();
    }

    private void MoveSpikesObstacleOnY()
    {
        transform.DOLocalMoveY(_targetPosY, _movementDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCubic).SetLink(transform.gameObject);
    }
}
