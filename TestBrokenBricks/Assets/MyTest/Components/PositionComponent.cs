using ECS;
using UnityEngine;
using System;

namespace MyTest.Components
{
	[Serializable]
	public struct PositionComponent : IComponent
	{
		public Vector3 position;

		[NonSerialized]
		public Vector2 lookingDirection;
	}
	
}