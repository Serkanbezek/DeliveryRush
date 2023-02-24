using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PackageDelivery : MonoBehaviour
{
    public static event Action PackageDelivered;

    [SerializeField] private Vector3[] _tunnelWayPoints = new Vector3[2];
    [SerializeField] private float _deliveryDelay = 0.08f;
    [SerializeField] private float _deliveryDuration = 0.5f;

    private Coroutine _deliveryCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null && DeliveryManager.Instance.PackagesOnPlayer.Count > 0)
        {
            _deliveryCoroutine = StartCoroutine(DeliverPackagesCoroutine(gameObject));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            if (_deliveryCoroutine != null)
            {
                StopCoroutine(_deliveryCoroutine);
            } 
        }
    }

    private IEnumerator DeliverPackagesCoroutine(GameObject deliveryTrigger)
    {
        int deliveryTunnelChildIndex = 1;
        Transform deliveryStation = deliveryTrigger.transform.parent;
        Transform deliveryTunnel = deliveryStation.GetChild(deliveryTunnelChildIndex);
        Vector3 tunnelEntrance = new(0, -1f, 0);
        Vector3 tunnelExit = new(0, 1f, 0f);
        for (int i = DeliveryManager.Instance.PackagesOnPlayer.Count - 1; i >= 0; i--)
        {
            if (DeliveryManager.Instance.PackagesOnPlayer.Count > i)
            {
                GameObject package = DeliveryManager.Instance.PackagesOnPlayer[i];
                DeliveryManager.Instance.PackagesOnPlayer.Remove(package);
                package.transform.SetParent(deliveryTunnel);
                PackageDelivered?.Invoke();
                _tunnelWayPoints[0] = tunnelEntrance;
                _tunnelWayPoints[1] = tunnelExit;
                package.transform.DOLocalPath(_tunnelWayPoints, _deliveryDuration);
                yield return new WaitForSeconds(_deliveryDelay);
            }
        }
    }

}
