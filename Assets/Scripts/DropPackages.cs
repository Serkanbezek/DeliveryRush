using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class DropPackages : MonoBehaviour
{
    public static event Action PackageDropped;

    [SerializeField] private float _packageDefaultPositionY = 1;
    [SerializeField] private float _jumpPower = 1;
    [SerializeField] private float _jumpDuration = 0.8f;
    [SerializeField] private float _distanceOffsetLimitZ = 0.3f;
    [SerializeField] private float _timeRequiredToDropAgain = 1.5f;

    [SerializeField] private int _jumpNum = 1;
    [SerializeField] private int _dropAmount = 3;

    [SerializeField] private bool _isReadyToDrop = true;
    
    private const float RoadLimitX = 2.5f;


    private void OnTriggerEnter(Collider other)
    {
        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null && _isReadyToDrop)
        {
            _isReadyToDrop = false;
            PackageDrop();
            StartCoroutine(GetReadyToDrop());
        }
    }
   
    private void PackageDrop()
    {
        int packageAmountOnPlayer = DeliveryManager.Instance.PackagesOnPlayer.Count;
        if (packageAmountOnPlayer >= _dropAmount)
        {
            float dropDistanceZ = 6f;
            for (int packageIndex = packageAmountOnPlayer - 1; packageIndex >= packageAmountOnPlayer - _dropAmount; packageIndex--)
            {
                Drop(dropDistanceZ, packageIndex);
                dropDistanceZ -= 1;
            }
        }
        else if (packageAmountOnPlayer > 0 && packageAmountOnPlayer < _dropAmount)
        {
            float dropDistanceZ = 5f;
            for (int packageIndex = packageAmountOnPlayer - 1; packageIndex >= 0; packageIndex--)
            {
                Drop(dropDistanceZ, packageIndex);
                dropDistanceZ -= 1;
            }
        }
    }

    private void Drop(float dropDistanceZ, int packageIndex)
    {
        GameObject package = DeliveryManager.Instance.PackagesOnPlayer[packageIndex];
        DeliveryManager.Instance.PackagesOnPlayer.Remove(package);
        PackageDropped?.Invoke();
        package.transform.SetParent(null);
        Vector3 endPos = package.transform.position;
        endPos.z += dropDistanceZ + UnityEngine.Random.Range(-_distanceOffsetLimitZ, _distanceOffsetLimitZ);
        endPos.x = Mathf.Clamp(endPos.x, -RoadLimitX, RoadLimitX);
        endPos.y = _packageDefaultPositionY;
        package.transform.DOJump(endPos, _jumpPower, _jumpNum, _jumpDuration).SetEase(Ease.OutBounce).OnComplete(() =>
        Destroy(package));
    }

    private IEnumerator GetReadyToDrop()
    {
        yield return new WaitForSeconds(_timeRequiredToDropAgain);
        _isReadyToDrop = true;
    }
}
