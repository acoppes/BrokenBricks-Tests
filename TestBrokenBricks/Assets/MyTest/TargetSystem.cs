using System.Collections.Generic;
using ECS;
using MyTest.Components;
using UnityEngine;

namespace MyTest.Systems
{
	public class TargetSystem : ComponentSystem, IEntityAddedEventListener, IEntityRemovedEventListener
	{
		SpatialStructure _spatialStructure;

		ComponentGroup _group;

		public TargetSystem(SpatialStructure spatialStructure)
		{
			_spatialStructure = spatialStructure;
		}

		public override void OnStart() {
			base.OnStart();

			_group = EntityManager.GetComponentGroup(
				typeof(PositionComponent),
				typeof(TargetComponent)
			);
			
			_group.SubscribeOnEntityAdded(this);
		}

		public void OnEntityAdded(object sender, Entity entity)
		{
			var targetComponent = entity.GetComponent<TargetComponent>();
			var positionComponent = entity.GetComponent<PositionComponent>();

			for (int i = 0; i < targetComponent.targets.Length; i++)
			{
				var target = targetComponent.targets[i];
				target.node = _spatialStructure.Add();
				targetComponent.targets[i] = target;
			}
		}

		public void OnEntityRemoved(object sender, Entity entity)
		{
			var targetComponent = entity.GetComponent<TargetComponent>();
			
			for (int i = 0; i < targetComponent.targets.Length; i++)
			{
				var target = targetComponent.targets[i];
				_spatialStructure.Remove(target.node);
				target.node = null;
			}
		}

		public override void OnFixedUpdate() 
		{
			base.OnFixedUpdate();

			// updates spatial structure
			var positionArray = _group.GetComponent<PositionComponent> ();
			var targetArray = _group.GetComponent<TargetComponent>();

			for (int i = 0; i < targetArray.Length; i++) {
				var targetComponent = targetArray[i];
				var positionComponent = positionArray[i];

				for (int j = 0; j < targetComponent.targets.Length; j++)
				{
					var target = targetComponent.targets[i];

					var bounds = new Bounds(
						positionComponent.position + target.bounds.center,
						target.bounds.extents);

					target.node.Update(bounds);
				}
			}

		}

	}
}