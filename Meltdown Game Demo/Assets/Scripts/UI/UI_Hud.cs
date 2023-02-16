using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UI_Hud : AModal
{
    [Header("Components")]
    [SerializeField] TextMeshProUGUI _roundText;
    [SerializeField] RoundTimer _timer;
    [SerializeField] GameObject _reverseText;

    public RoundTimer Timer { get { return _timer; } }

    public void SetRoundText(int round, bool isFinalRound = false)
    {
        string text = isFinalRound ? "FINAL ROUND" : $"Round <b>{round}</b>";
        _roundText.SetText(text);
    }

    public void DoReverseText()
    {
        _reverseText.transform.DOPunchScale(Vector3.one, 2f, vibrato: 1);
    }
}
