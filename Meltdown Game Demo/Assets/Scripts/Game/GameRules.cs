using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Game Rules Definition")]
public class GameRules : ScriptableObject
{
    public int NumberOfRounds = 5;
    public float SpeedIncreasePerRound = 30f;
    public float RoundDuration = 10f;
    public bool AlwaysChangeDirection = false;

    public float GetSpeedForRound(int round)
    {
        return round * SpeedIncreasePerRound;
    }
}
