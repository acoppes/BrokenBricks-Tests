using System.Collections.Generic;
using ECS;
using Gemserk.ECS;
using MyTest.Components;
using UnityEngine;

namespace MyTest.Systems
{
	public class TargetSystem : ComponentSystem, IEntityAddedEventListener, IEntityRemovedEventListener
	{
		[InjectDependency]
		EntityManager _entityManager;

		ISpatialStructure<TargetNode> _spatialStructure;
		ComponentTuple<PositionComponent, TargetComponent> _tuple;


		public TargetSystem(ISpatialStructure<TargetNode> spatialStructure)
		{
			_spatialStructure = spatialStructure;
		}

		public override void OnStart() {
			base.OnStart();

			_tuple = new ComponentTuple<PositionComponent, TargetComponent>(_entityManager);

			_tuple.Group.SubscribeOnEntityAdded(this);
			_tuple.Group.SubscribeOnEntityRemoved(this);
		}

		public void OnEntityAdded(object sender, Entity entity)
		{
			var targetComponent = entity.GetComponent<TargetComponent>();
			var positionComponent = entity.GetComponent<PositionComponent>();

			for (int i = 0; i < targetComponent.targets.Length; i++)
			{
				targetComponent.targets[i].node = new TargetNode() {
					// entity = entity,
					target = targetComponent.targets[i]
				};
				_spatialStructure.Add(targetComponent.targets[i].node);
			}

			entity.SetComponent(targetComponent);
		}

		public void OnEntityRemoved(object sender, Entity entity)
		{
			var targetComponent = entity.GetComponent<TargetComponent>();
			
			for (int i = 0; i < targetComponent.targets.Length; i++)
			{
				_spatialStructure.Remove(targetComponent.targets[i].node);
				targetComponent.targets[i].node = null;
			}

			entity.SetComponent(targetComponent);
		}

		public override void OnFixedUpdate() 
		{
			base.OnFixedUpdate();

			// updates spatial structure

			for (int i = 0; i < _tuple.Count; i++) {
				_tuple.EntityIndex = i;

				var positionComponent = _tuple.component1;
				var targetComponent = _tuple.component2;

				if (targetComponent.targets == null)
					continue;

				for (int j = 0; j < targetComponent.targets.Length; j++)
				{
					var target = targetComponent.targets[j];

					var bounds = new Bounds(
						positionComponent.position + target.bounds.center,
						target.bounds.extents);

					target.bounds = bounds;

					target.node.target = target; // strange ...

					_spatialStructure.Update(target.node);
				}

				_tuple.component2 = targetComponent;
			}

		}

	}
}