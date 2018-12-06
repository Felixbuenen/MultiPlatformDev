using System.Collections;
using System.Collections.Generic;
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
  private float jumpSpeed;
  public override bool Available { get; set; }

  public override string Pattern
  {
    get { return "51"; }
  }

  public Ollie() : base()
  {
  }

  public override void Evaluate(string recordedTrick)
  {
    // set ollie parameters
    Debug.Log("Ollie evaluated...");
  }

  public override void DoExecute(GameObject player)
  {
    Available = false;

    player.GetComponent<Rigidbody>().AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
    //TrickExecuter.StartCoroutine(Execute());
  }

  protected override IEnumerator Execute(GameObject player)
  {
    // do ollie
    Available = false;
    Debug.Log("Doing ollie....");
    yield return new WaitForSeconds(3f);
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
    get { return "52"; }
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
