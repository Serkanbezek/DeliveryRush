using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GuillotineArm : Obstacle
{
    [SerializeField] private Vector3 _rotateVector;
    [SerializeField] private Vector3 _rotateVector2 = Vector3.zero;


    private void Start()
    {
        MoveGuillotineArm();
    }


    private void MoveGuillotineArm()
    {
        transform.DORotate(_rotateVector, 1.5f).SetEase(Ease.OutBounce);
    }
}
