using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _swipeSpeedOffSet = 324;
    [SerializeField] private float _smoothTime;
    
    public bool IsControllerDisabled = false;

    private const float HorizontalLimit = 3f;

    private float _swipeSpeed;
    private float _defaultMoveSpeed;
    private float _defaultSwipeSpeed;
    private float _lastFrameFingerPosX;
    private Vector3 _playerVelocity = Vector3.zero;
    

    private void Awake()
    {
        _defaultMoveSpeed = _moveSpeed;
        _defaultSwipeSpeed = _swipeSpeedOffSet / Screen.width;
    }
    private void Start()
    {
        DisableController();
    }

    private void Update()
    {
        MoveForward();
        MovePlayerOnXAxis();
        ConstrainPlayer();
    }

    private void MovePlayerOnXAxis()
    {
        float inputValue = GetInputValue();
        Vector3 targetPos = transform.position;
        targetPos.x += inputValue * _swipeSpeed;
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

    public void EnableController()
    {
        _moveSpeed = _defaultMoveSpeed;
        _swipeSpeed = _defaultSwipeSpeed;
    }

    private float GetInputValue()
    {
        float differenceX = 0;
        if (Input.GetMouseButtonDown(0))
        {
            _lastFrameFingerPosX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            differenceX = Input.mousePosition.x - _lastFrameFingerPosX;
            _lastFrameFingerPosX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            differenceX = 0f;
        }
        return differenceX;
    }
}
