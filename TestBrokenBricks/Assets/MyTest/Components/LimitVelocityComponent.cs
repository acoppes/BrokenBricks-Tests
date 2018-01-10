using ECS;
using UnityEngine;
using System;

namespace MyTest.Components
{

	[Serializable]
	public class LimitVelocityComponent : IComponent
	{
		public float maxSpeedHorizontal;
	}
	
}