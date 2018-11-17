using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : PlayerState
{
  float m_crouchvalue; // used to determine jump strength
  public static int ID;

  public float CrouchValue
  {
    get { return m_crouchvalue; }
    set { m_crouchvalue = value; }
  }

  public PlayerCrouchState(PlayerStats playerStats) : base(playerStats)
  {
    ID = GetNewID();
  }

  public override void Start()
  {
    Debug.Log("Player crouch state started");
    //playerStats.Velocity = Vector3.zero;
  }

  public override void Stop()
  {
    Debug.Log("Player crouch state stopped");
    m_crouchvalue = 0.0f;
  }

  public override void HandleInput()
  {
    if (Input.GetAxisRaw("PS4_RightStickY") == 0)
    {
      PlayerStateManager.instance.SwitchState(PlayerRidingState.ID);
      return; // switch state to rolling
    }

    // player fully crouched, increase jump force
    if (Input.GetAxis("PS4_RightStickY") == 1.0f)
    {
      // increase jump force
      Debug.Log("Increasing jump...");
    }

    else
    {
      m_crouchvalue = Input.GetAxis("PS4_RightStickY");
      //Debug.Log("Jump force is: " + m_crouchvalue);
      Debug.Log("Non-optimal crouch");
    }
  }

  public override void Update(float dt)
  {
    // do state logic
    playerStats.Position += playerStats.Velocity * playerStats.Speed * dt;
  }

}
