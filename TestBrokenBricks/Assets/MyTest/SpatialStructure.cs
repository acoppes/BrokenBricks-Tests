using System.Collections.Generic;
using MyTest.Components;
using UnityEngine;
using System.Linq;

namespace MyTest.Systems
{
    public class SpatialStructure
	{
		public class SpatialNode
		{
            Target _target;
			SpatialStructure _spatialStructure;
			Bounds _bounds;

			public Target Target
			{
				get {
					return _target;
				}
			}

			public Bounds Bounds {
				get {
					return _bounds;
				}
			}

			public SpatialNode(SpatialStructure spatialStructure, Target target)
			{
				_spatialStructure = spatialStructure;
                _target = target;

				_target.node = this;
			}

			public void Update(Bounds bounds)
			{
				_bounds = bounds;
			}
		}

		List<SpatialNode> _nodes = new List<SpatialNode>();

		public void Add(Target target) {
			_nodes.Add(new SpatialNode(this, target));
		}

		public void Remove(Target target)
		{
			if (target.node == null)
					return;
			_nodes.Remove(target.node);
			target.node = null;
		}

		public void Update(Target target, Bounds bounds)
		{
			target.node.Update(bounds);
		}

        public void Collect(Bounds bounds, Target[] targetsArray)
        {
            // for each node (target) that matches the bounds check, adds it to the targets array.
			// until targets array is complete or no more nodes (targets).
			int currentTarget = 0;

			for (int i = 0; i < _nodes.Count; i++)
			{
				if (currentTarget >= targetsArray.Length)
					return;

				if (bounds.Intersects(_nodes[i].Bounds)) {
					targetsArray[currentTarget++] = _nodes[i].Target;
				}
			}
        }
	}
}