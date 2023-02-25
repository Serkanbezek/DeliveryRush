using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class Obstacle : MonoBehaviour
{
    [SerializeField] private float _pushPower = 10f;
    [SerializeField] private float _pushDuration = 1.5f;


    private readonly float _shakeStrength = 0.05f;
    private readonly int _shakeVibrato = 20;
    private readonly float _shakeRandomness = 90;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null && !playerController.IsControllerDisabled)
        {
            PushPlayerBack(other.gameObject);
            DisablePlayerController(other.gameObject);
        }
    }

    private void PushPlayerBack(GameObject player)
    {
        float pushEndPosZ = player.transform.position.z - _pushPower;
        player.transform.DOShakePosition(_pushDuration, _shakeStrength, _shakeVibrato, _shakeRandomness);
        player.transform.DOMoveZ(pushEndPosZ, _pushDuration).SetEase(Ease.OutCirc);
    }

    private void DisablePlayerController(GameObject player)
    {
        StartCoroutine(player.GetComponent<PlayerController>().DisableControllerCoroutine(_pushDuration));
    }
}
