using ECS;
using UnityEngine;
using System;

namespace MyTest.Components
{

	[Serializable]
	public class ControllerComponent : IComponent
	{
		[NonSerialized]
		public Vector2 movement;

		[NonSerialized]
		public bool isJumpPressed;
	}
	
}