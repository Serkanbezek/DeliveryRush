using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndLine : MonoBehaviour
{
    [SerializeField] private Transform _lateDeliveryTrigger;
    [SerializeField] private float _movementDuration;
    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.DisableController();
            MovePlayerToLateDeliveryTrigger(other.transform);
        }
    }

    private void MovePlayerToLateDeliveryTrigger(Transform player)
    {
        Vector3 targetPos = _lateDeliveryTrigger.transform.position;
        targetPos.y = 0;
        player.DOMove(targetPos, _movementDuration).SetEase(Ease.Linear);
    }

}
