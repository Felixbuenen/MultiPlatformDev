using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRidingState : PlayerState
{
  public static int ID;
  private Animator animator;

  private Vector3 previousPosition;

  public PlayerRidingState(int id) : base()
  {
    //ID = GetNewID();
    ID = id;
    animator = playerStats.GetComponentInChildren<Animator>();
  }

  public override void Start()
  {
    Debug.Log("Player riding state started.");
    playerStats.Velocity = Vector3.forward;
    previousPosition = Vector3.zero;
  }

  public override void Stop() { }

  public override void HandleInput(PlayerController playerController)
  {
    //Debug.Log(playerController.GetRawTrickControl().y);
    animator.SetFloat("CrouchValue", playerController.GetRawTrickControl().y * -1f);

    if (playerController.GetRawTrickControl().y > 0.5f)
    {
      stateManager.SwitchState(PlayerCrouchState.ID);
    }

    List<Trick> tricks = playerController.GetTricks();
    if (tricks.Count != 0)
    {
      Trick latestTrick = tricks[tricks.Count - 1];
      tricks.RemoveAt(tricks.Count - 1);

      if (!latestTrick.IsAirTrick)
      {
        if (!playerStats.DoingTrick)
        {
          latestTrick.DoExecute(stateManager.gameObject);
          ScoreManager.Instance.AddTrickToSequence(latestTrick);
        }

        // remove automatically added air tricks
        foreach (Trick trick in tricks)
        {
          if (trick.IsAirTrick) tricks.Remove(trick);
        }
      }
    }

    playerStats.Velocity = new Vector3(playerController.GetMovement().x, playerStats.Velocity.y, 1);
    //Debug.Log("Number of tricks in queue: " + playerController.GetTricks().Count);
    // check left - right movement
    // check ground trick input
  }

  public override void OnCollisionEnter(Collision c)
  {

  }

  public override void Update()
  {
    // update player position
    //playerStats.Position += playerStats.Velocity * playerStats.Speed * Time.deltaTime;
    float steerVal = playerStats.Velocity.x * playerStats.SteerSpeed;
    float forwardVal = playerStats.Velocity.z * playerStats.Speed;
    Vector3 vel = new Vector3(steerVal, playerStats.Velocity.y, forwardVal);

    playerStats.GetComponent<Rigidbody>().velocity = vel * Time.deltaTime;

    // player stuck
    if ((previousPosition - playerStats.transform.position).magnitude < 0.002f)
    {
      stateManager.SwitchState(PlayerFallingState.ID);
    }

    previousPosition = playerStats.transform.position;

  }
}