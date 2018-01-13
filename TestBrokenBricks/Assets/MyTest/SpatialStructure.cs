using System.Collections.Generic;
using UnityEngine;

namespace MyTest.Systems
{
    public class SpatialStructure
	{
		public class SpatialNode
		{
			SpatialStructure _spatialStructure;

			Bounds _bounds;

			public SpatialNode(SpatialStructure spatialStructure)
			{
				_spatialStructure = spatialStructure;
			}

			public void Update(Bounds bounds)
			{
				_bounds = bounds;
			}
		}

		List<SpatialNode> _nodes = new List<SpatialNode>();

		public SpatialNode Add() {
			var node =  new SpatialNode(this);
			_nodes.Add(node);
			return node;
		}

		public void Remove(SpatialNode node)
		{
			_nodes.Remove(node);
		}
	}
}