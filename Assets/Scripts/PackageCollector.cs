using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PackageCollector : MonoBehaviour
{
    [SerializeField] private Transform _packageHolder;

    [SerializeField] private float _packageJumpPower;
    [SerializeField] private float _packageJumpDuration;
    [SerializeField] private float _spaceBetweenPackages;

    [SerializeField] private int _packageJumpNum;

    private Vector3 _newPackageTargetPosition = Vector3.zero;

    private void OnEnable()
    {
        DropPackages.PackageDropped += LowerPackageTargetPosition;
        PackageDelivery.PackageLeftPlayer += LowerPackageTargetPosition;
    }

    private void OnDisable()
    {
        DropPackages.PackageDropped -= LowerPackageTargetPosition;
        PackageDelivery.PackageLeftPlayer -= LowerPackageTargetPosition;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Package"))
        {
            CollectPackage(other.gameObject);
        }
    }

    private void CollectPackage(GameObject package)
    {
        package.GetComponent<BoxCollider>().enabled = false;
        package.transform.SetParent(_packageHolder);
        DeliveryManager.Instance.PackagesOnPlayer.Add(package);
        RaisePackageTargetPosition();
        package.transform.DOLocalJump(_newPackageTargetPosition, _packageJumpPower, _packageJumpNum, _packageJumpDuration).SetEase(Ease.Linear);
    }

    private void LowerPackageTargetPosition()
    {
        _newPackageTargetPosition.y -= _spaceBetweenPackages;
    }
    private void RaisePackageTargetPosition()
    {
        _newPackageTargetPosition.y += _spaceBetweenPackages;
    }
}
