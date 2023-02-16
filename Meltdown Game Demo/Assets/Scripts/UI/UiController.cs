using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    static public UiController Instance;

    [Header("Displays")]
    [SerializeField] UI_MainMenu _mainMenu;
    [SerializeField] UI_PauseMenu _pauseMenu;
    [SerializeField] UI_Hud _uiHud;
    [SerializeField] UI_GameResultDisplay _gameResultDisplay;

    public UI_MainMenu MainMenu { get { return _mainMenu; } }
    public UI_PauseMenu PauseMenu { get { return _pauseMenu; } }
    public UI_Hud UiHud { get { return _uiHud; } }
    public UI_GameResultDisplay GameResultDisplay { get { return _gameResultDisplay; } }

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
}
