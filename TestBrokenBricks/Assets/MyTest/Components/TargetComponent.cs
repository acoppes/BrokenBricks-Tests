
using System;
using ECS;
using MyTest.Systems;
using UnityEngine;

namespace MyTest.Components
{
	[Serializable]
	public struct TargetFilter
	{
		// declaration data to be used as filter that doesn't change over time.
		// or dynamic data that is cached here to improve the filtering later, 
		// for example: the current health percentage.
	}

	[Serializable]
	public class Target
	{
		[NonSerialized]
		public Entity entity;
		
		public Bounds bounds;
		public TargetFilter filters;

		[NonSerialized]
		public SpatialStructure.SpatialNode node;
    }

	[Serializable]
	public class TargetComponent : IComponent
	{
		public Target[] targets = new Target[1];
	}

	//

	[Serializable]
	public struct TargetingQuery
	{
		// limit of targets
		// public int quantity;
		public Bounds bounds;
		// sort order
		// sort function?
	}

	[Serializable]
	public struct Targeting 
	{
		public TargetingQuery query;
		
		[NonSerialized]
		public Target[] targets;
	}

	[Serializable]
	public class TargetingComponent : IComponent
	{
		public Targeting[] targetings = new Targeting[] {
			new Targeting {
				targets = new Target[1]
			}
		};
	}

}