using ECS;
using UnityEngine;
using System;

namespace MyTest.Components
{

	[Serializable]
	public struct InputComponent : IComponent
	{
		public string horizontalAxisName;
		public string verticalAxisName;
		public string jumpActionName;
	}
	
}