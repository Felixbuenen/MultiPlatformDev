using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Trick
{
  public abstract bool Available { get; set; }
  public abstract string Pattern { get; }
  private static TrickExecuter trickExecuter;
  protected static TrickExecuter TrickExecuter
  {
    get
    {
      if (trickExecuter) return trickExecuter;

      trickExecuter = GameObject.Find("_TrickExecuter").GetComponent<TrickExecuter>();
      return trickExecuter;
    }
  }

  public Trick()
  {
    Available = true;
  }

  public abstract void DoExecute(GameObject player);
  public abstract void Evaluate(string recordedTrick); // sets trick parameters based on input
  protected abstract IEnumerator Execute(GameObject player);
}

public class Ollie : Trick
{
  private float maxJumpForce = 6f;
  private float jumpSpeed = 0f;
  private int maxPossibleJumpTime = 10; // num of frames player takes to go from crouch to jump

  public override bool Available { get; set; }

  public override string Pattern
  {
    get { return "501"; }
  }

  public Ollie() : base()
  {
  }

  public override void Evaluate(string recordedTrick)
  {
    // set ollie parameters
    // evaluate jump speed
    int numZeros = recordedTrick.Count(x => x == '0');
    jumpSpeed = 1 - (float)numZeros / (float)maxPossibleJumpTime; // DOESNT WORK

    Debug.Log("Num zeros: " + numZeros);
    Debug.Log("Recorded jump: " + recordedTrick);
    Debug.Log("Jump speed: " + jumpSpeed);
  }

  public override void DoExecute(GameObject player)
  {
    // too much multiplication, maybe minJumpValue + crouchValue * maxCrouchForce + jumpSpeed * maxJumpSpeed
    //  crouch is going wrong (gives 0)
    float jumpStrength = player.GetComponent<PlayerStats>().LastRecordedCrouchValue + jumpSpeed + maxJumpForce;

    player.GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpStrength, 0), ForceMode.Impulse);
    player.GetComponent<PlayerStateManager>().SwitchState(PlayerAirState.ID);
    TrickExecuter.StartCoroutine(Execute(player));
  }

  protected override IEnumerator Execute(GameObject player)
  {
    // do ollie
    Available = false;
    player.GetComponent<PlayerStats>().DoingTrick = true;
    Debug.Log("Doing ollie....");
    yield return new WaitForSeconds(.25f);
    player.GetComponent<PlayerStats>().DoingTrick = false;
    Debug.Log("Ollie succesful!");
    yield return new WaitForSeconds(1f); // wait to reset availability
    Available = true;
    Debug.Log("Ollie available again");
  }
}

public class Kickflip : Trick
{
  private float jumpSpeed;
  public override bool Available { get; set; }

  public override string Pattern
  {
    get { return "502"; }
  }

  public Kickflip() : base()
  {
  }

  public override void DoExecute(GameObject player)
  {
    player.GetComponent<Rigidbody>().AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
    player.GetComponent<PlayerStateManager>().SwitchState(PlayerAirState.ID);
    TrickExecuter.StartCoroutine(Execute(player));
  }

  public override void Evaluate(string recordedTrick)
  {
    // set kickflip parameters
  }
  protected override IEnumerator Execute(GameObject player)
  {
    // do kickflip
    Available = false;
    player.GetComponent<PlayerStats>().DoingTrick = true;
    Debug.Log("Doing kickflip....");
    yield return new WaitForSeconds(.25f);
    player.GetComponent<PlayerStats>().DoingTrick = false;
    Debug.Log("Kickflip succesful!");
    yield return new WaitForSeconds(1f); // wait to reset availability
    Available = true;
    Debug.Log("Kickflip available again");
  }
}
