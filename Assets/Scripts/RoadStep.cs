using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RoadStep : MonoBehaviour
{
    [SerializeField] private Color _roadStepColor;

    private Renderer _roadStepRenderer = null;
    private MaterialPropertyBlock _materialPropertyBlock = null;

    private void Start()
    {
        _roadStepRenderer = GetComponent<Renderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
        _materialPropertyBlock.SetColor("_Color", _roadStepColor);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            _roadStepRenderer.SetPropertyBlock(_materialPropertyBlock);
        }
    }

}
