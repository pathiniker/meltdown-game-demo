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

    [Header("Components")]
    [SerializeField] Transform _spinTransform;

    SpinDirection _currentDirection = SpinDirection.NONE;
    float _currentSpeed;

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
        _currentDirection = SpinDirection.Clockwise;
        _currentSpeed = Random.Range(_minSpeed, _maxSpeed);
    }

    private void Update()
    {
        Vector3 rotate = GetRotateEulersForDirection(_currentDirection);
        float speed = _currentSpeed * Time.deltaTime;
        _spinTransform.Rotate(rotate * speed);
    }
}
