using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS4InputSystem : IInputSystem
{
  TrickQueue trickQueue;
  Transform player;
  //Queue<int> parsedInput;
  public TrickQueue GetSerializedTrickInput()
  {
    //return parsedInput;
    return trickQueue;
  }

  // Use this for initialization
  public PS4InputSystem()
  {
    //parsedInput = new Queue<int>();
    //for (int i = 0; i < 90; i++) parsedInput.Enqueue(0); // fill queue with 0

    trickQueue = new TrickQueue(90);
  }

  // Update is called once per frame
  public void Update()
  {
    // get new parsed input
    int newInputValue = ParseTrickInput(AnalogTrickInput());

    // update input buffer
    // parsedInput.Enqueue(newInputValue);
    //parsedInput.Enqueue(0);
    //parsedInput.Dequeue();

    trickQueue.EnqueueDequeue(newInputValue);
    //trickQueue.DebugToConsole();
    // USED FOR DEBUGGING
    /*string buffer = "";
    foreach (int i in parsedInput) buffer += i;

    Debug.Log("Buffer: " + buffer);*/
  }

  // returns raw trick-input
  public Vector2 AnalogMoveInput()
  {
    float x = Input.GetAxis("PS4_LeftStickX");
    float y = Input.GetAxis("PS4_LeftStickY");

    return new Vector2(x, y);
  }

  public Vector2 AnalogTrickInput()
  {
    float x = Input.GetAxis("PS4_RightStickX");
    float y = Input.GetAxis("PS4_RightStickY");

    return new Vector2(x, y);
  }

  private int ParseTrickInput(Vector2 input)
  {
    // actions only valid if joystick reaches ±max distance
    if (input.magnitude < 0.8f) return 0;

    float angle = Vector2.Angle(Vector2.down, input);
    if (input.x < 0) angle = 360f - angle;

    // map degree to 1-8 int
    float direction = (float)Math.Round(angle / 45f, MidpointRounding.AwayFromZero) % 8;
    direction += 1f;

    return (int)direction;
  }
}
