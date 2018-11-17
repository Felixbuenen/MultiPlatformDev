using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
  public static PlayerStateManager instance = null;

  private PlayerState[] states;
  private PlayerState currentState;

  private PlayerStats playerStats;

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
    playerStats = GetComponent<PlayerStats>();

    states = new PlayerState[]{
      new PlayerRidingState(playerStats),
      new PlayerCrouchState(playerStats)
    };

    // set initial state
    currentState = states[PlayerRidingState.ID];
    currentState.Start();
  }

  // Update is called once per frame
  void Update()
  {
    currentState.HandleInput();
    currentState.Update(Time.deltaTime);
  }

  void OnCollisionEnter(Collision c)
  {
    currentState.OnCollisionEnter(c);
  }

  void OnCollisionExit()
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
