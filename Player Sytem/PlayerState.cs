using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] private CurrentState currentState;

    public bool holdingKey = false;
    [HideInInspector] public bool usingTab = false;
    [HideInInspector] public bool aimingDownSights = false;
    [HideInInspector] public bool gamePaused = false;
    [HideInInspector] public bool readingJournal = false;


    [HideInInspector] public bool curerntlyOnMission = false;
    [HideInInspector] public bool completedMission = false;

    public enum CurrentState
    {
        none,
        currentlyTabbing,
        aimingDownSights,
        paused
    }

    private void Start()
    {
        currentState = CurrentState.none;
    }

    private void Update()
    {
        Conditions();
    }

    private void Conditions()
    {
        if (usingTab)
            currentState = CurrentState.currentlyTabbing;
        else if (aimingDownSights)
            currentState = CurrentState.aimingDownSights;
        else if (gamePaused)
            currentState = CurrentState.paused;
        else
            currentState = CurrentState.none;
    }

    public CurrentState GetCurrentState() => currentState;
}
