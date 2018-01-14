using ECS;
using UnityEngine;
using System;

namespace MyTest.Components
{

	[Serializable]
	public struct LimitVelocityComponent : IComponent
	{
		public float maxSpeedHorizontal;
	}
	
}