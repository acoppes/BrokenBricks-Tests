using ECS;
using UnityEngine;
using System;

namespace MyTest.Components
{
	[Serializable]
	public struct MovementPhysicsComponent : IComponent
	{
		[NonSerialized]
		public Vector3 direction;

		// it is the movement force (acceleration)
		public float force;

		public float maxSpeedHorizontal;
	}
	
}