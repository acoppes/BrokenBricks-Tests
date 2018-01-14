using ECS;
using UnityEngine;
using System;

namespace MyTest.Components
{
	[Serializable]
	public struct DelegatePhysicsComponent : IComponent {

		[NonSerialized]
		public Vector3 force;

		public Vector3 velocity;

		public float maxForce;

		public Vector3 position;

		public float gravityMultiplier;

		public float frictionMultiplier;

		public Vector3 AddForce(Vector3 force)
		{
			this.force += force;
			return this.force;
		}

		public void StopAtHeight(float height)
		{
			position.z = height;
			velocity.z = 0.0f;
		}

		public bool IsOnFloor()
		{
			return Mathf.Abs (position.z - 0.0f) < Mathf.Epsilon;
		}
	}
	
}