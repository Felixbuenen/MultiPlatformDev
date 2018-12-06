using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
  private PlayerState[] states;
  private PlayerState currentState;
  private PlayerController playerController;

  private PlayerStats playerStats;

  private void Awake()
  {
    // setup player state object
    playerStats = GetComponent<PlayerStats>();
    PlayerState.SetPlayerStats(playerStats);
    PlayerState.SetPlayerStateManager(this);

    states = new PlayerState[]{
      new PlayerRidingState(),
      new PlayerCrouchState(),
      new PlayerAirState(),
      new PlayerFallingState()
    };

    playerController = new PlayerController();

    // set initial state
    currentState = states[PlayerRidingState.ID];
    currentState.Start();
  }

  public void FixedUpdate()
  {
    //Debug.Log("Current state: " + currentState);
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
    //if (currentState == null) Debug.Log("NO CURRENT STATE");
    currentState.Stop();

    //if (states[stateID] == null) Debug.Log("COULDN'T FIND STATE IN BUFFER");
    currentState = states[stateID];
    currentState.Start();
  }
}
