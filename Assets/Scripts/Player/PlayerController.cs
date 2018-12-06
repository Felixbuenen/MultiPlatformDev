using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
  TrickAnalyzer trickAnalyzer;
  PS4InputSystem inputSystem;

  public PlayerController()
  {
    trickAnalyzer = new TrickAnalyzer();
    if (!ServiceManager.Singleton.RequestService<PS4InputSystem>(out inputSystem))
    {
      Debug.Log("------- PlayerController COULD NOT GET PS4InputSystem -------");
    }
  }

  public void Update()
  {
    inputSystem.Update();
  }

  public List<Trick> GetTricks()
  {
    List<Trick> tricks = trickAnalyzer.GetTricks(inputSystem.ParsedInput);
    return tricks;
  }

  public Vector2 GetMovement()
  {
    Debug.Log("GetMovement() NOT YET IMPLEMENTED");
    return Vector2.zero;
  }

  public Vector2 GetRawTrickControl()
  {
    return inputSystem.AnalogTrickInput();
  }
}
