using ECS;
using UnityEngine;
using System;

namespace MyTest.Components
{

	[Serializable]
	public class ViewComponent : IComponent
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