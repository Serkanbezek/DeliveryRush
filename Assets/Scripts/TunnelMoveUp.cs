using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TunnelMoveUp : MonoBehaviour
{
    [SerializeField] private float _tunnelEndPos = 8;
    [SerializeField] private float _tunnelMoveDuration = 0.3f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            MoveTunnelUp();
        }
    }

    private void MoveTunnelUp()
    {
        int deliveryTunnelChildIndex = 1;
        Transform deliveryStation = transform.parent;
        Transform deliveryTunnel = deliveryStation.GetChild(deliveryTunnelChildIndex);
        deliveryTunnel.DOLocalMoveY(_tunnelEndPos, _tunnelMoveDuration).SetEase(Ease.InBack);
    }
}
