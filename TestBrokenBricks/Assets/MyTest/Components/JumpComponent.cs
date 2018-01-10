using ECS;
using UnityEngine;
using System;

namespace MyTest.Components
{

	[Serializable]
	public class JumpComponent : IComponent
	{
		public float jumpForce;

		[NonSerialized]
		public float currentJumpForce;

		public float jumpStopFactor;

		[NonSerialized]
		public bool isJumping = false;

		[NonSerialized]
		public bool isFalling = false;
	}
	
}