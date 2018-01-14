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
			_group.SubscribeOnEntityRemoved(this);
		}

		public void OnEntityAdded(object sender, Entity entity)
		{
			var targetComponent = entity.GetComponent<TargetComponent>();
			var positionComponent = entity.GetComponent<PositionComponent>();

			for (int i = 0; i < targetComponent.targets.Length; i++)
			{
				 _spatialStructure.Add(targetComponent.targets[i]);
			}
		}

		public void OnEntityRemoved(object sender, Entity entity)
		{
			var targetComponent = entity.GetComponent<TargetComponent>();
			
			for (int i = 0; i < targetComponent.targets.Length; i++)
			{
				_spatialStructure.Remove(targetComponent.targets[i]);
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

					_spatialStructure.Update(target, bounds);
				}
			}

		}

	}
}