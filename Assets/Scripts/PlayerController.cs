using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _swipeSpeed;
    [SerializeField] private float _smoothTime;
    [SerializeField] private GameInput _gameInput;

    private const float HorizontalLimit = 3f;

    private float _defaultMoveSpeed;
    private float _defaultSwipeSpeed;
    private Vector3 _playerVelocity = Vector3.zero;
    public bool IsControllerDisabled = false;

    private void Start()
    {
        _defaultMoveSpeed = _moveSpeed;
        _defaultSwipeSpeed = _swipeSpeed;
    }

    private void Update()
    {
        MoveForward();
        MovePlayerOnXAxis();
        ConstrainPlayer();
    }

    private void MovePlayerOnXAxis()
    {
        float inputValue = _gameInput.GetMovementValue();
        Vector3 targetPos = transform.position;
        targetPos.x += (inputValue * _swipeSpeed);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _playerVelocity, _smoothTime);
    }

    private void MoveForward() => transform.position += _moveSpeed * Time.deltaTime * Vector3.forward;

    private void ConstrainPlayer()
    {
        var tempPos = transform.position;
        tempPos.x = Mathf.Clamp(tempPos.x, -HorizontalLimit, HorizontalLimit);
        transform.position = tempPos;
    }


    public IEnumerator DisableControllerCoroutine(float disableDuration)
    {
        DisableController();
        IsControllerDisabled = true;
        yield return new WaitForSeconds(disableDuration);
        EnableController();
        IsControllerDisabled = false;
    }

    public void DisableController()
    {
        _moveSpeed = 0;
        _swipeSpeed = 0;
    }

    private void EnableController()
    {
        _moveSpeed = _defaultMoveSpeed;
        _swipeSpeed = _defaultSwipeSpeed;
    }
}
