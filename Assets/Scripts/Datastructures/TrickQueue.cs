using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// queue that is able to access data at index
public class TrickQueue
{
  private int[] queue;
  //string queue;
  private int length;
  private bool somethingHappened;

  public TrickQueue(int size)
  {
    length = size;
    queue = new int[length];
    //for (int i = 0; i < size; i++) queue += "0";
    Debug.Log("Trick queue[0]: " + queue[0]);
  }

  // enqueue element 'input' and automatically dequeues head
  public int EnqueueDequeue(int input)
  {
    int head = queue[0];
    for (int i = 0; i < length - 1; i++)
    {
      queue[i] = queue[i + 1];
    }
    queue[length - 1] = input;

    // update state
    somethingHappened = SomethingHappened();

    return head;
  }

  // if trick found, returns trick sequence and removes trick from trick buffer
  public string FindTrick(Trick trick)
  {
    if (somethingHappened)
    {
      // analyze if 'trick' happened
      //if (Contains(int.Parse(trick.Pattern)))
      //{
      //  return trick.Pattern;
      //}

      int patternIndex = trick.Pattern.Length - 1;
      char[] patternChars = trick.Pattern.ToCharArray();

      string recordedPattern = "";

      for (int i = 1; i < 4; i++)
      {
        string currentDigit = patternChars[patternIndex].ToString(); // "51" --> "1"
        int currentDigitInt = int.Parse(currentDigit);
        if (queue[length - i] == currentDigitInt)
        {
          recordedPattern += currentDigit;
          int recordingTraverseIndex = length - i - 1;

          while (recordingTraverseIndex >= 0)
          {
            // traverse recording, stop if digit doesn't match or complete trick was found
            if (queue[recordingTraverseIndex] == currentDigitInt)
            {
              recordedPattern += currentDigit;
              recordingTraverseIndex--;
              continue;
            }

            patternIndex--;
            if (patternIndex < 0) break;

            string nextDigit = patternChars[patternIndex].ToString();
            int nextDigitInt = int.Parse(nextDigit);

            if (queue[recordingTraverseIndex] == nextDigitInt)
            {
              currentDigit = nextDigit;
              currentDigitInt = nextDigitInt;

              recordedPattern += currentDigit;
              recordingTraverseIndex--;
            }

            else
            {
              break;
            }
          }

          // all chars found --> valid trick
          if (patternIndex == -1)
          {
            return recordedPattern;
          }

          // no use searching from the other input values
          if (recordedPattern.Length > 3) return "";

          break;
        }
      }
    }

    // nothing happened, return empty
    return "";
  }

  public bool Contains(int digit)
  {
    for (int i = 0; i < length; i++)
    {
      if (queue[i] == digit) return true;
    }

    return false;
  }

  public void DebugToConsole()
  {
    string buffer = "";
    foreach (int i in queue) buffer += i;

    //Debug.Log("Buffer: " + buffer);
  }

  // check if input in last 3 frames
  private bool SomethingHappened()
  {
    for (int i = 1; i < 4; i++)
    {
      if (queue[length - i] != 0) return true;
    }

    return false;
  }

}
