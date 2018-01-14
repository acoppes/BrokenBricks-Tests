
using System;
using ECS;
using MyTest.Systems;
using UnityEngine;

namespace MyTest.Components
{
	public class TargetNode : ISpatialNode {

		// public Entity entity;
		public Target target;

        public Bounds GetBounds()
        {
            return target.bounds;
        }
    }

	[Serializable]
	public struct TargetFilter
	{
		// declaration data to be used as filter that doesn't change over time.
		// or dynamic data that is cached here to improve the filtering later, 
		// for example: the current health percentage.
	}

	[Serializable]
	public struct Target
	{
		// [NonSerialized]
		// public Entity entity;
		
		public Bounds bounds;

		// public TargetFilter filters;

		[NonSerialized]
		public TargetNode node;
    }

	[Serializable]
	public struct TargetComponent : IComponent
	{
		public Target[] targets;
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
		public TargetNode targetNode; // store one target for now...
	}

	[Serializable]
	public struct TargetingComponent : IComponent
	{
		public Targeting[] targetings;
	}

}