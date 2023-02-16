using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GamePhase
{
    NONE = 0,
    Menu = 1,
    Game = 2
}

public class GameController : MonoBehaviour
{
    static public GameController Instance;

    [Header("Settings")]
    [SerializeField] GameRules _rules;

    [Header("Objects")]
    [SerializeField] Arena _arena;

    public bool GameIsPaused { get; private set; }
    public int CurrentRound { get; private set; }
    public GamePhase CurrentPhase { get; private set; }
    public Arena Arena { get { return _arena; } }
    public GamePlayer Player { get; private set; }

    public bool IsFinalRound()
    {
        return CurrentRound >= _rules.NumberOfRounds;
    }

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

    private void Start()
    {
        LoadPhase(GamePhase.Menu);
    }

    void StartGame()
    {
        Debug.Log("<b>Start game!</b>");
        _arena.Spinner.SetToStartingPosition();
        Player = _arena.SpawnPlayer();
        EnableCursor(false);
        CurrentRound = 1;
        InitializeRound();
    }

    void InitializeRound()
    {
        UiController.Instance.UiHud.SetRoundText(CurrentRound, IsFinalRound());
        UiController.Instance.UiHud.Timer.SetTimer(_rules.RoundDuration);

        float speed = _rules.GetSpeedForRound(CurrentRound);
        _arena.Spinner.SetSpeed(speed);

        bool shouldChangeDirection = _rules.AlwaysChangeDirection ? true : Random.Range(0, 1f) > 0.5f;
        if (shouldChangeDirection)
            _arena.Spinner.ReverseDirection();
    }

    void AdvanceRound()
    {
        CurrentRound = Mathf.Min(CurrentRound + 1, _rules.NumberOfRounds);
        InitializeRound();
    }

    void UnloadGame()
    {
        _arena.Spinner.RunSpinner(false);
        UiController.Instance.UiHud.Timer.StopTimer();

        if (Player != null && Player.gameObject != null)
            Destroy(Player.gameObject);
    }

    public void LoadPhase(GamePhase phase)
    {
        CurrentPhase = phase;

        UiController.Instance.MainMenu.DisplayModal(phase == GamePhase.Menu);

        switch (phase)
        {
            case GamePhase.Game: // Load and start game
                StartGame();
                UiController.Instance.UiHud.DisplayModal(true);
                break;

            case GamePhase.Menu: // Unload game and display menu
                UnloadGame();
                EnableCursor(true);
                CameraController.Instance.MoveToMenuPosition();
                UiController.Instance.UiHud.DisplayModal(false);
                UiController.Instance.MainMenu.DisplayModal(true);
                break;
        }
    }

    public void EnableCursor(bool show)
    {
        Cursor.lockState = show ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = show;
    }

    public void EndGame(GameResult result)
    {
        UnloadGame();
        UiController.Instance.GameResultDisplay.DisplayResult(result);
    }

    public void NotifyRoundTimeOut()
    {
        if (IsFinalRound())
        {
            EndGame(GameResult.Win);
            return;
        }

        AdvanceRound();
    }

    public void HandlePause(bool pause)
    {
        GameIsPaused = pause;
        Time.timeScale = pause ? 0f : 1f;

        EnableCursor(pause);
    }
}
