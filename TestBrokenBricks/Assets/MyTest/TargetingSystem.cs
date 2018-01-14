using System.Collections.Generic;
using ECS;
using MyTest.Components;

namespace MyTest.Systems
{
	public class TargetingSystem : ComponentSystem
	{
		[InjectDependency]
		SpatialStructure _spatialStructure;

		ComponentGroup _group;

		List<SpatialStructure.SpatialNode> _nodes = new List<SpatialStructure.SpatialNode>();

		public TargetingSystem()
		{
			
		}

		public TargetingSystem(SpatialStructure spatialStructure)
		{
			_spatialStructure = spatialStructure;
		}

		public override void OnStart() {
			base.OnStart();

			_group = EntityManager.GetComponentGroup(
				typeof(TargetingComponent)
			);
		}

		public override void OnFixedUpdate() 
		{
			base.OnFixedUpdate();

			// updates targets in all targeting based on filters, etc.
			var targetingArray = _group.GetComponent<TargetingComponent> ();

			for (int i = 0; i < targetingArray.Length; i++) {
				var targetingComponent = targetingArray[i];

				for (int j = 0; j < targetingComponent.targetings.Length; j++)
				{
					var targeting = targetingComponent.targetings[j];
					 _spatialStructure.Collect(targeting.query.bounds, targeting.targets);

					 // filter targets given the query

					 _nodes.Clear();
				}
			}

		}

	}
}