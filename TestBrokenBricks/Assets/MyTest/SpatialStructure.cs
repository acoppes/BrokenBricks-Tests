using System.Collections.Generic;
using MyTest.Components;
using UnityEngine;
using System.Linq;
using System;

namespace MyTest.Systems
{
	public interface ISpatialNode 
	{
		Bounds GetBounds();
	}

	public interface ISpatialStructure<T>
	{
		void Add(T spatialObject);

		void Remove(T spatialObject);

		void Update(T spatialObject);

		void Collect(Bounds bounds, List<T> spatialObjects);
	}

    public class SpatialStructure<T> : ISpatialStructure<T> where T : ISpatialNode
	{
		List<T> _nodes = new List<T>();

		// Func<T, Bounds> _boundsDelegate;

		// public SpatialStructure(Func<T, Bounds> boundsDelegate) {
		// 	_boundsDelegate = boundsDelegate;
		// }

        public void Add(T spatialObject)
        {
            _nodes.Add(spatialObject);
        }

        public void Remove(T spatialObject)
        {
			_nodes.Remove(spatialObject);
        }

        public void Update(T spatialObject)
        {
			// var newBounds = _boundsDelegate(spatialObject);
			// don't know yet
        }

        public void Collect(Bounds bounds, List<T> spatialObjects)
        {
            // for each node (target) that matches the bounds check, adds it to the targets array.
			// until targets array is complete or no more nodes (targets).
			// int currentTarget = 0;

			for (int i = 0; i < _nodes.Count; i++)
			{
				if (bounds.Intersects(_nodes[i].GetBounds())) {
					spatialObjects.Add(_nodes[i]);
					// targetsArray[currentTarget++] = _nodes[i].Target;
				}
			}
        }

    }
}