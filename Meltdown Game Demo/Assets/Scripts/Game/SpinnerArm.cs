using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerArm : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] bool _flipForward;

    [Header("Components")]
    [SerializeField] Spinner _parent;

    Vector3 GetForwardVector()
    {
        Vector3 result = gameObject.transform.forward;

        if (_flipForward)
            result *= -1f;

        switch (GameController.Instance.Arena.Spinner.CurrentDirection)
        {
            case SpinDirection.CounterClockwise:
                result *= -1f;
                break;
        }

        return result;
    }

#if UNITY_EDITOR
    private void Reset()
    {
        if (_parent == null)
            _parent = GetComponentInParent<Spinner>();
    }
#endif

    private void Awake()
    {
        Debug.Assert(_parent != null, $"Spinner arm parent null ({gameObject.name})");
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag(ProjectConstants.Tags.PLAYER))
        {
            GamePlayer player = obj.GetComponentInParent<GamePlayer>();

            if (player == null)
            {
                Debug.LogWarning($"Did not get GamePlayer component on GameObject tagged with {ProjectConstants.Tags.PLAYER}: {obj.name}");
                return;
            }

            _parent.ReportPlayerCollision(player, GetForwardVector());
        }
    }
}
