using ECS;
using UnityEngine;
using System;

namespace MyTest.Components
{

	[Serializable]
	public class InputComponent : IComponent
	{
		public string horizontalAxisName;
		public string verticalAxisName;
		public string jumpActionName;
	}
	
}