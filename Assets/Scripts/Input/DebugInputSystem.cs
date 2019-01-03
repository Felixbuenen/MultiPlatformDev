using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DebugInputSystem : IInputSystem
{
  TrickQueue trickQueue;
  float steerSensitivity = 1.5f;

  // vars for input radius
  Vector2 inputCentre;
  float inputRadius; // in pixels

  bool playerTouchingScreen
  {
    get { return Input.GetMouseButton(0); }
  }
  bool playerTouchedScreen
  {
    get { return Input.GetMouseButtonDown(0); }
  }

  public TrickQueue GetSerializedTrickInput()
  {
    return trickQueue;
  }

  // Use this for initialization
  public DebugInputSystem()
  {
    Debug.Log("<b><color=red>------------ DEBUG INPUT ENABLED ---------------</color></b>");

    inputCentre = Vector2.zero;
    inputRadius = 75f;

    trickQueue = new TrickQueue(90);
  }

  public void Update()
  {
    int newInputValue = 0;

    // checks if player just touched the screen
    if (playerTouchedScreen)
    {
      inputCentre = Input.mousePosition;
    }

    newInputValue = ParseTrickInput(AnalogTrickInput());

    trickQueue.EnqueueDequeue(newInputValue);
    //trickQueue.DebugToConsole();

  }

  Vector2 movement = new Vector2();
  public Vector2 AnalogMoveInput()
  {
    float x = Input.GetAxis("PS4_LeftStickX");
    float y = Input.GetAxis("PS4_LeftStickY");

    return new Vector2(x, y);
  }

  Vector2 trickInputPosition = new Vector2();
  public Vector2 AnalogTrickInput()
  {
    if (!playerTouchingScreen) return Vector2.zero;

    trickInputPosition.x = (Input.mousePosition.x - inputCentre.x) / inputRadius;
    trickInputPosition.y = (Input.mousePosition.y - inputCentre.y) / inputRadius;

    float inputVectorLength = trickInputPosition.magnitude;
    float difference = (inputVectorLength * inputRadius) - inputRadius;
    if (difference > 0)
    {
      //Debug.Log("CENTRE CHANGED");
      Vector2 normalizedInputVector = (trickInputPosition / inputVectorLength);
      Vector2 diffVector = normalizedInputVector * difference;

      inputCentre += diffVector;
      trickInputPosition = normalizedInputVector;
    }

    Debug.Log("<b>Trick position: </b>" + trickInputPosition);

    return trickInputPosition;
  }

  private int ParseTrickInput(Vector2 input)
  {
    if (input.magnitude < 0.8f) return 0;

    float angle = Vector2.Angle(Vector2.up, input);
    if (input.x < 0) angle = 360f - angle;

    // map degree to 1-8 int
    float direction = (float)Math.Round(angle / 45f, MidpointRounding.AwayFromZero) % 8;
    direction += 1f;

    Debug.Log("Direction: " + direction);

    return (int)direction;
  }

}
