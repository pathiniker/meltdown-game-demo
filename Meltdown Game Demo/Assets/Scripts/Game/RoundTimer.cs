using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundTimer : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] TextMeshProUGUI _timeRemainingText;

    bool _shouldRun = false;
    float _timeRemaining;

    public void SetTimer(float time)
    {
        _timeRemaining = time;
        _shouldRun = true;
    }

    public void StopTimer()
    {
        _shouldRun = false;
    }

    private void Update()
    {
        if (!_shouldRun)
            return;

        if (_timeRemaining <= 0)
        {
            GameController.Instance.NotifyRoundTimeOut();
        } else
        {
            _timeRemaining -= Time.deltaTime;
            _timeRemainingText.SetText(Mathf.RoundToInt(_timeRemaining).ToString());
        }
    }
}
