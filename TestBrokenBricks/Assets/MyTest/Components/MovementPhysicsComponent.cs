using ECS;
using UnityEngine;
using System;

namespace MyTest.Components
{

	[Serializable]
	public class MovementPhysicsComponent : IComponent
	{
		[NonSerialized]
		public Vector2 direction;

		// it is the movement force (acceleration)
		public float force;

		public float maxSpeedHorizontal;
	}
	
}