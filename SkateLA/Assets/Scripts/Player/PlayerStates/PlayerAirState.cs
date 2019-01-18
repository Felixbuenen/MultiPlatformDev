using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
  private bool doingTrick;
  private Animator animator;
  private float currentJumpForce;
  private Rigidbody rb;

  public static int ID;
  public PlayerAirState(int id) : base()
  {
    //ID = GetNewID();
    ID = id;
    animator = playerStats.GetComponentInChildren<Animator>();
    currentJumpForce = 0.0f;
    rb = playerStats.GetComponent<Rigidbody>();
  }

  public override void Start()
  {
    Debug.Log("Player air state started");
    //animator.SetFloat("CrouchValue", 1f);
  }

  public override void Stop()
  {
    //animator.SetFloat("CrouchValue", 0f);
    currentJumpForce = 0.0f;
  }

  public override void HandleInput(PlayerController playerController)
  {
    List<Trick> tricks = playerController.GetTricks();
    //foreach (Trick trick in tricks) Debug.Log(trick.Name);

    if (tricks.Count != 0)
    {
      Trick latestTrick = tricks[tricks.Count - 1];
      if (latestTrick.IsAirTrick && !playerStats.DoingTrick)
      {
        tricks.RemoveAt(tricks.Count - 1);
        latestTrick.DoExecute(stateManager.gameObject);
        ScoreManager.Instance.AddTrickToSequence(latestTrick);
      }
    }
  }

  public override void Update()
  {
    if (currentJumpForce == 0.0f && rb.velocity.y != 0.0f)
    {
      currentJumpForce = rb.velocity.y;
    }

    animator.SetFloat("CrouchValue", 1f - Mathf.Abs(rb.velocity.y) * 1.25f / currentJumpForce);
  }

  public override void OnCollisionEnter(Collision c)
  {
    if (Vector3.Dot(c.contacts[0].normal, Vector3.up) > 0.8 && !playerStats.DoingTrick)
    {
      Debug.Log("Score sequence should be stopped...");
      ScoreManager.Instance.StopScoreSequence();

      // play impact animation, process combo score
      stateManager.SwitchState(PlayerRidingState.ID);
    }
    else
    {
      ScoreManager.Instance.PlayerFell();

      stateManager.SwitchState(PlayerFallingState.ID);
    }
  }

  public override void OnCollisionExit() { }
}
