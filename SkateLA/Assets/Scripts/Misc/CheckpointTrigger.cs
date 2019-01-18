using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
	public static CheckpointTrigger currentCheckpoint;

	void OnTriggerEnter(Collider c)
	{
		if (c.name == "Player")
		{
			currentCheckpoint = this;
		}
	}
}