using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
  private bool doingTrick;

  public static int ID;
  public PlayerAirState(int id) : base()
  {
    //ID = GetNewID();
    ID = id;

  }

  public override void Start()
  {
    Debug.Log("Player air state started");
  }

  public override void Stop()
  {

  }

  public override void HandleInput(PlayerController playerController)
  {
  }

  public override void Update()
  {
    //playerStats.Position += playerStats.Velocity * playerStats.Speed * Time.deltaTime;
    //playerStats.GetComponent<Rigidbody>().AddForce(playerStats.Velocity * playerStats.Speed * Time.deltaTime);
  }

  public override void OnCollisionEnter(Collision c)
  {
    if (Vector3.Dot(c.contacts[0].normal, Vector3.up) > 0.8 && !playerStats.DoingTrick)
    {
      // play impact animation, process combo score
      stateManager.SwitchState(PlayerRidingState.ID);
    }
    else
    {
      stateManager.SwitchState(PlayerFallingState.ID);
    }
  }

  public override void OnCollisionExit() { }
}
