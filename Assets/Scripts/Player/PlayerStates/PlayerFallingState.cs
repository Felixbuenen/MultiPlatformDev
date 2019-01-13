using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerState
{
  public static Action PlayerFell;
  public static Action PlayerRidingAgain;

  private TrickExecuter monoBehaviour;
  public static int ID;

  public PlayerFallingState(int id) : base()
  {
    //ID = GetNewID();
    ID = id;

    monoBehaviour = GameObject.Find("_TrickExecuter").GetComponent<TrickExecuter>();
  }

  public override void Start()
  {
    monoBehaviour.StartCoroutine(DoFallSequence());
  }
  public override void Stop()
  {

  }

  public override void HandleInput(PlayerController playerController)
  {
    // do nothing
  }
  public override void Update()
  {

  }

  private IEnumerator DoFallSequence()
  {
    // call event
    if (PlayerFell != null) PlayerFell();

    yield return new WaitForSeconds(2f);
    Vector3 currentPosition = playerStats.transform.position;
    Vector3 checkpointPosition = CheckpointTrigger.currentCheckpoint.transform.position;

    // 1.02 is an ugly magic number that sets the player to the 'grounded' position
    playerStats.transform.position = new Vector3(currentPosition.x, 1.02f, checkpointPosition.z);

    if (PlayerRidingAgain != null) PlayerRidingAgain();

    stateManager.SwitchState(PlayerRidingState.ID);
  }
}