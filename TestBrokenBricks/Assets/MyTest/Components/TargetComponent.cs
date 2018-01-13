
using System;
using ECS;
using MyTest.Systems;
using UnityEngine;

namespace MyTest.Components
{
	public struct TargetFilter
	{

	}

	public struct Target
	{
		public Bounds bounds;
		public TargetFilter filters;
		public SpatialNode node;
    }

	public class TargetComponent : IComponent
	{
		public Target[] targets = new Target[1];
	}

}