using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    static public CameraController Instance;

    [Header("Settings")]
    [SerializeField] Transform _mainMenuPosition;
    [SerializeField] Vector3 _playerOffset;
    [SerializeField] float _smoothTime = 0.3f;
    [SerializeField] float _mouseSensitivity = 1f;
    [SerializeField] float _minZoom = -2f;
    [SerializeField] float _maxZoom = 6f;

    Vector3 _velocity = Vector3.zero;
    Vector3 _zoomOffset = Vector3.zero;
    GamePlayer _followPlayer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void LateUpdate()
    {
        if (_followPlayer == null)
            return;

        FollowPlayer();
    }

    void FollowPlayer()
    {
        // update position
        Vector3 targetPosition = _followPlayer.transform.position + _playerOffset + _zoomOffset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);

        // update rotation
        transform.LookAt(_followPlayer.transform);
    }

    public void SetFollowPlayer(GamePlayer player)
    {
        _followPlayer = player;
    }

    public void MoveToMenuPosition()
    {
        gameObject.transform.position = _mainMenuPosition.position;
        transform.LookAt(GameController.Instance.Arena.Spinner.transform);
    }
}
