using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRidingState : PlayerState
{

  public static int ID;

  public PlayerRidingState()
  {
    ID = GetNewID();
  }

  public override void Start()
  {
    Debug.Log("Player riding state started.");
  }

  public override void Stop()
  {
    Debug.Log("Player riding state stopped");
  }

  public override void HandleInput()
  {
    if (Input.GetAxis("PS4_RightStickY") > 0.2f)
    {
      PlayerStateManager.instance.SwitchState(PlayerCrouchState.ID);
    }
    // check left - right movement
    // check ground trick input
  }

  public override void Update()
  {
    // update player position

  }
}
