using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAction
{
    NONE = 0,
    Jump = 1,
    Duck = 2
}

public class PlayerControl : MonoBehaviour
{
    const string JUMPING = "IsJumping";
    const string RUNNING = "IsRunning";
    const string DUCKING = "IsDucking";
    const string VELOCITY = "Velocity";

    [Header("Settings")]
    [SerializeField] float _moveSpeed;
    [SerializeField] float _jumpForce;
    [SerializeField] float _gravity = -9.81f;

    [Header("Controls")]
    [SerializeField] KeyCode _jumpButton = KeyCode.Space;
    [SerializeField] KeyCode _duckButton = KeyCode.C;
    [SerializeField] KeyCode _pauseButton = KeyCode.Escape;

    [Header("Components")]
    [SerializeField] CharacterController _char;
    [SerializeField] Animator _anim;

    bool _wasGroundedLastFrame = true;
    PlayerAction _currentAction = PlayerAction.NONE;
    Vector3 _currentMotion;

    public bool IsAlive { get; private set; }

    bool IsDoingAction()
    {
        return _currentAction != PlayerAction.NONE;
    }

    private void OnEnable()
    {
        _char.detectCollisions = false;
        _currentMotion = Vector3.zero;
        _currentAction = PlayerAction.NONE;
        MarkPlayerAsAlive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(_pauseButton))
            UiController.Instance.PauseMenu.DisplayModal(true);

        if (GameController.Instance.GameIsPaused)
            return;

        if (!IsAlive)
            return;

        HandleMovement();
    }

    void HandleMovement()
    {
        bool isGrounded = _char.isGrounded;
        if (isGrounded && _currentMotion.y < 0)
            _currentMotion.y = 0f;

        if (isGrounded)
        {
            // Just landed, end jump animation
            if (!_wasGroundedLastFrame)
                HandleJump(false);

            if (Input.GetKeyDown(_jumpButton))
            {
                _currentMotion.y += Mathf.Sqrt(_jumpForce * -3.0f * _gravity);
                HandleJump(true);
            }

            bool isJumping = _currentAction == PlayerAction.Jump;
            if (!isJumping)
                HandleDuck(Input.GetKey(_duckButton));
        }

        bool isDucking = _currentAction == PlayerAction.Duck;
        Vector3 lateralMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Prohibit movement while ducking
        if (!isDucking)
            _char.Move(lateralMovement * Time.deltaTime * _moveSpeed);

        // Face move direction
        if (lateralMovement != Vector3.zero)
            gameObject.transform.forward = lateralMovement;

        _currentMotion.y += _gravity * Time.deltaTime;

        if (!isDucking)
        {
            _char.Move(_currentMotion * Time.deltaTime);
            _anim.SetFloat(VELOCITY, _currentMotion.sqrMagnitude);
        }

        HandleRunAnimation(lateralMovement);
        _wasGroundedLastFrame = isGrounded;
    }

    void HandleJump(bool doAction)
    {
        bool isJumping = _currentAction == PlayerAction.Jump;
        _currentAction = doAction ? PlayerAction.Jump : PlayerAction.NONE;

        if ((isJumping && doAction) || (!isJumping && !doAction))
            return;

        _anim.SetBool(JUMPING, doAction);
    }

    void HandleDuck(bool doAction)
    {
        bool isDucking = _currentAction == PlayerAction.Duck;
        _currentAction = doAction ? PlayerAction.Duck : PlayerAction.NONE;

        if ((isDucking && doAction) || (!isDucking && !doAction))
            return;

        _anim.SetBool(DUCKING, doAction);
    }

    void HandleRunAnimation(Vector3 lateralMovement)
    {
        bool isRunning = _anim.GetBool(RUNNING);
        bool shouldRun = !IsDoingAction() && lateralMovement.sqrMagnitude > 0.1f;

        if ((isRunning && shouldRun) || (!isRunning && !shouldRun))
            return;

        _anim.SetBool(RUNNING, shouldRun);
    }

    public void MarkPlayerAsAlive(bool alive)
    {
        IsAlive = alive;
    }
}
