using ECS;
using UnityEngine;
using System;

namespace MyTest.Components
{
    [Serializable]
	public struct IABehaviourComponent : IComponent
	{
		public bool waitingForAction;

		[NonSerialized]
		public bool walking;

		[NonSerialized]
		public float actionTime;

		public float waitForActionTime;

		public float maxRandomDistance;

		[NonSerialized]
		public Vector3 destination;

	}
}