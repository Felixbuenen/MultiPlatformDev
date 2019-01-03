using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Trick
{
  public abstract bool Available { get; set; }
  public abstract bool IsAirTrick { get; }
  public abstract string Pattern { get; }
  public abstract int Score { get; }
  public abstract string Name { get; }

  protected PlayerStats playerStats;

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
    playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
  }

  public virtual void DoExecute(GameObject player)
  {
    Available = false;
    playerStats.DoingTrick = true;
  }

  public abstract void Evaluate(string recordedTrick); // sets trick parameters based on input
  protected abstract IEnumerator Execute(GameObject player);
}

public class Ollie : Trick
{
  private float maxJumpForce = 6f;
  private float jumpSpeed = 0f;
  private int maxPossibleJumpTime = 10; // num of frames player takes to go from crouch to jump

  public override bool Available { get; set; }
  public override bool IsAirTrick { get { return false; } }

  public override string Pattern
  {
    get { return "501"; }
  }

  public override int Score { get { return 100; } }
  public override string Name { get { return "Ollie"; } }

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
    base.DoExecute(player);

    float jumpStrength = player.GetComponent<PlayerStats>().LastRecordedCrouchValue + jumpSpeed + maxJumpForce;

    player.GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpStrength, 0), ForceMode.Impulse);
    player.GetComponent<PlayerStateManager>().SwitchState(PlayerAirState.ID);
    TrickExecuter.StartCoroutine(Execute(player));
  }

  protected override IEnumerator Execute(GameObject player)
  {
    // do ollie
    Debug.Log("Doing ollie....");
    yield return new WaitForSeconds(.25f);
    playerStats.DoingTrick = false;
    Debug.Log("Ollie succesful!");
    yield return new WaitForSeconds(.5f); // wait to reset availability
    Available = true;
    Debug.Log("Ollie available again");
  }
}

public class Kickflip : Trick
{
  private float jumpSpeed;
  public override bool Available { get; set; }
  public override bool IsAirTrick { get { return false; } }

  public override string Pattern
  {
    get { return "502"; }
  }

  public override int Score { get { return 200; } }
  public override string Name { get { return "Kickflip"; } }

  public Kickflip() : base()
  {
  }

  public override void DoExecute(GameObject player)
  {
    base.DoExecute(player);

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
    Debug.Log("Doing kickflip....");
    yield return new WaitForSeconds(.25f);
    playerStats.DoingTrick = false;
    Debug.Log("Kickflip succesful!");
    yield return new WaitForSeconds(.5f); // wait to reset availability
    Available = true;
    Debug.Log("Kickflip available again");
  }
}

public class Heelflip : Trick
{
  private float jumpSpeed;
  public override bool Available { get; set; }
  public override bool IsAirTrick { get { return false; } }

  public override string Pattern
  {
    get { return "508"; }
  }

  public override int Score { get { return 200; } }
  public override string Name { get { return "Heelflip"; } }

  public Heelflip() : base()
  {
  }

  public override void DoExecute(GameObject player)
  {
    base.DoExecute(player);

    player.GetComponent<Rigidbody>().AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
    player.GetComponent<PlayerStateManager>().SwitchState(PlayerAirState.ID);

    TrickExecuter.StartCoroutine(Execute(player));
  }

  public override void Evaluate(string recordedTrick)
  {
    // set Heelflip parameters
  }
  protected override IEnumerator Execute(GameObject player)
  {
    // do Heelflip
    Debug.Log("Doing Heelflip....");
    yield return new WaitForSeconds(.25f);
    playerStats.DoingTrick = false;
    Debug.Log("Heelflip succesful!");
    yield return new WaitForSeconds(.5f); // wait to reset availability
    Available = true;
    Debug.Log("Heelflip available again");
  }
}

public class KickflipAir : Trick
{
  public override bool Available { get; set; }
  public override bool IsAirTrick { get { return true; } }

  public override string Pattern
  {
    get { return "02"; }
  }

  public override int Score { get { return 150; } }
  public override string Name { get { return "Kickflip (Air)"; } }

  public KickflipAir() : base()
  {
  }

  public override void DoExecute(GameObject player)
  {
    base.DoExecute(player);

    TrickExecuter.StartCoroutine(Execute(player));
  }

  public override void Evaluate(string recordedTrick)
  {
    // set kickflip parameters
  }
  protected override IEnumerator Execute(GameObject player)
  {
    // do air kickflip
    Debug.Log("Doing air kickflip....");
    yield return new WaitForSeconds(.25f);
    playerStats.DoingTrick = false;
    Debug.Log("Air Kickflip succesful!");
    yield return new WaitForSeconds(.25f); // wait to reset availability
    Available = true;
    Debug.Log("Air Kickflip available again");
  }
}

public class HeelFlipAir : Trick
{
  public override bool Available { get; set; }
  public override bool IsAirTrick { get { return true; } }

  public override string Pattern
  {
    get { return "08"; }
  }

  public override int Score { get { return 200; } }
  public override string Name { get { return "Heelflip (Air)"; } }

  public HeelFlipAir() : base()
  {
  }

  public override void DoExecute(GameObject player)
  {
    base.DoExecute(player);

    TrickExecuter.StartCoroutine(Execute(player));
  }

  public override void Evaluate(string recordedTrick)
  {
    // set heelflip parameters
  }
  protected override IEnumerator Execute(GameObject player)
  {
    // do air heelflip
    Debug.Log("Doing air heelflip....");
    yield return new WaitForSeconds(.25f);
    playerStats.DoingTrick = false;
    Debug.Log("Air heelflip succesful!");
    yield return new WaitForSeconds(.25f); // wait to reset availability
    Available = true;
    Debug.Log("Air heelflip available again");
  }
}