﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleClass : MonoBehaviour
{
	// See Order of Execution for Event Functions for information on FixedUpdate() and Update() related to physics queries
	void FixedUpdate()
	{
		// Bit shift the index of the layer (8) to get a bit mask

		// This would cast rays only against colliders in layer 8.
		// But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.


		RaycastHit hit;
		// Does the ray intersect any objects excluding the player layer
		if (Physics.Raycast(transform.position, -Vector3.up, out hit, Mathf.Infinity, 1 << 10))
		{
			Debug.DrawRay(transform.position, -Vector3.up * hit.distance, Color.yellow);
			Debug.Log("Did Hit");
		}
		else
		{
			Debug.DrawRay(transform.position, -Vector3.up * 1000, Color.white);
			Debug.Log("Did not Hit");
		}
	}
}