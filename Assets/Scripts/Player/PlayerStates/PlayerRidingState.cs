using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRidingState : PlayerState
{
  public static int ID;

  public PlayerRidingState() : base()
  {
    ID = GetNewID();
  }

  public override void Start()
  {
    Debug.Log("Player riding state started.");
    playerStats.Velocity = Vector3.forward * playerStats.Speed;
  }

  public override void Stop()
  {
    //Debug.Log("Player riding state stopped");
    //playerStats.Velocity = Vector3.zero;
  }

  // HandleInput(ref List<GameEvent> events);
  // events is een buffer van de events die plaatsgevonden hebben (events worden gegenereerd) door een
  //  class die kijkt naar bijv. de laatste kwart seconde input en daar een event aan koppelt. Events kunnen vervolgens
  //  worden geconsumed (waarna ze uit de lijst verwijderd worden). Of misschien wil je dat als de player een kickflip doet
  //  en hij zit nog 1 frame in de lucht, dat hij na die frame de event alsnog uitvoert. Een soort delay. 

  // input hoeft niet uit input buffer verwijderd te worden. Zeg gewoon: genereer een jump (als die dat detecteert).
  //  stop dit in een event buffer. Als die na 5 frames bijv. nog steeds niet uitgevoerd is, verwijder je 'm uit de buffer.
  //  
  public override void HandleInput(PlayerController playerController)
  {
    if (playerController.GetRawTrickControl().y > 0.5f)
    {
      stateManager.SwitchState(PlayerCrouchState.ID);
    }

    playerStats.Velocity = new Vector3(playerController.GetMovement().x * playerStats.Speed, playerStats.Velocity.y, playerStats.Velocity.z);

    List<Trick> tricks = playerController.GetTricks();
    if (tricks.Count != 0)
    {
      Trick latestTrick = tricks[tricks.Count - 1];

      tricks.RemoveAt(tricks.Count - 1);

      latestTrick.Evaluate("");
      Debug.Log(latestTrick);
      latestTrick.DoExecute(stateManager.gameObject);
    }

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
    playerStats.GetComponent<Rigidbody>().velocity = playerStats.Velocity * playerStats.Speed * Time.deltaTime;
  }
}
