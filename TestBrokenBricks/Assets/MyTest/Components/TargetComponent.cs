
using System;
using ECS;
using MyTest.Systems;
using UnityEngine;

namespace MyTest.Components
{
	public struct TargetFilter
	{
		// declaration data to be used as filter that doesn't change over time.
		// or dynamic data that is cached here to improve the filtering later, 
		// for example: the current health percentage.
	}

	public struct Target
	{
		public Entity entity;
		public Bounds bounds;
		public TargetFilter filters;
		public SpatialStructure.SpatialNode node;
    }

	public class TargetComponent : IComponent
	{
		public Target[] targets = new Target[1];
	}

	//

	public struct TargetingQuery
	{
		public int quantity;
		public Bounds bounds;
		// sort order
		// sort function?
	}

	public struct Targeting 
	{
		public TargetingQuery query;
		public Target[] targets;
	}

	public class TargetingComponent
	{
		public Targeting[] targetings = new Targeting[] {
			new Targeting {
				targets = new Target[1]
			}
		};
	}

}