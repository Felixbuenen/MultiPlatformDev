using UnityEngine;

public abstract class PlayerState
{
  private static int currentID = 0;
  protected static int GetNewID()
  {
    return currentID++;
  }
  protected static PlayerStats playerStats;
  protected static PlayerStateManager stateManager;
  public static void SetPlayerStats(PlayerStats ps)
  {
    playerStats = ps;
    if (ps == null) Debug.Log("playerStats = null");
  }
  public static void SetPlayerStateManager(PlayerStateManager psm)
  {
    stateManager = psm;
  }

  public abstract void Start();
  public abstract void Stop();

  public abstract void HandleInput(PlayerController playerController);
  public abstract void Update();

  public virtual void OnCollisionEnter(Collision c) { }
  public virtual void OnCollisionExit() { }
}
