using System.Collections.Generic;
using ECS;
using Gemserk.ECS;
using MyTest.Components;

namespace MyTest.Systems
{
	public class TargetingSystem : ComponentSystem
	{
		ISpatialStructure<TargetNode> _spatialStructure;
		
		[InjectDependency]
		EntityManager _entityManager;

		ComponentTuple<TargetingComponent> _tuple;

		List<TargetNode> _nodes = new List<TargetNode>();

		public TargetingSystem(ISpatialStructure<TargetNode> spatialStructure)
		{
			_spatialStructure = spatialStructure;
		}

		public override void OnStart() {
			base.OnStart();
			_tuple = new ComponentTuple<TargetingComponent>(_entityManager);
		}

		public override void OnFixedUpdate() 
		{
			base.OnFixedUpdate();

			// updates targets in all targeting based on filters, etc.

			for (int i = 0; i < _tuple.Count; i++) {
				_tuple.EntityIndex = i;

				var targetingComponent = _tuple.component1;

				for (int j = 0; j < targetingComponent.targetings.Length; j++)
				{
					var targeting = targetingComponent.targetings[j];
					_spatialStructure.Collect(targeting.query.bounds, _nodes);

					targeting.targetNode = null;

					// storing only one target for now, and filtering 
					// and sorting logic is missing yet.

					if (_nodes.Count > 0) 
						targeting.targetNode = _nodes[0];

					 // filter targets given the query

					_nodes.Clear();
				}
			}

		}

	}
}