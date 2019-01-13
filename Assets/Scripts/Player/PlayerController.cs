using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
  TrickAnalyzer trickAnalyzer;
  IInputSystem inputSystem;

  public PlayerController()
  {
    trickAnalyzer = new TrickAnalyzer();
    if (!ServiceManager.Singleton.RequestService<IInputSystem>(out inputSystem))
    {
      Debug.Log("------- PlayerController COULD NOT GET InputSystem -------");
    }
  }

  public void Update()
  {
    inputSystem.Update();
  }

  public List<Trick> GetTricks()
  {
    List<Trick> tricks = trickAnalyzer.GetTricks(inputSystem.GetSerializedTrickInput());
    return tricks;
  }

  public Vector2 GetMovement()
  {
    return inputSystem.AnalogMoveInput();
  }

  public Vector2 GetRawTrickControl()
  {
    return inputSystem.AnalogTrickInput();
  }
}