using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HammerObstacle : Obstacle
{
    [SerializeField] private Vector3 _rotateVector;
    [SerializeField] private float _rotationDuration;
    private Vector3 _initialRotation = new(-100, 90, 0);

    private void Start()
    {
        RotateHammerObstacle();
    }

    private void RotateHammerObstacle()
    {
        Sequence rotateSequence = DOTween.Sequence();
        rotateSequence.Append(transform.DOLocalRotate(_rotateVector, _rotationDuration).SetEase(Ease.OutBounce));
        rotateSequence.Append(transform.DOLocalRotate(_initialRotation, _rotationDuration).SetDelay(0.3f));
        rotateSequence.SetLoops(-1);
        rotateSequence.SetLink(transform.gameObject);
    }
}
