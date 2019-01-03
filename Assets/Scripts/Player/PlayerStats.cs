using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
  void Start()
  {
    Velocity = Vector3.zero;
    Speed = 250.0f;
    SteerSpeed = 230.0f;
  }

  public Vector3 Velocity
  {
    get;
    set;
  }
  public Vector3 Position
  {
    get { return transform.position; }
    set { transform.position = value; }
  }

  public float Speed
  {
    get;
    set;
  }

  public float SteerSpeed
  {
    get;
    set;
  }

  public bool DoingTrick
  {
    get;
    set;
  }

  public float LastRecordedCrouchValue
  {
    get;
    set;
  }
}
