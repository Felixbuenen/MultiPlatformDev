using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
  public static PlayerStateManager instance = null;
  private PlayerState currentState;

  private PlayerState[] states;

  void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
    else if (instance != this)
    {
      Destroy(gameObject);
    }

    DontDestroyOnLoad(gameObject);
  }

  void Start()
  {
    states = new PlayerState[]{
      new PlayerRidingState(),
      new PlayerCrouchState()
    };

    // set initial state
    currentState = states[PlayerRidingState.ID];
    currentState.Start();
  }

  // Update is called once per frame
  void Update()
  {
    currentState.HandleInput();
    currentState.Update();
  }

  public void SwitchState(int stateID)
  {
    currentState.Stop();

    currentState = states[stateID];
    currentState.Start();
  }
}
