using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS4InputSystem : IInputSystem
{
  public class UIPS4Input : IUIInput
  {
    private bool DPadPressed = false;

    public bool GetBackButton() { return Input.GetButtonDown("PS4_O"); }
    public bool GetPressButton() { return Input.GetButtonDown("PS4_X"); }
    public bool GetUpButton()
    {
      float value = Input.GetAxisRaw("PS4_VERT_BTN");

      if (DPadPressed && value == 0f)
      {
        DPadPressed = false;
        return false;
      }
      else if (!DPadPressed && value > 0f)
      {
        DPadPressed = true;
        return true;
      }

      return false;
    }

    public bool GetDownButton()
    {
      float value = Input.GetAxisRaw("PS4_VERT_BTN");

      if (DPadPressed && value == 0f)
      {
        DPadPressed = false;
        return false;
      }
      else if (!DPadPressed && value < 0f)
      {
        DPadPressed = true;
        return true;
      }

      return false;
    }
  }

  public IUIInput UIInput { get { return ui; } }
  private IUIInput ui;

  TrickQueue trickQueue;
  Transform player;

  public TrickQueue GetSerializedTrickInput()
  {
    return trickQueue;
  }

  // Use this for initialization
  public PS4InputSystem()
  {
    trickQueue = new TrickQueue(90);
    ui = new UIPS4Input();
  }

  // Update is called once per frame
  public void Update()
  {
    // get new parsed input
    int newInputValue = ParseTrickInput(AnalogTrickInput());

    // update input buffer
    trickQueue.EnqueueDequeue(newInputValue);
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

    float angle = Vector2.Angle(Vector2.up, input);
    if (input.x < 0) angle = 360f - angle;

    // map degree to 1-8 int
    float direction = (float) Math.Round(angle / 45f, MidpointRounding.AwayFromZero) % 8;
    direction += 1f;

    return (int) direction;
  }
}