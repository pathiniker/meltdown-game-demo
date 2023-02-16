using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MainMenu : AModal
{
    public override void DisplayModal(bool show)
    {
        base.DisplayModal(show);

        if (show)
            GameController.Instance.Arena.Spinner.SetSpeed(45f);
    }

    #region UI Callbacks
    public void OnClick_PlayGame()
    {
        GameController.Instance.LoadPhase(GamePhase.Game);
    }

    public void OnClick_Quit()
    {
        Application.Quit();
    }
    #endregion
}
