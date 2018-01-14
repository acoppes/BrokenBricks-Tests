using ECS;
using UnityEngine;
using System;

namespace MyTest.Components
{

	[Serializable]
	public struct JumpComponent : IComponent
	{
		public float jumpForce;

		[NonSerialized]
		public float currentJumpForce;

		public float jumpStopFactor;

		[NonSerialized]
		public bool isJumping;

		[NonSerialized]
		public bool isFalling;
	}
	
}