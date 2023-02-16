using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GamePlayer : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] PlayerControl _control;

    private void Reset()
    {
        if (_control == null)
            _control = GetComponent<PlayerControl>();
    }

    void HandleDeath(float speed, Vector3 armForward)
    {
        Vector3 throwVelocity = armForward;
        throwVelocity *= speed / 4f;
        gameObject.transform.DOBlendableMoveBy(throwVelocity, 0.2f);
        StartCoroutine(FinishDeath());
    }

    IEnumerator FinishDeath()
    {
        yield return new WaitForSeconds(0.3f);
        GameController.Instance.EndGame(GameResult.Loss);
        yield break;
    }

    public void OnSpinnerCollision(float collisionSpeed, Vector3 armForward)
    {
        Debug.Log($"Hit by spinner! Collision speed: {collisionSpeed}");
        _control.MarkPlayerAsAlive(false);
        HandleDeath(collisionSpeed, armForward);
    }
}
