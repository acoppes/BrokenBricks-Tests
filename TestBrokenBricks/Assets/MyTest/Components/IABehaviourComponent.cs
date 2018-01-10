using ECS;
using UnityEngine;
using System;

namespace MyTest.Components
{
	[Serializable]
	public class IABehaviourComponent : IComponent
	{
		// link to behaviour tree?

		[NonSerialized]
		public bool waitingForAction = true;

		[NonSerialized]
		public bool walking;

		[NonSerialized]
		public float actionTime;

		public float waitForActionTime;

		public float maxRandomDistance = 1.0f;

		[NonSerialized]
		public Vector3 destination;

	}
}