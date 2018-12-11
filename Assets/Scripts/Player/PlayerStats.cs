using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
  Transform player;

  void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    Velocity = Vector3.zero;
    Speed = 130.0f;
    SteerSpeed = 230.0f;
  }

  public Vector3 Velocity
  {
    get;
    set;
  }
  public Vector3 Position
  {
    get { return player.position; }
    set { player.position = value; }
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
}
