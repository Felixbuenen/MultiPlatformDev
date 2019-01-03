using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrickAnalyzer
{
  Dictionary<System.Type, Trick> trickPool;
  List<Trick> trickBuffer;

  private int bufferStartCheckIndex;

  public TrickAnalyzer()
  {
    bufferStartCheckIndex = 0;

    // initialize trickpool
    trickPool = new Dictionary<System.Type, Trick>();
    trickPool.Add(typeof(Ollie), new Ollie());
    trickPool.Add(typeof(Kickflip), new Kickflip());
    trickPool.Add(typeof(KickflipAir), new KickflipAir());
    trickPool.Add(typeof(Heelflip), new Heelflip());
    trickPool.Add(typeof(HeelFlipAir), new HeelFlipAir());

    trickBuffer = new List<Trick>();
  }

  public List<Trick> GetTricks(TrickQueue parsedInput)
  {
    bool debugFoundTrick = false;

    foreach (Trick trick in trickPool.Values)
    {
      string recordedTrick = parsedInput.FindTrick(trick);
      //parsedInput.DebugToConsole();
      if (recordedTrick != "")
      {
        debugFoundTrick = true;
        //Debug.Log(trick + " recorded. Pattern: " + recordedTrick);
        if (trick.Available)
        {
          Debug.Log("Adding " + trick + " to buffer...");
          trick.Evaluate(recordedTrick);
          trickBuffer.Add(trick);
          break;
        }
      }
    }

    return trickBuffer;
  }
}
