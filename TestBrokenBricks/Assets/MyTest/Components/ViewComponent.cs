using ECS;
using UnityEngine;
using System;

namespace MyTest.Components
{
	[Serializable]
	public struct ViewComponent : IComponent
	{
		public GameObject viewPrefab;
		[NonSerialized]
		public GameObject view;
		[NonSerialized]
		public Animator animator;
		[NonSerialized]
		public SpriteRenderer sprite;
	}
	
}