using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GameResult
{
    NONE = 0,
    Win = 1,
    Loss = 2
}

public class UI_GameResultDisplay : AModal
{
    [Header("Settings")]
    [SerializeField] string _winPhrase;
    [SerializeField] string _lossPhrase;

    [Header("Components")]
    [SerializeField] TextMeshProUGUI _resultText;
    [SerializeField] TextMeshProUGUI _roundEndText;

    string GetResultText(GameResult result)
    {
        switch (result)
        {
            case GameResult.Win:
                return _winPhrase;

            case GameResult.Loss:
                return _lossPhrase;

            default:
                return "";
        }
    }

    public void DisplayResult(GameResult result)
    {
        GameController.Instance.EnableCursor(true);
        _resultText.SetText(GetResultText(result));

        string roundEndText = "";
        if (result == GameResult.Loss)
            roundEndText = $"You survived <b>{GameController.Instance.CurrentRound}</b> rounds.";

        _roundEndText.SetText(roundEndText);

        DisplayModal(true);
    }

    #region UI Callbacks
    public void OnClick_PlayAgain()
    {
        DisplayModal(false);
        GameController.Instance.LoadPhase(GamePhase.Game);
    }

    public void OnClick_Quit()
    {
        DisplayModal(false);
        GameController.Instance.LoadPhase(GamePhase.Menu);
    }
    #endregion
}
