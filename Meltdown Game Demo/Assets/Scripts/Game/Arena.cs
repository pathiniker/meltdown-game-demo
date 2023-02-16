using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Spinner _spinner;
    [SerializeField] Transform _playerSpawn;

    [Header("Prefabs")]
    [SerializeField] GamePlayer _playerPrefab;

    public Spinner Spinner { get { return _spinner; } }
    public Transform PlayerSpawn { get { return _playerSpawn; } }

    public GamePlayer SpawnPlayer()
    {
        GamePlayer newPlayer = Instantiate(_playerPrefab);
        newPlayer.transform.position = PlayerSpawn.position;
        CameraController.Instance.SetFollowPlayer(newPlayer);
        return newPlayer;
    }
}
