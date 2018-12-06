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

    trickBuffer = new List<Trick>();
  }

  public List<Trick> GetTricks(Queue<int> parsedInput)
  {
    // maybe change this to a regex expression that takes a list of things it can match (trick patterns)
    //  and hopefully it will return tricks in the right order
    /*foreach (KeyValuePair<string, System.Type> trick in patternTrickMap)
    {
      // analyse inputbuffer

      // check if trick available (?)

      // add to buffer
    }*/

    // -------- DEBUG -----------
    if (parsedInput.Contains(3))
    {
      if (trickPool[typeof(Kickflip)].Available)
      {
        Debug.Log("Adding a kickflip to the buffer...");
        trickBuffer.Add(trickPool[typeof(Kickflip)]);
        //Debug.Log(trickBuffer.Count);
      }
    }
    // -------- END DEBUG -----------

    return trickBuffer;
  }
}
