using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerState
{
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
    Debug.Log("Player fell! :(");
    yield return new WaitForSeconds(3f);
    stateManager.SwitchState(PlayerRidingState.ID);
  }
}
