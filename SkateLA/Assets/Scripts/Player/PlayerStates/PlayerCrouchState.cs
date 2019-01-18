using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : PlayerState
{
  float m_crouchvalue; // used to determine jump strength
  private float _maxCrouchValueTime = 2f;

  public static int ID;

  public float CrouchValue
  {
    get { return m_crouchvalue / _maxCrouchValueTime; } // normalized for multiplication
  }

  public PlayerCrouchState(int id) : base()
  {
    //ID = GetNewID();
    ID = id;
  }

  public override void Start()
  {
    Debug.Log("Player crouch state started");

    // reset state
    m_crouchvalue = 0.0f;
    playerStats.LastRecordedCrouchValue = 0.0f;
    //playerStats.Velocity = Vector3.zero;
  }

  public override void Stop()
  {
    Debug.Log("Player crouch state stopped");
  }

  public override void HandleInput(PlayerController playerController)
  {
    if (playerController.GetRawTrickControl().y == 0)
    {
      playerStats.LastRecordedCrouchValue = m_crouchvalue;
      stateManager.SwitchState(PlayerRidingState.ID);
      return; // switch state to rolling
    }

    playerStats.Velocity = new Vector3(playerController.GetMovement().x, playerStats.Velocity.y, 1);
  }

  public override void Update()
  {
    // increase crouch time
    m_crouchvalue += Time.deltaTime;
    m_crouchvalue = Mathf.Min(m_crouchvalue, _maxCrouchValueTime);

    // do state logic
    float steerVal = playerStats.Velocity.x * playerStats.SteerSpeed;
    float forwardVal = playerStats.Velocity.z * playerStats.Speed;
    Vector3 vel = new Vector3(steerVal, playerStats.Velocity.y, forwardVal);

    playerStats.GetComponent<Rigidbody>().velocity = vel * Time.deltaTime;
  }

}
