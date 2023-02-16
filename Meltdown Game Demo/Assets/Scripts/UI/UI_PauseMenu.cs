using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PauseMenu : AModal
{
    public override void DisplayModal(bool show)
    {
        base.DisplayModal(show);
        GameController.Instance.HandlePause(show);
    }

    #region UI Callbacks
    public void OnClick_Continue()
    {
        DisplayModal(false);
    }

    public void OnClick_Quit()
    {
        DisplayModal(false);
        GameController.Instance.LoadPhase(GamePhase.Menu);
    }
    #endregion
}
