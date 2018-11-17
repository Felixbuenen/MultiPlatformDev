using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRidingState : PlayerState
{

  public static int ID;

  public PlayerRidingState(PlayerStats playerStats) : base(playerStats)
  {
    ID = GetNewID();
  }

  public override void Start()
  {
    Debug.Log("Player riding state started.");
    playerStats.Velocity = Vector3.up;
  }

  public override void Stop()
  {
    Debug.Log("Player riding state stopped");
    playerStats.Velocity = Vector3.zero;
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

  public override void Update(float dt)
  {
    // update player position
    playerStats.Position += playerStats.Velocity * playerStats.Speed * dt;
  }
}
