using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// this automaticly adds a spherecollider when its used
// otherwise it will atleast provide an error
[RequireComponent(typeof(CircleCollider2D))]

public abstract class GroupBehaviour : SteeringBehaviour
{
	protected List<Vehicle> neighbours = new List<Vehicle>();

	void OnTriggerEnter(Collider c)
	{
		if (c.gameObject.CompareTag(gameObject.tag))
		{
			Vehicle vehicle = c.GetComponent<Vehicle>();
			neighbours.Add(vehicle);

		}
	}


	void OnTriggerExit(Collider c)
	{
		if (c.gameObject.CompareTag(gameObject.tag))
		{
			Vehicle vehicle = c.GetComponent<Vehicle>();
			neighbours.Remove(vehicle);

		}
	}

}
