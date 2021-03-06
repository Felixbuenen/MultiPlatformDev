﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
  private PlayerState[] states;
  private PlayerState currentState;
  private PlayerController playerController;

  private PlayerStats playerStats;

  public PlayerState CurrentState { get { return currentState; } }

  private void Start()
  {
    // setup player state object
    playerStats = GetComponent<PlayerStats>();
    PlayerState.SetPlayerStats(playerStats);
    PlayerState.SetPlayerStateManager(this);

    states = new PlayerState[]
    {
      new PlayerRidingState(0),
      new PlayerCrouchState(1),
      new PlayerAirState(2),
      new PlayerFallingState(3)
    };
    Debug.Log("Initialize PlayerStateManager, riding ID: " + PlayerRidingState.ID);

    playerController = new PlayerController();

    // set initial state
    currentState = states[PlayerRidingState.ID];
    currentState.Start();
  }

  public void FixedUpdate()
  {
    playerController.Update();

    // pass player controller
    currentState.HandleInput(playerController);
    currentState.Update();
  }

  public void OnCollisionEnter(Collision c)
  {
    currentState.OnCollisionEnter(c);
  }

  public void OnCollisionExit()
  {
    currentState.OnCollisionExit();
  }

  public void SwitchState(int stateID)
  {
    currentState.Stop();

    currentState = states[stateID];
    currentState.Start();
  }
}