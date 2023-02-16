using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpinDirection
{
    NONE = 0,
    Clockwise = 1,
    CounterClockwise = 2
}

public class Spinner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float _minSpeed;
    [SerializeField] float _maxSpeed;
    [SerializeField] float _currentSpeed;
    [SerializeField] SpinDirection _startDirection = SpinDirection.Clockwise;

    [Header("Components")]
    [SerializeField] Transform _spinTransform;

    public SpinDirection CurrentDirection { get; private set; }
    
    Vector3 GetRotateEulersForDirection(SpinDirection direction)
    {
        switch (direction)
        {
            case SpinDirection.Clockwise:
                return Vector3.up;

            case SpinDirection.CounterClockwise:
                return Vector3.down;

            default:
                return Vector3.zero;
        }
    }

    private void Start()
    {
        SetDirection(_startDirection);
    }

    private void Update()
    {
        DoSpin();
    }

    void DoSpin()
    {
        Vector3 rotate = GetRotateEulersForDirection(CurrentDirection);
        float speed = _currentSpeed * Time.deltaTime;
        _spinTransform.Rotate(rotate * speed);
    }

    public void SetToStartingPosition()
    {
        transform.eulerAngles = Vector3.zero;
    }

    public void SetDirection(SpinDirection direction)
    {
        CurrentDirection = direction;
    }

    public void SetSpeed(float speed)
    {
        _currentSpeed = speed;
    }

    public void RunSpinner(bool run)
    {
        _currentSpeed = run ? _currentSpeed : 0f;
    }

    public void ReverseDirection()
    {
        CurrentDirection = CurrentDirection switch
        {
            SpinDirection.Clockwise => SpinDirection.CounterClockwise,
            SpinDirection.CounterClockwise => SpinDirection.Clockwise,
            _ => SpinDirection.Clockwise
        };

        if (GameController.Instance.CurrentRound > 1)
            UiController.Instance.UiHud.DoReverseText();
    }

    public void ReportPlayerCollision(GamePlayer player, Vector3 armForward)
    {
        player.OnSpinnerCollision(_currentSpeed, armForward);
    }
}
