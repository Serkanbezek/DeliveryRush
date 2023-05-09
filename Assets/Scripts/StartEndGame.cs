using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartEndGame : MonoBehaviour
{
    [SerializeField] private bool _isPlayerColliding = false;
    [SerializeField] private TunnelMoveUp _tunnelMoveUp;
    [SerializeField] private List<GameObject> _roadSteps = new();

    private ScoreManager _scoreManager;

    private void Start()
    {
        _scoreManager = ScoreManager.Instance;

        for (int i = 1; i < _roadSteps.Count; i++)
        {
            Vector3 roadStepPos = _roadSteps[i].transform.localPosition;
            roadStepPos.z = _roadSteps[i - 1].transform.localPosition.z + 5;
            _roadSteps[i].transform.localPosition = roadStepPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            _isPlayerColliding = true;
        }
    }
    private IEnumerator OnTriggerStay(Collider other)
    {
        if (_isPlayerColliding && DeliveryManager.Instance.PackagesOnPlayer.Count == 0)
        {
            _isPlayerColliding = false;
            _tunnelMoveUp.MoveTunnelUp();
            float levelScore = _scoreManager.GetLevelScore();
            float distanceToMove = levelScore / 5f + 4f;
            yield return new WaitForSeconds(1f);
            other.transform.DOMoveZ(other.transform.position.z + distanceToMove, 4).SetEase(Ease.OutQuart).SetLink(other.gameObject)
                .OnComplete(() =>
            _scoreManager.MultiplyLevelScore());
        }

    }
}
