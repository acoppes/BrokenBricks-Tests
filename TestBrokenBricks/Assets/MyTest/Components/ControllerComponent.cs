using ECS;
using UnityEngine;
using System;

namespace MyTest.Components
{
	[Serializable]
	public struct ControllerComponent : IComponent
	{
		[NonSerialized]
		public Vector3 movement;

		[NonSerialized]
		public bool isJumpPressed;
	}
	
}