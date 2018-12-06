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

  public PlayerCrouchState() : base()
  {
    ID = GetNewID();
  }

  public override void Start()
  {
    Debug.Log("Player crouch state started");
    playerStats.Velocity = Vector3.zero;
  }

  public override void Stop()
  {
    Debug.Log("Player crouch state stopped");
    m_crouchvalue = 0.0f;
  }

  public override void HandleInput(PlayerController playerController)
  {
    if (playerController.GetRawTrickControl().y == 0)
    {
      stateManager.SwitchState(PlayerRidingState.ID);
      return; // switch state to rolling
    }

    // player fully crouched, increase jump force
    if (playerController.GetRawTrickControl().y == 1.0f)
    {
      // increase jump force
      Debug.Log("Increasing jump...");
    }

    else
    {
      m_crouchvalue = playerController.GetRawTrickControl().y;
      //Debug.Log("Jump force is: " + m_crouchvalue);
      Debug.Log("Non-optimal crouch");
    }
  }

  public override void Update()
  {
    // do state logic
    playerStats.Position += playerStats.Velocity * playerStats.Speed * Time.deltaTime;
  }

}
